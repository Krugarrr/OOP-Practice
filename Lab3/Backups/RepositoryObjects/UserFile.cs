using Backups.RepositoryObjects.Interface;
using Backups.Visitor;
using Backups.Visitor.Interface;
using IRepositoryObjectVisitor = Backups.Visitor.Interface.IRepositoryObjectVisitor;

namespace Backups.RepositoryObjects;

public class UserFile : IFile
{
    private Func<Stream> userStream;
    public UserFile(string name, Func<Stream> stream)
    {
        Name = name;
        userStream = stream;
    }

    public string Name { get; }
    public Stream Abobus()
    {
        return userStream.Invoke();
    }

    public void Accept(IRepositoryObjectVisitor visitor)
    {
        visitor.VisitFile(this);
    }
}