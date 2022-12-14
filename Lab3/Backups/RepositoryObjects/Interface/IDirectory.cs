using Backups.RepositoryObjects;
using Backups.RepositoryObjects.Interface;
using Backups.Visitor;
using Backups.Visitor.Interface;

internal interface IDirectory : IRepositoryObject
{
    public void Accept(IRepositoryObjectVisitor visitor);
}