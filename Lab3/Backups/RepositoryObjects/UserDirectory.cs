using Backups.Exceptions;
using Backups.RepositoryObjects.Interfaces;
using Backups.Visitor.Interfaces;

namespace Backups.RepositoryObjects;

public class UserDirectory : IDirectory
{
    private readonly Func<List<IRepositoryObject>> _userStream;

    public UserDirectory(string name, Func<List<IRepositoryObject>> stream)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw PathException.PathIsNullOrEmptyException();
        ArgumentNullException.ThrowIfNull(stream);

        Name = name;
        _userStream = stream;
    }

    public string Name { get; }

    public List<IRepositoryObject> InvokeStream() => _userStream.Invoke();

    public void Accept(IZipArchiveVisitor visitor)
    {
        visitor.VisitDirectory(this);
    }
}