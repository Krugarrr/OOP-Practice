using System.IO.Compression;
using Backups.ArchiveEntities;
using Backups.RepositoryObjects.Interfaces;
using Backups.Visitor.Interfaces;
using ZipFile = Backups.ArchiveEntities.ZipFile;

namespace Backups.Visitor;

public class ZipArchiveVisitor : IZipArchiveVisitor
{
    private readonly Stack<ZipArchive> _archivesStack;
    private readonly Stack<List<IZipObject>> _objects;

    public ZipArchiveVisitor(ZipArchive archive)
    {
        ArgumentNullException.ThrowIfNull(archive);
        _archivesStack = new Stack<ZipArchive>();
        _objects = new Stack<List<IZipObject>>();
        _archivesStack.Push(archive);
        _objects.Push(new List<IZipObject>());
    }

    public void VisitFile(IFile userFile)
    {
        ArgumentNullException.ThrowIfNull(userFile);

        ZipArchive archiveStackPeek = _archivesStack.Peek();
        ZipArchiveEntry archiveEntry = archiveStackPeek.CreateEntry(userFile.Name);

        using Stream archiveEntryStream = archiveEntry.Open();
        using Stream userFileStream = userFile.GetContents();
        userFileStream.CopyTo(archiveEntryStream);
        _objects.Peek().Add(new ZipFile(userFile.Name));
    }

    public void VisitDirectory(IDirectory userDirectory)
    {
        ArgumentNullException.ThrowIfNull(userDirectory);

        ZipArchive archiveStackPeek = _archivesStack.Peek();
        ZipArchiveEntry archiveEntry = archiveStackPeek.CreateEntry($"{userDirectory.Name}.zip");

        using Stream archiveEntryStream = archiveEntry.Open();
        using var archive = new ZipArchive(archiveEntryStream, ZipArchiveMode.Create);
        _archivesStack.Push(archive);
        _objects.Push(new List<IZipObject>());

        foreach (IRepositoryObject directory in userDirectory.GetContents())
        {
            directory.Accept(this);
        }

        List<IZipObject> zipFolder = _objects.Pop();
        _objects.Peek().Add(new ZipDirectory(userDirectory.Name, zipFolder));

        _archivesStack.Pop();
    }

    public IReadOnlyList<IZipObject> GetZipObjects() => _objects.Peek();
}