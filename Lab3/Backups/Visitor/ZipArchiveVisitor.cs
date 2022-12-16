using System.IO.Compression;
using Backups.RepositoryObjects.Interfaces;
using Backups.Visitor.Interfaces;

namespace Backups.Visitor;

public class ZipArchiveVisitor : IZipArchiveVisitor
{
    private readonly Stack<ZipArchive> _archivesStack;

    public ZipArchiveVisitor(ZipArchive archive)
    {
        ArgumentNullException.ThrowIfNull(archive);
        _archivesStack = new Stack<ZipArchive>();
        _archivesStack.Push(archive);
    }

    public void VisitFile(IFile userFile)
    {
        ArgumentNullException.ThrowIfNull(userFile);

        ZipArchive archiveStackPeek = _archivesStack.Peek();
        ZipArchiveEntry archiveEntry = archiveStackPeek.CreateEntry(userFile.Name);

        using Stream archiveEntryStream = archiveEntry.Open();
        using Stream userFileStream = userFile.InvokeStream();
        userFileStream.CopyTo(archiveEntryStream);
    }

    public void VisitDirectory(IDirectory userDirectory)
    {
        ArgumentNullException.ThrowIfNull(userDirectory);

        ZipArchive archiveStackPeek = _archivesStack.Peek();
        ZipArchiveEntry archiveEntry = archiveStackPeek.CreateEntry(userDirectory.Name + ".zip");

        using Stream archiveEntryStream = archiveEntry.Open();
        using var archive = new ZipArchive(archiveEntryStream, ZipArchiveMode.Create);
        _archivesStack.Push(archive);
        foreach (IRepositoryObject directory in userDirectory.InvokeStream())
        {
            directory.Accept(this);
        }

        _archivesStack.Pop();
    }
}