using System.IO.Compression;
using Backups.Entities;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;
using Backups.StorageEntity;
using Backups.Visitor;

namespace Backups.Archiver;

public class ZipArchiver : IArchiver
{
    public IStorage CreateZipStorage(
        string archivePath,
        IReadOnlyList<IRepositoryObject> repoObjects,
        IRepository repository)
    {
        Stream archiveStream = repository.OpenFile($"{archivePath}.zip");
        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
        var visitor = new ZipArchiveVisitor(archive);
        foreach (IRepositoryObject obj in repoObjects)
            obj.Accept(visitor);

        var storage = new Storage(repository, $"${{arhivePath}}{repository.RepositoryPath}");
        return storage;
    }
}