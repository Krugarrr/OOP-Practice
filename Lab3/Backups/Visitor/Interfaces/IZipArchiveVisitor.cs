using Backups.RepositoryObjects.Interfaces;

namespace Backups.Visitor.Interfaces;

public interface IZipArchiveVisitor
{
    public void VisitFile(IFile userFile);
    public void VisitDirectory(IDirectory userDirectory);
}