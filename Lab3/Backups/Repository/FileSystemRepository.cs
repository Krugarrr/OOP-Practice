using Backups.Entities;
using Backups.Exceptions;
using Backups.RepositoryObjects;
using Backups.RepositoryObjects.Interfaces;
using Directory = System.IO.Directory;
using File = System.IO.File;

namespace Backups.Repository;

public class FileSystemRepository : IRepository
{
    private readonly Func<string, Stream> _userFilestream;
    private readonly Func<string, List<IRepositoryObject>> _userDirectoryStream;

    public FileSystemRepository(string repositoryPath)
    {
        ValidatePath(repositoryPath);

        if (!File.GetAttributes(repositoryPath).HasFlag(FileAttributes.Directory))
            throw new Exception();

        RepositoryPath = repositoryPath;
        _userFilestream = OpenFileStream;
        _userDirectoryStream = OpenDirectoryStream;
    }

    public string RepositoryPath { get; }
    public IRepositoryObject CreateRepositoryObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        string objectPath = backupObject.ObjectPath;

        if (File.Exists(objectPath))
            return new UserFile(new FileInfo(objectPath).Name, () => _userFilestream(objectPath));

        if (Directory.Exists(objectPath))
            return new UserDirectory(new DirectoryInfo(objectPath).Name, () => _userDirectoryStream(objectPath));

        throw RepositoryException.RestorePointCreationException();
    }

    public Stream OpenFileStream(string path)
    {
        ValidatePath(path);
        return File.Open(path, FileMode.OpenOrCreate);
    }

    public List<IRepositoryObject> OpenDirectoryStream(string path)
    {
        ValidatePath(path);
        var directories = new List<IRepositoryObject>();
        var files = new List<IRepositoryObject>();

        foreach (string directory in Directory.GetDirectories(path))
        {
            string fullPath = $"{directory}";
            directories.Add(new UserDirectory(new DirectoryInfo(fullPath).Name, () => _userDirectoryStream(fullPath)));
        }

        foreach (string file in Directory.GetFiles(path))
        {
            string fullPath = $"{file}";
            files.Add(new UserFile(new FileInfo(fullPath).Name, () => _userFilestream(fullPath)));
        }

        return files.Union(directories).ToList();
    }

    protected void ValidatePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw PathException.PathIsNullOrEmptyException();
    }
}