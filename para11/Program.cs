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
namespace  para11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Order> orders = new List<Order>();

            orders.Add(new Order(2, 1000, 10));
            orders.Add(new Order(5, 250, 20));
            orders.Add(new Order(1, 3000, 5));

            Console.WriteLine("Начальные данные:");
            foreach (Order order in orders)
            {
                order.PrintInfo();
            }

            Console.WriteLine("После изменения первого заказа:");
            orders[0].Count = 3;
            orders[0].Price = 1200;
            orders[0].Discount = 15;
            orders[0].PrintInfo();

            Console.WriteLine("После изменения второго заказа:");
            orders[1].Count = 10;
            orders[1].Discount = 25;
            orders[1].PrintInfo();

            Console.WriteLine("После изменения третьего заказа:");
            orders[2].Price = 2800;
            orders[2].Discount = 50;
            orders[2].PrintInfo();

            Console.ReadKey();
        }

        class Order
        {
            private int count;
            private decimal price;
            private decimal discount;

            public string Name { get; set; }

            public Order(string name, int count, decimal price, decimal discount)
            {
                Name = name;
                Count = count;
                Price = price;
                Discount = discount;
            }

            public int Count
            {
                get { return count; }
                set
                {
                    if (value > 0)
                    {
                        count = value;
                    }
                }
            }

            public decimal Price
            {
                get { return price; }
                set
                {
                    if (value > 0)
                    {
                        price = value;
                    }
                }
            }

            public decimal Discount
            {
                get { return discount; }
                set
                {
                    if (value >= 0 && value <= 50)
                    {
                        discount = value;
                    }
                }
            }

            public decimal TotalPrice
            {
                get { return Count * Price * (1 - Discount / 100); }
            }

            public void PrintInfo()
            {
                Console.WriteLine($"-------------------------------------- ");
                Console.WriteLine($"назвыавние: {Name} ");
                Console.WriteLine($"Количество: {Count} шт. ");
                Console.WriteLine($"Цена за единицу: {Price}₽");
                Console.WriteLine($"Скидка: {Discount}%");
                Console.WriteLine($"Итоговая стоимость: {TotalPrice}");
                Console.WriteLine();
            }
        }
    }
}