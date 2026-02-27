using System;

namespace ATMSystem {
    public class AutomatedTellerMachine {
        public string ATMId { get; set; }
        public string Address { get; set; }
        private Bank bank;

        public delegate void AccountStateHandler(string message);

        public event AccountStateHandler AuthSuccess;
        public event AccountStateHandler BalanceViewed;
        public event AccountStateHandler Withdrawn;
        public event AccountStateHandler Added;
        public event AccountStateHandler TransferCompleted;

        public AutomatedTellerMachine(string id, string address, Bank bank) {
            ATMId = id;
            Address = address;
            this.bank = bank;
        }

        public Account Authenticate(string cardNumber, string pin) {
            var acc = bank.GetAccountByCard(cardNumber);

            if (acc != null && acc.PIN == pin) {
                AuthSuccess?.Invoke("Аутентифікація успішна!");
                return acc;
            }

            return null;
        }

        public void ViewBalance(Account acc) {
            BalanceViewed?.Invoke($"Баланс: {acc.Balance} грн");
        }

        public void Withdraw(Account acc, decimal amount) {
            if (amount <= 0) {
                Withdrawn?.Invoke("Сума повинна бути більшою за нуль.");
                return;
            }

            acc.Withdraw(amount);
        }

        public void Deposit(Account acc, decimal amount) {
            if (amount <= 0) {
                Added?.Invoke("Сума повинна бути більшою за нуль.");
                return;
            }

            acc.Put(amount);
        }

        public void Transfer(Account fromAcc, string toCard, decimal amount) {
            if (toCard == fromAcc.CardNumber) {
                TransferCompleted?.Invoke("Не можна переказати самому собі.");
                return;
            }

            var to = bank.GetAccountByCard(toCard);
            if (to == null) {
                TransferCompleted?.Invoke("Картку отримувача не знайдено.");
                return;
            }

            if (amount <= 0) {
                TransferCompleted?.Invoke("Сума має бути більшою за нуль.");
                return;
            }

            if (amount > fromAcc.Balance) {
                TransferCompleted?.Invoke($"Недостатньо коштів. Баланс: {fromAcc.Balance} грн");
                return;
            }

            fromAcc.TransferTo(to, amount);

            TransferCompleted?.Invoke(
                $"Переказано {amount} грн на {toCard}. Баланс: {fromAcc.Balance} грн");
        }
    }
}