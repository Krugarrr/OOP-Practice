using Backups.Visitor.Interfaces;

namespace Backups.RepositoryObjects.Interfaces;

public interface IRepositoryObject
{
    string Name { get; }
    void Accept(IZipArchiveVisitor visitor);
}