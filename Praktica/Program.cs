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
            CategoryName categoryName;

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

                                break;
                            default:
                                Console.WriteLine("Неверный выбор");
                                break;
                        }
                        Product h = new Product(,categoryName);
                        //h добавляем в лист продукт
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

            public Product (string  Name, double Price, int Count, bool Exist, CategoryName category)
            {
                this.Name = Name;
                this.Price = Price; 
                this.Count = Count;
                this.Exist = Exist;
                Category = category;
            }

            public bool Sklad(Count)
            {
                return true;
            }
        }

        public enum CategoryName
        {
            Еда,
            Техника,
            Быт,

        }

    }
}
