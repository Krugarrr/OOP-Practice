using Backups.Entities;
using Backups.RepositoryObjects.Interfaces;
using Newtonsoft.Json;

namespace Backups.StorageEntity;

public class SplitStorage : IStorage
{
    [JsonProperty("storages")]
    private readonly List<IStorage> _storages;

    public SplitStorage(List<IStorage> storages)
    {
        ArgumentNullException.ThrowIfNull(storages);
        _storages = storages;
    }

    public IReadOnlyList<IRepositoryObject> GetObjects() => _storages.SelectMany(s => s.GetObjects()).ToList();

    public void Suicide()
    {
        foreach (IStorage s in _storages)
        {
            s.Suicide();
        }
    }
}