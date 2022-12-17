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
        var algorithm = new SplitStorageAlgorithm();
        var archiver = new ZipArchiver();
        var repository = new FileSystemRepository(@"C:\Users\Arthur\Desktop\repo");
        var backupObjects = new List<BackupObject>();
        var firstBackupObject = new BackupObject(repository, @"C:\Users\Arthur\Desktop\bo\");
        var secondBackupObject = new BackupObject(repository, @"C:\Users\Arthur\Desktop\bo2\");
        backupObjects.Add(firstBackupObject);
        backupObjects.Add(secondBackupObject);
        var task = new BackupTask(repository, archiver, backupObjects, algorithm, backup);
        task.CreateRestorePoint(@"C:\Users\Arthur\Desktop\arch3");
    }
}