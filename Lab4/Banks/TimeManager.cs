namespace Banks;

public class TimeManager
{
    private DateTime currentTime;

    public TimeManager()
    {
        this.currentTime = DateTime.Now;
    }

    public void AddDay()
    {
        currentTime.AddDays(1);
    }

    public void AddDays(int days)
    {
        currentTime.AddDays(days);
    }

    public void AddMonth()
    {
        currentTime.AddMonths(1);
    }

    public void AddMonths(int months)
    {
        currentTime.AddMonths(months);
    }

    public void AddYear()
    {
        currentTime.AddYears(1);
    }

    public void AddYears(int years)
    {
        currentTime.AddYears(years);
    }
}