using ArmoryManagerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ArmoryManagerApi.Services;

public class GunsService
{
    private readonly IMongoCollection<Gun> _gunsCollection;

    public GunsService(
        IOptions<ArmoryManagerDatabaseSettings> armoryManagerDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            armoryManagerDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            armoryManagerDatabaseSettings.Value.DatabaseName);

        _gunsCollection = mongoDatabase.GetCollection<Gun>("Guns");
    }

    public async Task<List<Gun>> GetAsync() =>
        await _gunsCollection.Find(_ => true).ToListAsync();

    public async Task<Gun?> GetAsync(string id) =>
        await _gunsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Gun newGun) =>
        await _gunsCollection.InsertOneAsync(newGun);

    public async Task UpdateAsync(string id, Gun updatedGun) =>
        await _gunsCollection.ReplaceOneAsync(x => x.Id == id, updatedGun);

    public async Task RemoveAsync(string id) =>
        await _gunsCollection.DeleteOneAsync(x => x.Id == id);
}