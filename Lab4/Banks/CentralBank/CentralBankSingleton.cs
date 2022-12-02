namespace Banks.CentralBank;

public sealed class CentralBankSingleton : ICentralBank
{
    private static CentralBankSingleton _instance;
    private readonly List<IBank> _banks;

    private CentralBankSingleton(TimeManager timeManager)
    {
        TimeManager = timeManager;
        _banks = new List<IBank>();
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

    // business logic
    public Bank AddBank(string bankName, BankConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(bankName);
        var newBank = new Bank(bankName, configuration);
        _banks.Add(newBank);
        return newBank;
    }
}