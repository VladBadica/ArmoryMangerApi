namespace ArmoryManagerApi.Models;

public class ArmoryManagerDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string GunsCollectionName { get; set; } = null!;
}