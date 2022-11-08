using ArmoryManagerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ArmoryManagerApi.Services
{
    public class PicklistService
    {
        private readonly IMongoCollection<Picklist> _picklistsCollection;

        public PicklistService(IOptions<ArmoryManagerDatabaseSettings> armoryManagerDatabaseSettings)
        {
            var mongoClient = new MongoClient(armoryManagerDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(armoryManagerDatabaseSettings.Value.DatabaseName);

            _picklistsCollection = mongoDatabase.GetCollection<Picklist>("Picklists");
        }

        public async Task<List<Picklist>> GetAsync() =>
            await _picklistsCollection.Find(_ => true).ToListAsync();

       public async Task<List<Picklist>> GetAsyncByName(string name) =>
            await _picklistsCollection.Find(x => x.Name == name).ToListAsync();

        public async Task<Picklist?> GetAsync(string id) =>
            await _picklistsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Picklist newPicklist) =>
            await _picklistsCollection.InsertOneAsync(newPicklist);
        public async Task RemoveAsync(string id) =>
            await _picklistsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
