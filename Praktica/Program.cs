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
            //проверка каут если нет то в экзист записать фалсе
            //category name!!
            List<Product> products = new List<Product>();
            products.Add(new Product("пельмени", 100, 10, CategoryName.Еда));
            products.Add(new Product("ручка", 50, 30, CategoryName.Концелярия));
            products.Add(new Product("карандаш", 30, 123, CategoryName.Концелярия));
            products.Add(new Product("MacBook Neo", 100000, 5, CategoryName.Техника));
            products.Add(new Product("мышка", 1500, 67, CategoryName.Техника));
            CategoryName categoryName = CategoryName.Еда;

            bool k = true;
            while (k)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Выберите что хотите сделать:");

                Console.WriteLine("1. Добавить товар"); //
                Console.WriteLine("2. Удалить товар");//
                Console.WriteLine("3. Заказать поставку товара");
                Console.WriteLine("4. Продать товар ");//
                Console.WriteLine("5. Поиск товаров");//
                Console.WriteLine("Выйти - 0");

                int z = Convert.ToInt32(Console.ReadLine());
                switch (z)
                {
                    case 0:
                        k = false;
                        break;
                    case 1:
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("Выберите категорию нового товара:");
                        Console.WriteLine("1. Еда"); //
                        Console.WriteLine("2. Техника ");//
                        Console.WriteLine("3. Быт");
                        bool validInput = true;
                        while (validInput)
                        {
                            int n = Convert.ToInt32(Console.ReadLine());
                            switch (n)
                            {
                                case 1:
                                    categoryName = CategoryName.Еда;
                                    break;
                                case 2:
                                    categoryName = CategoryName.Техника;
                                    break;
                                case 3:
                                    categoryName = CategoryName.Быт;
                                    break;
                                default:
                                    Console.WriteLine("Неверный выбор");
                                    validInput = false;
                                    break;
                            }
                            Console.WriteLine("Введите  название товара");
                            string productName = Console.ReadLine();
                            Console.WriteLine("Введите  цену за единицу товара");
                            double productPrice = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Введите количество в наличии");
                            int productCount = Convert.ToInt32(Console.ReadLine());

                            Product h = new Product(productName, productPrice, productCount, categoryName);
                            //h добавляем в лист продукт
                        }



                        break;
                    case 2:

                        break;
                    case 3:



                    case 4:



                        break;
                    case 5:

                        break;

                    default:
                        Console.WriteLine("Не верное число");
                        break;

                }
            }
        }

        class Product
        {
            public static int Id = 1;
            public string Name { get; set; }

            public double Price { get; set; }

            public int Count { get; set; }

            public bool Exist { get; set; }

            public CategoryName Category;

            public Product(string Name, double Price, int Count, CategoryName category)
            {
                this.Name = Name;
                this.Price = Price;
                this.Count = Count;
                if (Count <= 0)
                {
                    this.Exist = false;
                }
                else
                {
                    this.Exist = true;
                }
                Category = category;
            }

            public bool Search(int  Count)
            {
                if (Count <= 0)
                {
                    return false;
                }
                else if (Count > 0)
                {

                    return true;
                }
                else
                {
                    this.Count -= Count;
                    Console.WriteLine($"Продано {Count} единиц товара {Name}. Остаток на складе: {this.Count}");
                    return true;
                }
                
            }
        }

        public enum CategoryName
        {
            Еда,
            Техника,
            Быт,
            Концелярия

        }

    }
}
