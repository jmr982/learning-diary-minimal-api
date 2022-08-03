using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
string databaseName = "schoolwork";
string collectionName = "learning-diary";

var builder = WebApplication.CreateBuilder(args);

var settings = MongoClientSettings.FromConnectionString(connectionString);
var client = new MongoClient(settings);
var database = client.GetDatabase(databaseName);
var collection = database.GetCollection<Report>(collectionName, null);

var app = builder.Build();

app.MapGet("/", () => "All reports /report\nBy date /report/mm-dd-yyyy");
//get all
app.MapGet("/report", async () => await collection.Find(new BsonDocument()).ToListAsync());
//get by date
app.MapGet("/report/{date}", async (string date) => await collection.Find(new BsonDocument { { "date", date } }).ToListAsync());

app.Run();

[BsonIgnoreExtraElements]
public class Report
{
    public string date { get; set; } = null!;
    public string description { get; set; } = null!;
}