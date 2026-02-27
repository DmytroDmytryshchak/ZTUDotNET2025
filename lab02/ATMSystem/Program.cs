using System;
using System.Runtime.InteropServices;

namespace ATMSystem {
    class Program {
        static Bank bank;
        static AutomatedTellerMachine atm;

        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleOutputCP(uint wCodePageID);
        [DllImport("kernel32.dll")]
        public static extern bool SetConsoleCP(uint wCodePageID);

        static void Main(string[] args) {
            Console.Title = "ATM SYSTEM Dmytro Dmytryshchak  ZIPZ-24-1  lab02 .NET";

            SetConsoleCP(65001);
            SetConsoleOutputCP(65001);

            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bank = new Bank("Monobank");

            bank.AddAccount(new Account("Dmytro", "Dmytryshchak", "1111 2222 3333 4444", "1234", 5000));
            bank.AddAccount(new Account("Ivan", "Ivanenko", "5555 6666 7777 8888", "1234", 10000));

            atm = new AutomatedTellerMachine("ATM-01", "Kyiv, Main Street 10", bank);

            atm.AuthSuccess += msg => ShowCenteredMessage(msg);
            atm.BalanceViewed += msg => ShowCenteredMessage(msg);
            atm.Withdrawn += msg => ShowCenteredMessage(msg);
            atm.Added += msg => ShowCenteredMessage(msg);
            atm.TransferCompleted += msg => ShowCenteredMessage(msg);

            StartScreen();
        }

        static void StartScreen() {
            while (true) {
                Console.Clear();
                DrawFrame();
                CenterText(7, "ВАС ВІТАЄ ATM MONOBANK");
                CenterText(8, "======================");
                CenterText(10, "ВСТАВТЕ КАРТКУ");
                CenterText(12, "(натисніть Enter)");
                Console.ReadLine();
                Login();
            }
        }

        static void Login() {
            Console.Clear();
            DrawFrame();
            CenterText(7, "ВВЕДІТЬ НОМЕР КАРТКИ (**** **** **** ****):");
            string card = ReadCardNumberCentered(10);

            CenterText(13, "ВВЕДІТЬ PIN:");
            string pin = ReadHiddenCentered(15);

            var account = atm.Authenticate(card, pin);
            if (account == null) {
                ShowCenteredMessage("НЕВІРНА КАРТКА АБО PIN");
                return;
            }

            account.Withdrawn += msg => ShowCenteredMessage(msg);
            account.Added += msg => ShowCenteredMessage(msg);

            MainMenu(account);
        }

        static void MainMenu(Account acc) {
            while (true) {
                Console.Clear();
                DrawFrame();
                PrintHeader(acc);
                PrintMenu();

                Console.SetCursorPosition(8, 18);
                Console.Write("Ваш вибір (1–5): ");
                string choice = Console.ReadLine();

                switch (choice) {
                    case "1":
                        OperationScreen(acc, () => atm.ViewBalance(acc));
                        break;

                    case "2":
                        WithdrawScreen(acc);
                        break;

                    case "3":
                        DepositScreen(acc);
                        break;

                    case "4":
                        TransferScreen(acc);
                        break;

                    case "5":
                        return;

                    default:
                        OperationScreen(acc, () => ShowCenteredMessage("Невірний вибір!"));
                        break;
                }
            }
        }

        static void WithdrawScreen(Account acc) {
            Console.Clear();
            DrawFrame();
            PrintHeader(acc);
            CenterText(7, "ВВЕДІТЬ СУМУ ЗНЯТТЯ:");

            decimal sum;
            while (!decimal.TryParse(ReadCenteredInput(10, 10), out sum) || sum <= 0)
            {
                ShowCenteredMessage("Введіть коректну позитивну суму!");
                CenterText(7, "ВВЕДІТЬ СУМУ ЗНЯТТЯ:");
            }

            OperationScreen(acc, () => atm.Withdraw(acc, sum));
        }

        static void DepositScreen(Account acc) {
            Console.Clear();
            DrawFrame();
            PrintHeader(acc);
            CenterText(7, "ВВЕДІТЬ СУМУ ПОПОВНЕННЯ:");

            decimal sum;
            while (!decimal.TryParse(ReadCenteredInput(10, 10), out sum) || sum <= 0)
            {
                ShowCenteredMessage("Введіть коректну позитивну суму!");
                CenterText(7, "ВВЕДІТЬ СУМУ ПОПОВНЕННЯ:");
            }

            OperationScreen(acc, () => atm.Deposit(acc, sum));
        }

