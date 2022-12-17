using Backups.RepositoryObjects.Interfaces;

namespace Backups.ArchiveEntities;

public interface IZipObject
{
    string Name { get; }
    IRepositoryObject Convert();
}