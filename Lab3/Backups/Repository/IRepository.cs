using System.Globalization;
using Backups.Entities;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.Repository;

public interface IRepository
{
    string RepositoryPath { get; }
    IRepositoryObject CreateRepositoryObject(BackupObject backupObject);
    Stream OpenFile(string path);
}
