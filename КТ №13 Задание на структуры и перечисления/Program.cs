using System;
using System.Collections.Generic;

namespace КТ__13_Задание_на_структуры_и_перечисления
{

    class Program
    {
        static void Main()
        {
            List<Card> cards = new List<Card>();

            Console.WriteLine("=== Ввод колоды из 5 карт пользователем ===");
            Console.WriteLine($"Доступные масти: {string.Join(", ", Enum.GetNames(typeof(Suit)))}");
            Console.WriteLine($"Доступные ранги: {string.Join(", ", Enum.GetNames(typeof(Rank)))}");
            Console.WriteLine();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"--- Карточка #{i + 1} ---");
                Suit suit = ReadSuitFromUser();
                Rank rank = ReadRankFromUser();

                cards.Add(new Card { Suit = suit, Rank = rank });
            }

            Console.WriteLine("\nКолода успешно сформирована!");
            Console.WriteLine($"cards[0] (до изменения копии): {cards[0]}");
            Console.WriteLine(new string('-', 40));

            Card copiedCard = cards[0];

            Console.WriteLine("Введите новый ранг для изменения копии карты:");
            copiedCard.Rank = ReadRankFromUser();

            Console.WriteLine($"\nКопия после изменения: {copiedCard}");
            Console.WriteLine($"cards[0] в коллекции (остался без изменений): {cards[0]}");
            Console.WriteLine(new string('-', 40));

            Console.WriteLine("=== Демонстрация Enum.TryParse ===");

            string validName = "King";
            if (Enum.TryParse<Rank>(validName, true, out var validRank))
            {
                Console.WriteLine($"Успешно распарсено '{validName}': true, результат == {validRank}");
            }

            string invalidName = "Joker";
            if (Enum.TryParse<Rank>(invalidName, true, out var invalidRank))
            {
                Console.WriteLine($"Распарсено '{invalidName}': true, результат == {invalidRank}");
            }
            else
            {
                Console.WriteLine($"Распарсено '{invalidName}': false (исключение не вызвано, возвращено значение по умолчанию: {invalidRank})");
            }

            Console.WriteLine(new string('-', 40));

            Console.WriteLine("=== Демонстрация Enum.Parse с try-catch ===");
            TestEnumParseWithException("Queen");
            TestEnumParseWithException("SuperJoker");
        }

        static Suit ReadSuitFromUser()
        {
            while (true)
            {
                try
                {
                    Console.Write("Введите масть: ");
                    string? input = Console.ReadLine();

                    if (!string.IsNullOrEmpty(input) && Enum.TryParse<Suit>(input, true, out Suit suit))
                    {
                        return suit;
                    }
                    throw new ArgumentException($"Масть '{input}' не найдена.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Ошибка ввода]: {ex.Message} Попробуйте еще раз.");
                }
            }
        }

        static Rank ReadRankFromUser()
        {
            while (true)
            {
                try
                {
                    Console.Write("Введите ранг: ");
                    string? input = Console.ReadLine();

                    if (!string.IsNullOrEmpty(input) && Enum.TryParse<Rank>(input, true, out Rank rank))
                    {
                        return rank;
                    }
                    throw new FormatException($"Ранг '{input}' введен некорректно.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Ошибка ввода]: {ex.Message} Попробуйте еще раз.");
                }
            }
        }

        static void TestEnumParseWithException(string valueToParse)
        {
            try
            {
                Console.WriteLine($"Пытаемся разобрать через Enum.Parse: \"{valueToParse}\"");
                Rank parsedRank = (Rank)Enum.Parse(typeof(Rank), valueToParse, true);
                Console.WriteLine($"Успех! Результат: {parsedRank}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Перехвачено исключение (ArgumentException): {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Перехвачено непредвиденное исключение: {ex.Message}");
            }
        }
    }
}