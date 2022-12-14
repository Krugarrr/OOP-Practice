using Backups.Visitor;
using Backups.Visitor.Interface;

namespace Backups.RepositoryObjects.Interface;

internal interface IFile : IRepositoryObject
{
    public void Accept(IRepositoryObjectVisitor visitor);
}