using System.Reactive.Subjects;
using Banks.BankEntity;

namespace Banks.CentralBank;

public sealed class CentralBankSingleton : ICentralBank
{
    private static CentralBankSingleton _instance;
    private readonly List<IBank> _banks;
    private ISubject<TimeManager> subject = new Subject<TimeManager>();

    private CentralBankSingleton(TimeManager timeManager)
    {
        TimeManager = timeManager;
        _banks = new List<IBank>();
        subject.Subscribe(t => t.UpdateTime());
    }

    public TimeManager TimeManager { get; }

    public static CentralBankSingleton GetInstance(TimeManager timeManager)
    {
        if (_instance is null)
        {
            _instance = new CentralBankSingleton(timeManager);
        }

        return _instance;
    }

    public Bank AddBank(string bankName, BankConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(bankName);
        var newBank = new Bank(bankName, configuration);
        _banks.Add(newBank);
        return newBank;
    }

    public void Fundraising()
    {
        foreach (IBank bank in _banks)
        {
            bank.Fundraising(TimeManager.UpdateTime());
        }
    }
}