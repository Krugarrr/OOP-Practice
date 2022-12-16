using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using Backups.Entities;
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
        _userFilestream = path => OpenFile(path);
        _userDirectoryStream = path => OpenDir(path);
    }

    public string RepositoryPath { get; }
    public IRepositoryObject CreateRepositoryObject(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        string path = backupObject.ObjectPath;

        if (File.Exists($"{backupObject.ObjectPath}"))
            return new UserFile(new FileInfo(path).Name, () => _userFilestream(path));

        if (Directory.Exists($"{backupObject.ObjectPath}"))
            return new UserDirectory(new DirectoryInfo(path).Name, () => _userDirectoryStream(path));

        throw new Exception();
    }

    public Stream OpenFile(string path)
    {
        ValidatePath(path);
        return File.Open(path, FileMode.OpenOrCreate);
    }

    public List<IRepositoryObject> OpenDir(string path)
    {
        ValidatePath(path);
        var directories = new List<IRepositoryObject>();
        var files = new List<IRepositoryObject>();

        foreach (string directory in Directory.GetDirectories(path))
        {
            string fullpath = $"{directory}";
            directories.Add(new UserDirectory(directory, () => _userDirectoryStream(fullpath)));
        }

        foreach (string file in Directory.GetFiles(path))
        {
            string fullpath = $"{file}";
            files.Add(new UserFile(file, () => _userFilestream(fullpath)));
        }

        return files.Union(directories).ToList();
    }

    protected void ValidatePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new Exception();
    }
}