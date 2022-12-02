namespace Banks.CentralBank;

public interface ICentralBank
{
    public Bank AddBank(string bankName, BankConfiguration configuration);

    // public void DeleteBank(string bankName);
}