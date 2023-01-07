using Backups.Algorithm;
using Backups.Archiver;
using Backups.Entities;
using Backups.Extra.LimitAlgorithms;
using Backups.Extra.Logger;
using Backups.Extra.Merge;
using Backups.Extra.Recover;
using Backups.Extra.Serialization;
using Backups.Repository;

namespace Backups.Extra;

public class Program
{
    public static void Main()
    {
        var backup = new Backup();
        var logger = new ConsoleLogger();
        var algorithm = new SplitStorageAlgorithm();
        var selector = new AmountLimitAlgorithm(2);
        var archiver = new ZipArchiver();
        string archivePath = $@"arch3";
        var repository = new FileSystemRepository(@"C:\Users\Arthur\Desktop\pipi\");
        var backupExtra = new BackupExtra(
            backup,
            new MergeRestorePoint(new MergeAlgorithm(algorithm, archiver, repository, archivePath)),
            logger,
            new AmountLimitAlgorithm(2));

        var backupObjects = new List<BackupObject>();
        var firstBackupObject = new BackupObject(repository, @"bo\");
        var secondBackupObject = new BackupObject(repository, @"bo2\");
        backupObjects.Add(firstBackupObject);
        backupObjects.Add(secondBackupObject);
        var task = new BackupTask(repository, archiver, backupObjects, algorithm, backup);
        var taskExtra = new BackupTaskExtra(backupExtra, task);

        taskExtra.CreateRestorePoint(@"arch3");
        var serializer = new Serializer(@"C:\Users\Arthur\Desktop\pipi\task.json");
        serializer.Serialize(taskExtra);
        BackupTaskExtra a = serializer.Deserialize();
        var recover = new RecoverToOriginal();

        recover.Execute(taskExtra.Backup.Backup.BackupHistory.First());
    }
}