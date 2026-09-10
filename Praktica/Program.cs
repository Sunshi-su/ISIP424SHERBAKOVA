using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Praktica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сколько операций будет заполнено?");
            int kolvo = Convert.ToInt32(Console.ReadLine());
            double kurs = 1;

            if (kolvo >= 2 && kolvo <= 40)
            {
                Dictionary<string, int> Slovar = new Dictionary<string, int>();
                int [] operations = new int [kolvo];
                Console.WriteLine("Введите траты по шаблону (Название услуги или товара; Количество денег (в рублях)  )");
                for (int i = 0; i < kolvo; i++)
                {
                    string text = Console.ReadLine();
                    char[] del = new char[] { '(', ')' };
                    text = text.Trim(del);
                    string[] words = text.Split(new char[] { ';' });
                    string key = words[0];
                    int value = Convert.ToInt32(words[1]);
                    //это  собствеенно вспомогательный  массив , который хранит значения для сортировки
                    operations[i] = Convert.ToInt32(value);
                    Slovar.Add(key, value);
                }


                bool k = true;
                while (k)
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Выберите что хотите сделать:");

                    Console.WriteLine("1. Вывод данных"); //+
                    Console.WriteLine("2. Статистика");//+
                    Console.WriteLine("3.  Сортировка по цене");
                    Console.WriteLine("4. Конвертация валюты ");//+
                    Console.WriteLine("5. Поиск по названию");//+
                    Console.WriteLine("Выйти - 0");

                    int z = Convert.ToInt32(Console.ReadLine());
                    switch (z)
                    {
                        case 0:
                            k = false;
                            break;
                        case 1:
                            Console.WriteLine("Ваши траты");
                            Console.WriteLine($"Курс 1 рубль = {kurs}");
                            foreach (var kvp in Slovar)
                            {
                                Console.WriteLine($"{kvp.Key}: {kvp.Value * kurs}");
                            }
                            break;
                        case 2:

                            Console.WriteLine("-=-=-=-==-=-=-=-=-=-=-=-=-=-=");
                            Console.WriteLine("Cтатистика по данным");
                            Console.WriteLine($"Cреднее значение цены: {operations.Average()}");
                            Console.WriteLine($"Минимальное значение цены: {operations.Min()}");
                            Console.WriteLine($"Максимальное значение цены: {operations.Max()}");
                            Console.WriteLine($"Сумма всех значений: {operations.Sum()}");

                            break;
                        case 3:

                            Console.WriteLine("============================");
                            Console.WriteLine("Сортирую по цене все тарты от меньшего к большему");
                            for (int i = 0; i < operations.Length - 1; i++)
                            {
                                bool swapped = false;

                                for (int j = 0; j < operations.Length - 1 - i; j++)
                                {

                                    if (operations[j] > operations[j + 1])
                                    {
                                        int temp = operations[j];
                                        operations[j] = operations[j + 1];
                                        operations[j + 1] = temp;
                                        swapped = true;
                                    }
                                }
                                if (!swapped)
                                {
                                    break;
                                }
                            }
                            break;

                        case 4:

                            Console.WriteLine("Выберете курс валюты  1-5");
                            Console.WriteLine("1) BYN");
                            Console.WriteLine("2) $");
                            Console.WriteLine("3) €");
                            Console.WriteLine("4) ¥");
                            Console.WriteLine("5) Ввести свой курс валюты");
                            int n = Convert.ToInt32(Console.ReadLine());
                            switch (n)
                            {
                                case 1:
                                    kurs = 0.036;
                                    break;
                                case 2:
                                    kurs = 0.012;
                                    break;
                                case 3:
                                    kurs = 0.010;
                                    break;
                                case 4:
                                    kurs = 1.84;
                                    break;
                                case 5:
                                    Console.WriteLine("Введите свой курс валюты в значении 1 рубль  =  :");
                                    kurs = Convert.ToDouble(Console.ReadLine());
                                    break;
                                default:
                                    Console.WriteLine("Неверный выбор");
                                    break;
                            }
                            break;
                        case 5:
                            Console.WriteLine("Введите название услуги или товара");
                            string name = Console.ReadLine();
                            if (Slovar.ContainsKey(name))
                            {
                                Console.WriteLine($"Цена за {name} составляет {Slovar[name]}");
                            }
                            else
                            {
                                Console.WriteLine("Такого названия нет в списке");
                            }
                            break;

                        default:
                            Console.WriteLine("Не верное число");
                            break;

                    } 
                }
            }
            else
            {
                Console.WriteLine("Количество операций меньше 2 или превышает 40");

            }
        }
    }
}
