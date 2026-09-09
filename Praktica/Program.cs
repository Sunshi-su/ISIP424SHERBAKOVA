using System;
using System.Collections.Generic;
using System.Linq;
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
            if (kolvo >= 2 && kolvo <= 40)
            {
                Dictionary<int, string> Slovar = new Dictionary<int, string>();
                Console.WriteLine("Введите траты по шаблону (Название услуги или товара; Количество денег)");
                for (int i = 0; i < kolvo; i++)
                {
                    string text = Console.ReadLine();
                    char[] del = new char[] { '(', ')' };
                    text = text.Trim(del);
                    string[] words = text.Split(new char[] { ';' });
                    string key = words[0];
                    string value = words[1];
                }


                bool k = true;
                while (k)
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Выберите что хотите сделать:");

                    Console.WriteLine("1. Вывод данных");
                    Console.WriteLine("2. Статистика");
                    Console.WriteLine("3.  Сортировка по цене");
                    Console.WriteLine("4. Конвертация валюты ");
                    Console.WriteLine("5. Поиск по названию");
                    Console.WriteLine("Выйти - 0");

                    int z = Convert.ToInt32(Console.ReadLine());
                    switch (z)
                    {
                        case 0:
                            k = false;
                            break;
                        case 1:
                            Console.WriteLine("Введите название новой задачи");
                            string New = Console.ReadLine();

                            break;
                        case 2:
                            Console.WriteLine("-=-=-=-==-=-=-=-=-=-=-=-=-=-=");
                            Console.WriteLine("Какую задачу удаляем?");
                            int ind;
                            string cansel = Console.ReadLine();
                            bool perevod = int.TryParse(cansel, out ind);


                            break;
                        case 3:
                            Console.WriteLine("============================");

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
