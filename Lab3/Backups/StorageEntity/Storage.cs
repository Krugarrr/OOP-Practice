using Backups.Entities;
using Backups.Repository;

namespace Backups.StorageEntity;

public class Storage : IStorage
{
    public Storage(IRepository repository, string path)
    {
        ArgumentNullException.ThrowIfNull(repository);
        if (string.IsNullOrWhiteSpace(path))
            throw new Exception();

        Repository = repository as FileSystemRepository;
        Path = path;
    }

    public FileSystemRepository Repository { get; }
    public string Path { get; }
}