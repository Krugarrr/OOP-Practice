using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using Backups.Entities;
using Backups.RepositoryObjects;
using Backups.RepositoryObjects.Interface;
using Directory = System.IO.Directory;
using File = System.IO.File;

namespace Backups.Repository;

public class FSRepository : IRepository
{
    private Func<string, Stream> userFilestream;
    private Func<string, List<IRepositoryObject>> userDirstream;

    public FSRepository(string repoPath)
    {
        // провекра на Path.GetDirectorySeparatorChar на конце
        RepoPath = repoPath;
        userFilestream = OpenFile;
        userDirstream = OpenDir;
    }

    public string RepoPath { get; }
    public IRepositoryObject CreateRepositoryObject(BackupObject backupObject)
    {
        string fullpath = $"{RepoPath}{backupObject.ObjectPath}";
        if (File.Exists($"{RepoPath}{backupObject.ObjectPath}"))
        {
            return new UserFile(new FileInfo(fullpath).Name, () => userFilestream(fullpath));
        }

        if (Directory.Exists($"{RepoPath}{backupObject.ObjectPath}"))
        {
            return new UserDirectory(new DirectoryInfo(fullpath).Name, () => userDirstream(fullpath));
        }

        throw new Exception();
    }

    public Stream OpenFile(string path)
    {
        return File.Open(path, FileMode.Open);
    }

    public List<IRepositoryObject> OpenDir(string path)
    {
        List<IRepositoryObject> dirs = new List<IRepositoryObject>();
        List<IRepositoryObject> files = new List<IRepositoryObject>();
        foreach (var d in Directory.GetDirectories(path))
        {
            string fullpath = $"{path}{d}";
            dirs.Add(new UserDirectory(d, () => userDirstream($"{path}{d}")));
        }

        foreach (var f in Directory.GetFiles(path))
        {
           files.Add(new UserFile(f, () => userFilestream($"{path}{f}")));
        }

        return files.Union(dirs).ToList();
    }
}