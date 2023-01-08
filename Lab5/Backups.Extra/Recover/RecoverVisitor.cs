using Backups.Repository;
using Backups.RepositoryObjects.Interfaces;
using Backups.Visitor.Interfaces;
namespace Backups.Extra.Recover;

public class RecoverVisitor : IZipArchiveVisitor
{
    private IRepository _repository;
    private Stack<string> _zipPaths;

    public RecoverVisitor(IRepository repository, string path)
    {
        _repository = repository;
        _zipPaths = new Stack<string>();
        _zipPaths.Push(path);
        if (path.EndsWith("\\"))
        {
            throw new Exception();
        }
    }

    public void VisitFile(IFile userFile)
    {
        ArgumentNullException.ThrowIfNull(userFile);

        string zipPath = _zipPaths.Peek();
        string fullPath = $"{zipPath}{userFile.Name}";

        using Stream repositoryEntryStream = _repository.OpenZipStream(fullPath);
        using Stream userFileStream = userFile.GetContents();
        userFileStream.CopyTo(repositoryEntryStream);
    }

    public void VisitDirectory(IDirectory userDirectory)
    {
        ArgumentNullException.ThrowIfNull(userDirectory);

        string zipPath = _zipPaths.Peek();
        string fullPath = $"{zipPath}{userDirectory.Name}\\";
        _repository.OpenZipDirectory(fullPath);
        _zipPaths.Push(fullPath);

        foreach (IRepositoryObject directory in userDirectory.GetContents())
        {
            directory.Accept(this);
        }

        _zipPaths.Pop();
    }
}