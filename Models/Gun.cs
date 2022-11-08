using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArmoryManagerApi.Models;

public class Gun
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Category { get; set; } = null!;
}
