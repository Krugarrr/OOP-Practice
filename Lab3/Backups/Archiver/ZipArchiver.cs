using System.IO.Compression;
using Backups.Repository;
using Backups.RepositoryObjects.Interface;
using Backups.Visitor;
using Backups.Visitor.Interface;

namespace Backups.Entities;

public class Archiver
{
    public IStorage CreateZipStorage(
        string archivePath,
        string archiveName,
        IReadOnlyList<IRepositoryObject> repoObjects,
        IRepository repository)
    {
        Stream archiveStream = repository.OpenFile($"{archivePath}{archiveName}");
        using ZipArchive archive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
        RepositoryObjectVisitor visitor = new RepositoryObjectVisitor(archive);
        foreach (var obj in repoObjects)
        {
            obj.Accept(visitor);
        }

        return null;
    }
}