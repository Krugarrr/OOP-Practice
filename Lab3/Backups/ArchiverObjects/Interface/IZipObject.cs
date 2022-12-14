using System.IO.Compression;
using Backups.RepositoryObjects.Interface;

namespace Backups.ArchiverObjects.Interface;

public interface IZipObject
{
    string Name { get; }
    public IRepositoryObject CreateReppsitoryObject(ZipArchiveEntry archiveEntry);
}