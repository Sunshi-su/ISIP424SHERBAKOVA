using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace Praktica
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            
            Memory memory = new Memory();
            bool k = true;
            while (k)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Ввести текст и показать статистику");
                Console.WriteLine("2. Показать статистику прошлых текстов");
                Console.WriteLine("0. Выход");

                string menu = Console.ReadLine();

                if (!int.TryParse(menu, out int choice))
                {
                    Console.WriteLine("Нужно ввести число!");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите текст:");

                        string text = Console.ReadLine();

                        if (text.Length < 100)
                        {
                            Console.WriteLine("Текст должен быть минимум 100 символов.");
                            continue;
                        }
                        
                        TextStatistics statistics = new TextStatistics();
                        statistics.Text = text;
                        statistics.Print();
                        memory.Add(statistics);

                        break;

                    case 2:
                        memory.ShowText();
                        break;

                    case 0:
                        k = false;
                        break;

                    default:
                        Console.WriteLine("Неверный пункт меню");
                        break;
                }
            }
        }

        
        class TextStatistics
        {
            public string Text { get; set; }
            
            public string[] Words
            {
                get
                {
                    return Text.Split(new char[] { ' ', ',', ':', ';','-','—', '.', '!', '?'}, StringSplitOptions.RemoveEmptyEntries);
                }
                
            }

            public int WordCount
            {
                get
                {
                    return  Words.Length;
                }
            }

            public string ShortWord {
                get
                {
                    int min = Words[0].Length;
                    string shorts = null;
                    foreach (var slovo in Words)
                    {
                        if (slovo.Length < min)
                        {
                            min = slovo.Length;
                            shorts = slovo;
                        }
                        
                    }
                    return shorts;
                } 
            }

            public string LongWord
            {
                get
                {
                    int max = 0;
                    string longs = null;
                    foreach (var slovo in Words)
                    {
                        if (slovo.Length > max)
                        {
                            max = slovo.Length;
                            longs = slovo;
                        }
                        
                    }
                    return longs;
                }
            
            }

            public int VowelCount //гласные
            {
                get
                {
                    return Regex.Matches(Text, "[аеёиоуыэюяАЕЁИОУЫЭЮЯ]").Count;
                }
            }

            public int SentenceCount
            {
                get
                {
                    string[]  sentence = Text.Split(new char[] { '.', '!', '?'}, StringSplitOptions.RemoveEmptyEntries);
                    return  sentence.Length;
                }
            } //кол-во предложений

            public int ConsonantCount //согласные
            {
                get
                {
                    return Regex.Matches(Text, "[бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩ]").Count;
                }
            }

            public Dictionary<char, int> Statisticword {
                get
                {
                    return CountLetters();
                }
            }
            
            public Dictionary<char, int> CountLetters()
            {
                Dictionary<char, int> statisticWord = new Dictionary<char, int>();

                foreach (char symbol in Text.ToLower())
                {
                    if (char.IsLetter(symbol))
                    {
                        if (statisticWord.ContainsKey(symbol))
                        {
                            statisticWord[symbol]++;
                        }
                        else
                        {
                            statisticWord[symbol] = 1;
                        }
                    }
                    
                }
                return statisticWord;
            }

            public virtual void Print()
            {
                Console.WriteLine($"Количество слов: {WordCount} \n Самое короткое слово из текста: {ShortWord} \n Самое длинное слово из текста: {LongWord} \n Количество предложений: {SentenceCount} \n Количество гласных: {VowelCount} \n Количество согласных: {ConsonantCount} \n -----Встречаемость букв------");
                foreach (var w in Statisticword)
                {
                    Console.WriteLine($"{w.Key} - {w.Value} раз");
                }
            }

        }

        class Memory
        {
            private Dictionary<int, TextStatistics> History { get; set; }
                = new Dictionary<int, TextStatistics>();

            private int nextID = 1;

            public void Add(TextStatistics statistics)
            {
                History.Add(nextID, statistics);
                nextID++;
            }

            public Dictionary<int, TextStatistics> GetHistory()
            {
                return History;
            }

            public TextStatistics GetStatistics(int id)
            {
                return History[id];
            }
            
            public void ShowText()
            {
                if (History.Count > 0)
                {
                    Console.WriteLine($"Введите номер текста для вывода статистика");
                    foreach (var statistics in History)
                    {
                        Console.WriteLine($"{statistics.Key}.  {statistics.Value.Text.Remove(30)}...");
                    }

                    Console.WriteLine($"0. Вывести статистику по всем текстам ");
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out int choice))
                    {
                        Console.WriteLine("Нужно ввести число!");
                        return;
                    }

                    PrintText(choice);
                
                }
                else
                {
                    Console.WriteLine($"Вы еще не вводили ни один текст");
                }
                void PrintText(int index)
                {
                    if (index > History.Count)
                    {
                        Console.WriteLine("Такого текста нет.");
                        return;
                    }

                    if (index == 0)
                    {
                        foreach (var history in History)
                        {
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine($"Текст номер {history.Key}:");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine(history.Value.Text);
                            Console.WriteLine("Статистика:");
                            history.Value.Print();
                        }
                        return;
                    }

                    Console.WriteLine("Полный текст:");
                    Console.WriteLine(History[index].Text);
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Статистика:");
                    History[index].Print();
                }
            }
        }
    }
}
