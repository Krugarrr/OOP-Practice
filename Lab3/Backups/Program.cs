using System.Globalization;
using Backups.Algorithm;
using Backups.Archiver;
using Backups.Entities;
using Backups.Repository;

namespace Backups;

public class Program
{
    public static void Main(string[] args)
    {
        var backup = new Backup();
        var algorithm = new SingleStorageAlgorithm();
        var archiver = new ZipArchiver();
        var repository = new FileSystemRepository(@"C:\Users\Arthur\Desktop\repo");
        var bo = new List<BackupObject>();
        bo.Add(new BackupObject(repository, @"C:\Users\Arthur\Desktop\bo"));
        BackupTask task = new BackupTask(repository, archiver, bo, algorithm, backup);
        task.CreateRestorePoint(@"C:\Users\Arthur\Desktop\arch2");
    }
}