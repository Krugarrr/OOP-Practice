using System.Reflection;
using Backups.RepositoryObjects.Interfaces;
using Backups.Visitor;
using Backups.Visitor.Interfaces;

namespace Backups.RepositoryObjects;

public class UserDirectory : IDirectory
{
    private readonly Func<List<IRepositoryObject>> _userStream;

    public UserDirectory(string name, Func<List<IRepositoryObject>> stream)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();
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