        static void TransferScreen(Account acc) {
            Console.Clear();
            DrawFrame();
            PrintHeader(acc);

            CenterText(7, "НОМЕР КАРТКИ ОТРИМУВАЧА (**** **** **** ****):");
            string card = ReadCardNumberCentered(10);

            CenterText(13, "СУМА ПЕРЕКАЗУ:");

            decimal sum;
            while (!decimal.TryParse(ReadCenteredInput(15, 10), out sum) || sum <= 0)
            {
                ShowCenteredMessage("Введіть коректну позитивну суму!");
                CenterText(13, "СУМА ПЕРЕКАЗУ:");
            }

            OperationScreen(acc, () => atm.Transfer(acc, card, sum));
        }

        static void OperationScreen(Account acc, Action operation) {
            Console.Clear();
            DrawFrame();
            PrintHeader(acc);

            operation();

            CenterText(20, "Натисніть Enter, щоб повернутися до меню...");
            Console.ReadLine();
        }

        static void DrawFrame() {
            int width = 60, height = 22;
            Console.SetCursorPosition(0, 0);
            Console.Write("/" + new string('-', width) + "\\");
            for (int i = 1; i <= height; i++) {
                Console.SetCursorPosition(0, i);
                Console.Write("|" + new string(' ', width) + "|");
            }
            Console.SetCursorPosition(0, height + 1);
            Console.Write("\\" + new string('-', width) + "/");
        }

        static void PrintHeader(Account acc) {
            string nameLine = $" Користувач: {acc.FirstName} {acc.LastName} ";
            int x = (60 - nameLine.Length) / 2 + 1;
            Console.SetCursorPosition(x, 2);
            Console.Write(nameLine);

            Console.SetCursorPosition(1, 3);
            Console.Write(new string('=', 60));
        }

        static void PrintMenu() {
            WriteMenuItem(8, "> ПЕРЕГЛЯНУТИ БАЛАНС (1)");
            WriteMenuItem(9, "> ЗНЯТИ ГОТІВКУ (2)");
            WriteMenuItem(10, "> ПОПОВНИТИ РАХУНОК (3)");
            WriteMenuItem(11, "> ПЕРЕКАЗ КОШТІВ (4)");
            WriteMenuItem(12, "> ВИХІД (5)");
        }

        static void WriteMenuItem(int y, string text) {
            Console.SetCursorPosition(4, y);
            Console.Write(text);
        }

        static void ShowCenteredMessage(string msg) {
            string[] lines = SplitMessage(msg, 58);
            int y = 6;
            foreach (string line in lines) {
                CenterText(y++, line);
            }
        }

        static string[] SplitMessage(string msg, int max) {
            var words = msg.Split(' ');
            var lines = new System.Collections.Generic.List<string>();
            string cur = "";

            foreach (var w in words) {
                if ((cur + " " + w).Trim().Length > max) {
                    lines.Add(cur);
                    cur = w;
                }
                else {
                    cur = (cur + " " + w).Trim();
                }
            }
            if (cur != "") { 
                lines.Add(cur); 
            }
            return lines.ToArray();
        }

        static void CenterText(int y, string text) {
            int x = (60 - text.Length) / 2 + 1;
            if (x < 0) { 
                x = 0;
            }
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        static string ReadHiddenCentered(int y) {
            string input = "";
            int x = (60 - 4) / 2;
            Console.SetCursorPosition(x, y);

            ConsoleKey key;
            do {
                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && input.Length > 0) {
                    input = input[..^1];
                    Console.SetCursorPosition(Console.CursorLeft - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, y);
                }
                else if (char.IsDigit(keyInfo.KeyChar) && input.Length < 4) {
                    input += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (key != ConsoleKey.Enter);

            return input;
        }

        static string ReadCardNumberCentered(int y) {
            string input = "";
            int maxLength = 19;
            int x = (60 - maxLength) / 2;

            Console.SetCursorPosition(x, y);

            ConsoleKey key;
            do {
                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && input.Length > 0) {
                    input = input[..^1];
                    Console.SetCursorPosition(Console.CursorLeft - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, y);
                }
                else if (!char.IsControl(keyInfo.KeyChar) && input.Length < maxLength) {
                    input += keyInfo.KeyChar;
                    Console.Write(keyInfo.KeyChar);
                }
            } while (key != ConsoleKey.Enter);

            return input;
        }

        static string ReadCenteredInput(int y, int maxLength) {
            int x = (60 - maxLength) / 2;
            Console.SetCursorPosition(x, y);
            return Console.ReadLine();
        }
    }
}