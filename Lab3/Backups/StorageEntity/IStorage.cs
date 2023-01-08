using Backups.RepositoryObjects.Interfaces;

namespace Backups.StorageEntity;

public interface IStorage
{
    public IReadOnlyList<IRepositoryObject> GetObjects();
    public void Suicide();
}