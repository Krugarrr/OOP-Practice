using System.Reactive.Subjects;
using Banks.Accounts;
using Banks.BankEntity;
using Banks.CentralBank;
using Banks.ClientEntity;
using Banks.ClientEntity.Interfaces;
using Xunit;
namespace Banks.Test;
public class BanksTest
{
        private IAddressBuilder _addressBuilder = new AddressBuilder();
        private TimeManager timeManager = new TimeManager();
        private CentralBankSingleton centralBank;
        private Address address;
        private ClientBuilder _clientBuilder = new ClientBuilder();
        private BankConfigurationBuilder _bankConfigurationBuilder = new BankConfigurationBuilder();
        private Client _client;
        private BankConfiguration _configuration;
        private BankConfiguration _newConfiguration;
        private Client _suss;

        public BanksTest()
        {
            centralBank = CentralBankSingleton.GetInstance(timeManager);

            address = _addressBuilder
                .WithStreet("Кронва")
                .WithCity("Kukuevo")
                .WithHouse(147)
                .Build();

            _suss = _clientBuilder
                .WithName("Амогус")
                .WithSurname("ВнатуреСус").Build();

            _client = _clientBuilder
                .WithName("Амогус")
                .WithSurname("Сус")
                .WithAddress(address)
                .WithDocument(123123).Build();

            var dpir = DepositInterestRate.Builder
                .AddInterestPoint(new DepositInterestPoint(3, 10, 5))
                .AddMaxRate(4)
                .Build();

            _configuration = _bankConfigurationBuilder
                .WithCreditLimit(3)
                .WithCreditComission(3)
                .WithTransactionLimit(1000000)
                .WithDebitInterestRate(3)
                .WithDepositInterestRate(dpir)
                .Build();

            _newConfiguration = _bankConfigurationBuilder
                .WithCreditLimit(4)
                .WithCreditComission(3)
                .WithTransactionLimit(100000)
                .WithDebitInterestRate(3)
                .WithDepositInterestRate(dpir)
                .Build();
        }

        [Fact]
        public void BaseOperations()
        {
            Bank bank = centralBank.AddBank("СперБанк", _configuration);
            Bank anotherBank = centralBank.AddBank("МакаревичМани", _configuration);
            bank.CreateDebitAccount(_client);
            anotherBank.CreateDebitAccount(_client);

            var account = bank.GetAccount(0);
            account.AddMoney(50000);
            account.TakeMoney(4000);
            account.Cancel(1);
            Assert.Equal(50000, account.GetBalance());

            account.TransferMoney(4000, 0, anotherBank);
            Assert.Equal(4000, anotherBank.GetAccount(0).GetBalance());
        }

        [Fact]
        public void SussyClients_CantTakeEnough()
        {
            Bank bank = centralBank.AddBank("СперБанк", _configuration);
            bank.CreateDebitAccount(_suss);
            bank.ChangeConfiguration(_newConfiguration);
            bank.Accounts.Last().AddMoney(500000000);
            var account = bank.GetAccount(0);
            Assert.Throws<Exception>(() => account.TakeMoney(2000000));
        }

        [Fact]
        public void TimeMashineDoStuff()
        {
            Bank bank = centralBank.AddBank("СперБанк", _configuration);
            bank.CreateDebitAccount(_client);
            var account = bank.GetAccount(0);
            account.AddMoney(50000);
            centralBank.TimeManager.AddMonth();
            centralBank.TimeManager.AddDay();
            centralBank.Fundraising();
            Assert.True(account.GetBalance() > 50000);
        }
}