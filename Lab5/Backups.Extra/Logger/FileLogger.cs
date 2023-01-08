using System.Net.Mime;
using System.Reflection;

namespace Backups.Extra.Logger;

public class FileLogger : ILogger
{
    public FileLogger(string fullPath)
    {
        FullPath = fullPath;
    }

    public string FullPath { get; }
    public void Log(string message)
    {
        File.WriteAllText(FullPath, message);
    }
}