using Backups.RepositoryObjects.Interface;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly IReadOnlyList<BackupObject> _backupObjects;

    public RestorePoint(IReadOnlyList<BackupObject> backupObjects, DateTime date, IStorage storage)
    {
        _backupObjects = BackupObjects;
        Date = date;
        Storage = storage;
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public DateTime Date { get; }
    public IStorage Storage { get; }
}