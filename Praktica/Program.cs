using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.NetworkInformation;
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
            products.Add(new Product("Штангерциркуль", 50, 30, CategoryName.Концелярия));
            products.Add(new Product("карандаш", 30, 123, CategoryName.Концелярия));
            products.Add(new Product("MacBook Neo", 123000, 5, CategoryName.Техника));
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
                
                string menu = Console.ReadLine();
                if (!int.TryParse(menu, out int z))
                {
                    Console.WriteLine("Нужно ввести число!");
                    continue;
                }
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
                        Console.WriteLine("4. Концелярия");
                        bool validInput = true;
                        while (validInput)
                        {
                            
                            string categoryText = Console.ReadLine();
                            if (!int.TryParse(categoryText, out int n))
                            {
                                Console.WriteLine("Категория должна быть числом!");
                                validInput = false;
                                continue;
                            }
                            
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
                                case 4:
                                    categoryName = CategoryName.Концелярия;
                                    break;
                                default:
                                    Console.WriteLine("Неверный выбор");
                                    validInput = false;
                                    continue;
                            }
                            Console.WriteLine("Введите  название товара");
                            string productName = Console.ReadLine();
                            if (productName == null)
                            {
                                Console.WriteLine("Название не может быть пустым!!");
                                validInput = false;
                                continue;
                            }

                            Console.WriteLine("Введите  цену за единицу товара");
                            double productPrice;
                            if (!double.TryParse(Console.ReadLine(), out productPrice))
                            {
                                Console.WriteLine("Цена должна быть числом дурак!");
                                validInput = false;
                                continue;
                            }
                            else if (productPrice > 0)
                            {
                                productPrice = Convert.ToDouble(productPrice);
                            }
                            else
                            {
                                Console.WriteLine("Цена должна быть положительным числом гооосподи!");
                                validInput = false;
                                continue;
                            }
                            
                            Console.WriteLine("Введите количество в наличии");
                            int productCount;
                            if (!int.TryParse(Console.ReadLine(), out productCount))
                            {
                                Console.WriteLine("Количество должно быть числом ААААА!");
                                validInput = false;
                                continue;
                            }
                            else if (productCount >= 0)
                            {
                                productCount = Convert.ToInt32(productCount);
                            }
                            else
                            {
                                Console.WriteLine("Количество должно быть положительным числом БОООЖЕ!");
                                validInput = false;
                                continue;
                            }

                            Product h = new Product(productName, productPrice, productCount, categoryName);
                            products.Add(h);
                            //h добавляем в лист продукт
                            Console.WriteLine("Добавление успешно!");
                            Console.WriteLine("=====================================");
                            Product.Print(products);
                            validInput = false;
                        }
                        break;
                    
                    case 2:
                        Console.WriteLine("Введите порядковый номер товара для удаления из списка");
                        Product.Print(products);
                        string number = Console.ReadLine();
                        Product.Delete(number, products);
                        
                        break;
                    
                    case 3:
                        Console.WriteLine("Введите ID товара для заказа поставки");
                        Product.PrintShort(products);
                        if (int.TryParse(Console.ReadLine(), out int Id))
                        {
                            if (Id > 0 )
                            {
                                Product.Add(Id, products);
                            }
                            else
                            {
                                Console.WriteLine("Такого ID не существует");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Неверный ID");
                        }
                        
                        break;
                    
                    case 4:
                        Console.WriteLine("Введите ID товара продажи");
                        Product.PrintShort(products);
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            if (id > 0 )
                            {
                                Product.Sale(id, products);
                            }
                            else
                            {
                                Console.WriteLine("Такого ID не существует");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Неверный ID");
                        }
                        


                        break;
                    case 5:
                        Product.Search(products);

                        break;

                    default:
                        Console.WriteLine("Не верное число");
                        break;

                }
            }
        }

        class Product
        {
            private static int nextID = 0;
            
            public  int ID ;
            public string Name { get; set; }

            public double Price { get; set; }

            public int Count { get; set; }

            public bool Exist
            {
                get {
                    if (Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public CategoryName Category;

            public Product(string Name, double Price, int Count, CategoryName category)
            {
                ID = ++nextID;
                this.Name = Name;
                this.Price = Price;
                this.Count = Count;
                Category = category;
            }

            public static void Print(List<Product> products)
            {
                foreach (var item in products)
                {
                    Console.WriteLine($" ID товара: {item.ID} ");
                    Console.WriteLine($" название: {item.Name} ");
                    Console.WriteLine($" Цена: {item.Price}₽ ");
                    Console.WriteLine($" Остаток: {item.Count} шт. ");
                    Console.WriteLine($" Наличие на складе: {item.Exist} ");
                    Console.WriteLine($" Категория: {item.Category} ");
                    Console.WriteLine("----------------------------------------");
                }
            }
            
            public static void PrintShort(List<Product> products)
            {
                foreach (var item in products)
                {
                    Console.WriteLine($" ID товара: {item.ID} ");
                    Console.WriteLine($" название: {item.Name} ");
                    Console.WriteLine($" Остаток: {item.Count} шт.");
                    Console.WriteLine("----------------------------------------");
                }
            }

            public static void Delete(string num, List<Product>products)
            {
                if (int.TryParse(num, out int Num))
                {
                    if (Num > 0  && Num <= products.Count)
                    {
                        products.RemoveAt(Num-1);
                        Console.WriteLine($" Удаление успещно!");
                        Console.WriteLine("Новый список товаров: ");
                        Console.WriteLine("----------------------------------------");
                        Product.Print(products);
                        
                    }
                    else if (Num > products.Count)
                    {
                        Console.WriteLine($" Ваше число больше чем количество товаров в скписке");
                    }
                    else
                    {
                        Console.WriteLine($" Число не может быть отрицательным РРРР!!");
                        Console.WriteLine("----------------------------------------");
                    }
                    
                }
                else
                {
                    Console.WriteLine($" Напиши целое число епстудей!!");
                    Console.WriteLine("----------------------------------------");
                }
                
            }

            public static void Add(int ID, List<Product> product)
            {
                
                Console.Write("Введите желаемое количество товара для поставки: ");
                string newCount = Console.ReadLine();
                if (int.TryParse(newCount, out int count))
                {
                    if (count > 0)
                    {
                        var item = product.Find(p => p.ID == ID);
                        if (item != null)
                        {
                            item.Count += count;
                        }
                        else
                        {
                            Console.WriteLine($"Товар с этим ID не существует или был удален");
                        }
                    }
                    else
                    {
                        Console.WriteLine($" Число не может быть отрицательным РРРР!!");
                        Console.WriteLine("----------------------------------------");
                    }

                }
                else
                {
                    Console.WriteLine("Количество должно быть числом ААААА!");
                }
            }

            public static void Sale(int ID,List<Product> product)
            {
                Console.Write("Введите количество продаваемого товара: ");
                string newCount = Console.ReadLine();
                if (int.TryParse(newCount, out int count))
                {
                    if (count > 0)
                    {
                        var item = product.Find(p => p.ID == ID);
                        if (item != null)
                        {
                            if (item.Exist && count <= item.Count)
                            {
                                item.Count -= count;
                                Console.WriteLine("Продажа успешна!");
                                Console.WriteLine("Новые данные товара: ");
                                Console.WriteLine($" ID товара: {item.ID} ");
                                Console.WriteLine($" название: {item.Name} ");
                                Console.WriteLine($" Остаток: {item.Count} шт.");
                                Console.WriteLine("----------------------------------------");
                                
                            }
                            else
                            {
                                Console.WriteLine("Недостаточно товара на складе");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Товар с этим ID не существует или был удален");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Количество должно быть положительным числом");
                    }
                }
                else
                {
                    Console.WriteLine("Количество должно быть числом ААААА!");
                }
            }

            public static void Search(List<Product> product)
            {
                Console.WriteLine($"Выберете по какому критерию будем искать:");
                Console.WriteLine("1. ID"); 
                Console.WriteLine("2. Название");
                Console.WriteLine("3. Категория");
                Console.WriteLine("4. Цена");
                string Find = Console.ReadLine();
                if (int.TryParse(Find, out int n))
                {
                    switch (n)
                    {
                        case 1:
                            Console.WriteLine($"Введите ID");
                            string ID = Console.ReadLine();
                            if (int.TryParse(ID, out int id))
                            {
                                id  = Convert.ToInt32(ID);
                                var item = product.Find(p => p.ID == id);
                                if (id > 0)
                                {
                                    if (item != null)
                                    {
                                        Console.WriteLine($" ID товара: {item.ID} ");
                                        Console.WriteLine($" название: {item.Name} ");
                                        Console.WriteLine($" Цена: {item.Price}₽ ");
                                        Console.WriteLine($" Остаток: {item.Count} шт. ");
                                        Console.WriteLine($" Наличие на складе: {item.Exist} ");
                                        Console.WriteLine($" Категория: {item.Category} ");
                                        Console.WriteLine("----------------------------------------");
                                    }
                                    else 
                                        Console.WriteLine($"Товар с этим ID не существует или был удален");
                                    
                                }
                                else
                                {
                                    Console.WriteLine($"Товар не может быть с отрицательным ID гад демн!!"); 
                                }
                            }
                            else 
                                Console.WriteLine($"Глаза разуй, число пиши, а не ахинею свою!!"); 
                            break;
                        case 2:
                            Console.Write($"Введите название товара:");
                            string name = Console.ReadLine();
                            var itemName = product.FindAll(p => p.Name == name);
                            if (itemName.Count > 0)
                            {
                                Console.WriteLine($"Вот что нашлось по запросу: '{name}' ");
                                Product.Print(itemName);
                            }
                            else 
                                Console.WriteLine($"По запросу: '{name}' Ничего не нашлось :( ");
                            break;
                        case 3:
                            Console.WriteLine("Выберите категорию:");
                            Console.WriteLine("1. Еда");
                            Console.WriteLine("2. Техника");
                            Console.WriteLine("3. Быт");
                            Console.WriteLine("4. Концелярия");

                            string category = Console.ReadLine();

                            if (int.TryParse(category, out int c))
                            {
                                CategoryName categoryName = CategoryName.Еда;

                                switch (c)
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
                                    case 4:
                                        categoryName = CategoryName.Концелярия;
                                        break;
                                    default:
                                        Console.WriteLine("Такой категории нету емае!!");
                                        return;
                                }

                                var itemCategory = product.FindAll(p => p.Category == categoryName);

                                if (itemCategory.Count > 0)
                                {
                                    Console.WriteLine($"Вот что нашлось по категории: '{categoryName}' ");
                                    Product.Print(itemCategory);
                                }
                                else
                                {
                                    Console.WriteLine($"В категории: '{categoryName}' Ничего не нашлось :( ");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Это не числооо с//////аа");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Введите цену товара:");
                            string priceText = Console.ReadLine();

                            if (double.TryParse(priceText, out double price))
                            {
                                if (price >= 0)
                                {
                                    var itemPrice = product.FindAll(p => p.Price == price);

                                    if (itemPrice.Count > 0)
                                    {
                                        Console.WriteLine($"Вот что нашлось по цене: {price}₽ ");
                                        Product.Print(itemPrice);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"По цене: {price}₽ Ничего не нашлось :( ");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Цена не может быть отрицательной ну емае!!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Цена должна быть числом ААААА!");
                            }
                            break;
                        default:
                            Console.WriteLine("Неверное число");
                            break;
                    }
                    
                }
                else 
                    Console.WriteLine("Это не чиссчкаа");
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
