using System;

namespace ATMSystem {
    public class Account {
        public delegate void AccountStateHandler(string message);

        public event AccountStateHandler Withdrawn;
        public event AccountStateHandler Added;

        public decimal Balance { get; private set; }

        public string CardNumber { get; set; }
        public string PIN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Account(string first, string last, string card, string pin, decimal balance) {
            FirstName = first;
            LastName = last;
            CardNumber = card;
            PIN = pin;
            Balance = balance;
        }

        public void Put(decimal sum) {
            Balance += sum;
            Added?.Invoke($"На рахунок поступило {sum} грн. Новий баланс: {Balance} грн");
        }

        public void Withdraw(decimal sum) {
            if (sum <= Balance) {
                Balance -= sum;
                Withdrawn?.Invoke($"Знято {sum} грн. Залишок: {Balance} грн");
            }
            else {
                Withdrawn?.Invoke("Недостатньо коштів на рахунку");
            }
        }

        public void TransferTo(Account receiver, decimal amount) {
            if (Balance < amount) {
                Withdrawn?.Invoke("Недостатньо коштів для переказу.");
                return;
            }

            Balance -= amount;
            receiver.Balance += amount;

            Withdrawn?.Invoke($"Переказано {amount} грн на {receiver.CardNumber}. Новий баланс: {Balance} грн");
            receiver.Added?.Invoke($"Отримано {amount} грн з карти {CardNumber}. Новий баланс: {receiver.Balance} грн");
        }
    }
}