using System.Reflection;
using Backups.RepositoryObjects.Interface;
using Backups.Visitor;
using Backups.Visitor.Interface;
using IRepositoryObjectVisitor = Backups.Visitor.Interface.IRepositoryObjectVisitor;

namespace Backups.RepositoryObjects;

public class UserDirectory : IDirectory
{
    private Func<List<IRepositoryObject>> userStream;

    public UserDirectory(string name, Func<List<IRepositoryObject>> stream)
    {
        Name = name;
        userStream = stream;
    }

    public string Name { get; }

    public List<IRepositoryObject> Aboba()
    {
        return userStream.Invoke();
    }

    public void Accept(IRepositoryObjectVisitor visitor)
    {
        visitor.VisitDirectory(this);
    }
}