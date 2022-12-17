using Backups.Exceptions;
using Backups.RepositoryObjects.Interfaces;
using Backups.Visitor.Interfaces;

namespace Backups.RepositoryObjects;

public class UserFile : IFile
{
    private readonly Func<Stream> _userStream;
    public UserFile(string name, Func<Stream> stream)
    {
        if (string.IsNullOrWhiteSpace(name))
            PathException.PathIsNullOrEmptyException();
        ArgumentNullException.ThrowIfNull(stream);

        Name = name;
        _userStream = stream;
    }

    public string Name { get; }
    public Stream GetContents() => _userStream.Invoke();

    public void Accept(IZipArchiveVisitor visitor)
    {
        visitor.VisitFile(this);
    }
}