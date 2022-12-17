using Backups.RepositoryObjects;
using Backups.RepositoryObjects.Interfaces;

namespace Backups.ArchiveEntities;

public class ZipFile : IZipObject
{
    public ZipFile(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IRepositoryObject Convert()
    {
        return new UserFile(this.Name, () => null);
    }
}