using System.IO.Compression;
using Backups.ArchiverObjects.Interface;
using Backups.RepositoryObjects.Interface;

namespace Backups.ArchiverObjects;

public class ZipFile : IZipObject
{
    public ZipFile(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public IRepositoryObject CreateReppsitoryObject(ZipArchiveEntry archiveEntry)
    {
        throw new NotImplementedException();
    }
}