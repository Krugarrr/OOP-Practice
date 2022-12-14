using System.IO.Compression;
using Backups.ArchiverObjects;
using Backups.ArchiverObjects.Interface;
using Backups.Entities;
using Backups.RepositoryObjects;
using ZipFile = Backups.ArchiverObjects.ZipFile;

namespace Backups.Visitor;

public class RepositoryObjectVisitor
{
    private readonly Stack<IZipObject> zipObjectsStack;
    private readonly Stack<ZipArchive> archivesStack;

    public RepositoryObjectVisitor(ZipArchive archive)
    {
        archivesStack = new Stack<ZipArchive>();
        zipObjectsStack = new Stack<IZipObject>();
        archivesStack.Push(archive);
    }

    public void VisitFile(UserFile userFile)
    {
        string userFileName = userFile.Name;
        ZipArchiveEntry archiveEntry = archivesStack.Peek().CreateEntry(userFileName);
        using Stream archiveEntryStream = archiveEntry.Open();
        using Stream userFileStream = userFile.Abobus();
        userFileStream.CopyTo(archiveEntryStream);
    }

    public void VisitDirectory(UserDirectory userDir)
    {
        string userDirName = userDir.Name;
        ZipArchiveEntry archiveEntry = archivesStack.Peek().CreateEntry(userDirName);
        using Stream archiveEntryStream = archiveEntry.Open();
        using ZipArchive archive = new ZipArchive(archiveEntryStream, ZipArchiveMode.Create);
        archivesStack.Push(archive);
        foreach (var dir in userDir.Aboba())
        {
            dir.Accept(this);
        }

        archivesStack.Pop();
    }
}