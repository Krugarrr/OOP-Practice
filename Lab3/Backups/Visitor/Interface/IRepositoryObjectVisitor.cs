using Backups.RepositoryObjects;

namespace Backups.Visitor.Interface;

public interface IRepositoryObjectVisitor
{
    public void VisitFile(UserFile userFile);
    public void VisitDirectory(UserDirectory userDir);
}