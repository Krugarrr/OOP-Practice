using Backups.StorageEntity;
using Newtonsoft.Json;

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

    [JsonProperty("backupObjects")]
    public IReadOnlyList<BackupObject> BackupObjects { get; }

    [JsonProperty("date")]

    public DateTime Date { get; }

    [JsonProperty("storage")]
    public IStorage Storage { get; }
}