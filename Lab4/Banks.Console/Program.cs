using System.Reactive.Subjects;
using Banks;
using Banks.Accounts;
using Banks.BankEntity;
using Banks.CentralBank;
using Banks.ClientEntity;

AddressBuilder addressBuilder = new AddressBuilder();
TimeManager timeManager = new TimeManager();
CentralBankSingleton centralBank;
Address address;
ClientBuilder clientBuilder = new ClientBuilder();
BankConfigurationBuilder bankConfigurationBuilder = new BankConfigurationBuilder();

centralBank = CentralBankSingleton.GetInstance(timeManager);
address = addressBuilder
    .WithStreet("Кронва")
    .WithCity("Kukuevo")
    .WithHouse(147)
    .Build();
Client client = clientBuilder
    .WithName("Амогус")
    .WithSurname("Сус").Build();
var dpir = DepositInterestRate.Builder
    .AddInterestPoint(new DepositInterestPoint(3, 10, 5))
    .AddMaxRate(4)
    .Build();
BankConfiguration configuration = bankConfigurationBuilder
    .WithCreditLimit(3)
    .WithCreditComission(3)
    .WithTransactionLimit(1000000)
    .WithDebitInterestRate(3)
    .WithDepositInterestRate(dpir)
    .Build();

BankConfiguration newConfiguration = bankConfigurationBuilder
    .WithCreditLimit(4)
    .WithCreditComission(3)
    .WithTransactionLimit(1000000)
    .WithDebitInterestRate(3)
    .WithDepositInterestRate(dpir)
    .Build();

Bank bank = centralBank.AddBank("СперБанк", configuration);
bank.CreateDebitAccount(client);
var account = new DebitAccount(new Account(0, configuration, client));
bank.ChangeConfiguration(newConfiguration);
bank.Accounts.Last().AddMoney(300);
centralBank.TimeManager.AddDay();
centralBank.TimeManager.UpdateTime();