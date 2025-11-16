using System.Collections.Generic;
using System.Linq;

namespace ATMSystem {
    public class Bank {
        public string Name { get; set; }
        private List<Account> accounts = new();

        public Bank(string name) {
            Name = name;
        }

        public void AddAccount(Account acc) => accounts.Add(acc);

        public Account GetAccountByCard(string cardNumber) {
            return accounts.FirstOrDefault(a => a.CardNumber == cardNumber);
        }

        public List<Account> GetAllAccounts() => accounts;
    }
}