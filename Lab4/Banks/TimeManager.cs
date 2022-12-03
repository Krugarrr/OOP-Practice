using System.Reactive.Subjects;
using Banks.BankEntity;

namespace Banks;

public class TimeManager
{
    private DateTime currentTime;
    public TimeManager()
    {
       currentTime = default(DateTime);
    }

    public void AddDay()
    {
        currentTime.AddDays(1);
    }

    public void AddMonth()
    {
        currentTime.AddMonths(1);
    }

    public void AddYear()
    {
        currentTime.AddYears(1);
    }

    public int UpdateTime()
    {
        int days = currentTime.Day;
        currentTime = default(DateTime);
        return days;
    }
}