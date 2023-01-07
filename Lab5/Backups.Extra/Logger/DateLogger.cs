namespace Backups.Extra.Logger;

public class DateLogger : ILogger
{
    private ILogger _logger;

    public DateLogger(ILogger logger)
    {
        _logger = logger;
    }

    public void Log(string message)
    {
        _logger.Log($"{DateTime.Now} : {message}");
    }
}