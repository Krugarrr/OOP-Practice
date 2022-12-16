namespace Backups.Entities;

public class RestorePoint
{
    public RestorePoint(IReadOnlyList<BackupObject> backupObjects, DateTime date, IStorage storage)
    {
        ArgumentNullException.ThrowIfNull(backupObjects);
        ArgumentNullException.ThrowIfNull(date);
        ArgumentNullException.ThrowIfNull(storage);

        BackupObjects = backupObjects;
        Date = date;
        Storage = storage;
    }

    public IReadOnlyList<BackupObject> BackupObjects { get; }

    public DateTime Date { get; }
    public IStorage Storage { get; }
}