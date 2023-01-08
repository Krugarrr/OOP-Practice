using System.IO.Compression;
using Backups.RepositoryObjects;
using Backups.RepositoryObjects.Interfaces;
using Newtonsoft.Json;

namespace Backups.ArchiveEntities;

public class ZipFile : IZipObject
{
    private readonly ZipArchiveEntry _zipStream;
    public ZipFile(string name, ZipArchiveEntry stream)
    {
        Name = name;
        _zipStream = stream;
    }

    [JsonProperty("name")]
    public string Name { get; }

    public IRepositoryObject Convert()
    {
        return new UserFile(this.Name, () => _zipStream.Open());
    }
}