using System.Data;
using Backups.Visitor;
using Backups.Visitor.Interface;

namespace Backups.RepositoryObjects.Interface;

public interface IRepositoryObject
{
    public string Name { get; }
}