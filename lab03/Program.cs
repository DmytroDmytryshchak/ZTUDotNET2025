using System;

namespace StringExtensions {
    public static class StringExtensions {
        public static string ReverseString(this string stringExample) {
            if (string.IsNullOrEmpty(stringExample)) {
                return stringExample;
            }

            char[] array = stringExample.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        public static int CountChar(this string stringExample, char symbol) {
            if (string.IsNullOrEmpty(stringExample)) {
                return 0;
            }

            int count = 0;

            foreach (char c in stringExample) {
                if (c == symbol) {
                    count++;
                }
            }

            return count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tDmytro Dmytryshchak  ZIPZ-24-1  lab03  .NET programming\n");

            string stringExample = "This is a random string for ReverseString and CountChar methods.";

            string reversedString = stringExample.ReverseString();
            Console.WriteLine($"STRING: {stringExample}\n");
            Console.WriteLine($"REVERSED STRING: {reversedString}\n");

            char symbol = 'r';
            int count = stringExample.CountChar(symbol);
            Console.WriteLine($"CHAR/SYMBOL '{symbol}' => QUANTITY IN OUR STRING: {count}");
        }
    }
}