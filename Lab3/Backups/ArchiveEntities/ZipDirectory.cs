using Backups.RepositoryObjects;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.ArchiveEntities;

public class ZipDirectory : IZipObject
{
    private readonly List<IZipObject> _objects;
    public ZipDirectory(string name, List<IZipObject> zipObjects)
    {
        Name = name;
        _objects = zipObjects;
    }

    public string Name { get; }

    public IRepositoryObject Convert()
    {
        return new UserDirectory(this.Name, () => null);
    }
}