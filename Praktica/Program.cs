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
                            break;
                        }
                        else
                        {
                            TextStatistics statistics = new TextStatistics();
                            statistics.Text = text;
                            statistics.Print();
                        }

                        

                        // тут вывести всю статистику:
                        // количество слов
                        // самое короткое слово
                        // самое длинное слово
                        // количество предложений
                        // количество гласных
                        // количество согласных
                        // частоту букв

                        // тут добавить statistics в Memory

                        break;

                    case 2:
                        // тут показать статистику прошлых текстов
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
                    return Text.Split(new char[] { ' ', ',', ':','-','—', '.', '!', '?'}, StringSplitOptions.RemoveEmptyEntries);
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
            public List<TextStatistics> History { get; set; } = new List<TextStatistics>();

            
        }
    }
}
