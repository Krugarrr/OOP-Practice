using System.IO.Compression;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;
using Backups.StorageEntity;
using Backups.Visitor;

namespace Backups.Archiver;

public class ZipArchiver : IArchiver
{
    public IStorage Archive(
        string archivePath,
        IReadOnlyList<IRepositoryObject> repoObjects,
        IRepository repository)
    {
        if (string.IsNullOrWhiteSpace(archivePath))
            throw PathException.PathIsNullOrEmptyException();
        ArgumentNullException.ThrowIfNull(repoObjects);
        ArgumentNullException.ThrowIfNull(repository);

        Stream archiveStream = repository.OpenFileStream($"{repository.RepositoryPath}{archivePath}.zip");
        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
        var visitor = new ZipArchiveVisitor(archive);
        foreach (IRepositoryObject obj in repoObjects)
            obj.Accept(visitor);

        var storage = new Storage(repository, visitor.GetZipObjects(), $"{archivePath}");
        return storage;
    }
}