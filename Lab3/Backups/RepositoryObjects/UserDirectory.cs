using Backups.Exceptions;
using Backups.RepositoryObjects.Interfaces;
using Backups.Visitor.Interfaces;
using Newtonsoft.Json;

namespace Backups.RepositoryObjects;

public class UserDirectory : IDirectory
{
    [JsonProperty("stream")]

    private readonly Func<List<IRepositoryObject>> _userStream;

    public UserDirectory(string name, Func<List<IRepositoryObject>> stream)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw PathException.PathIsNullOrEmptyException();
        ArgumentNullException.ThrowIfNull(stream);

        Name = name;
        _userStream = stream;
    }

    [JsonProperty("name")]

    public string Name { get; }

    public IReadOnlyCollection<IRepositoryObject> GetContents() => _userStream.Invoke();

    public void Accept(IZipArchiveVisitor visitor)
    {
        visitor.VisitDirectory(this);
    }
}