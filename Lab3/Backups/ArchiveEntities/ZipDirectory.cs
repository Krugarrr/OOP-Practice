using Backups.RepositoryObjects;
using Backups.RepositoryObjects.Interfaces;
using Newtonsoft.Json;

namespace Backups.ArchiveEntities;

public class ZipDirectory : IZipObject
{
    [JsonProperty("zipObjects")]
    private readonly List<IZipObject> _zipObjects;
    public ZipDirectory(string name, List<IZipObject> zipObjects)
    {
        Name = name;
        _zipObjects = zipObjects;
    }

    [JsonProperty("name")]
    public string Name { get; }

    public IRepositoryObject Convert()
    {
        var repositoryObjects = _zipObjects.Select(z => z.Convert()).ToList();
        return new UserDirectory(this.Name, () => repositoryObjects);
    }
}