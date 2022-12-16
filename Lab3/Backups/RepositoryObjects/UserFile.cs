using Backups.RepositoryObjects.Interfaces;
using Backups.Visitor.Interfaces;

namespace Backups.RepositoryObjects;

public class UserFile : IFile
{
    private readonly Func<Stream> _userStream;
    public UserFile(string name, Func<Stream> stream)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception();
        ArgumentNullException.ThrowIfNull(stream);

        Name = name;
        _userStream = stream;
    }

    public string Name { get; }
    public Stream InvokeStream() => _userStream.Invoke();

    public void Accept(IZipArchiveVisitor visitor)
    {
        visitor.VisitFile(this);
    }
}