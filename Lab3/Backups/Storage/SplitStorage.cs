using System.Runtime.InteropServices.ComTypes;
using Backups.Repository;
using Backups.RepositoryObjects.Interface;

namespace Backups.Entities;

public class SplitStorage : IStorage
{
    private readonly List<IStorage> _storages;

    public SplitStorage(IReadOnlyList<IStorage> storages)
    {
        Storages = storages;
    }

    public IReadOnlyList<IStorage> Storages { get; }
    public FSRepository Repository { get; }
    public string StoragePath { get; }
}