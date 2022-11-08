using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ArmoryManagerApi.Models
{
    public class Picklist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;
    }
}
