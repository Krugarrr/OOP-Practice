using System.Globalization;
using Backups.Entities;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.Repository;

public interface IRepository
{
    string RepositoryPath { get; }
    IRepositoryObject CreateRepositoryObject(BackupObject backupObject);
    Stream OpenFileStream(string path);
    List<IRepositoryObject> OpenDirectoryStream(string path);
    Stream OpenZipStream(string path);
    void OpenZipDirectory(string path);
}
