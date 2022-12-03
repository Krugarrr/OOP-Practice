using System.Globalization;
using System.Reactive.Subjects;
using Banks.BankEntity;

namespace Banks;

public class TimeManager
{
    private const int DaysInYear = 365;
    private DateTime currentTime;

    public TimeManager()
    {
        currentTime = default;
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
        int days = currentTime.Day +
                   (currentTime.Month * DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                   + (currentTime.Year * DaysInYear);

        currentTime = default;
        return days;
    }
}