using System;
using System.Collections.Generic;
using System.Linq;

namespace Praktica
{
    internal class Program
    {
        
        static void Fight(Player player, Enemy enemy, Random random)
        {
            while (player.HP > 0 && enemy.HP > 0)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"Ваше HP: {player.HP}");
                Console.WriteLine($"{enemy.Name}: {enemy.HP} HP");

                bool isDefending = false;

                if (player.IsFrozen)
                {
                    Console.WriteLine("Вы заморожены и пропускаете ход");
                    player.IsFrozen = false;
                }
                else
                {
                    Console.WriteLine("1. Атаковать");
                    Console.WriteLine("2. Защищаться");

                    string choice = Console.ReadLine();

                    if (choice == null)
                        return;

                    if (choice == "1")
                    {
                        enemy.GetDamage(player.Attack);
                    }
                    else if (choice == "2")
                    {
                        isDefending = true;
                    }
                    else
                    {
                        Console.WriteLine("Выберите 1 или 2!");
                        continue;
                    }
                }
                
                if (enemy.HP <= 0)
                    break;

                enemy.DoAttack(player, isDefending, random);
            }

            if (player.HP > 0 && enemy.HP <= 0)
                Console.WriteLine($"Вы победили {enemy.Name}!");
        }



        static void Main(string[] args)
        {
            Random random = new Random();
            List<Armor> armors = new List<Armor> { new Armor("Кольчужная броня", 15), new Armor("Железная броня", 20), new Armor("Алмазная броня",25), new  Armor("Зеркальная броня",30) };
            List<Weapon> weapons = new List<Weapon>{ new Weapon("Острый меч",25), new Weapon("Заточенный меч", 30), new Weapon("Заколдованный меч", 35)};

            
            Player player = new Player(
                new Weapon("Обычный меч", 20),
                new Armor("Кожаные доспехи", 10), false);
            int turn = 0;

            bool k = true;
            while (k && player.HP > 0)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"Пройдено ходов: {turn}");
                Console.WriteLine($"Здоровье: {player.HP}");
                Console.WriteLine("1. Следующий ход");
                Console.WriteLine("2. Посмотреть экипировку");
                Console.WriteLine("0. Выход");
                Console.WriteLine("-----------------------------------");

                string menu = Console.ReadLine();
                if (menu == null)
                {
                    return;
                }

                if (!int.TryParse(menu, out int choice))
                {
                    Console.WriteLine("Нужно ввести число!");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                    {
                        turn++;
                        Console.WriteLine($"Ход {turn}");

                        if (turn % 10 == 0)
                        {
                            Console.WriteLine("Вы встретили босса!");

                            switch (random.Next(4))
                            {
                                case 0:
                                    Console.WriteLine("ВВГ");
                                    Goblin bossVVG = new Goblin(hp: 50 * 2.5f, def: 10 * 1.4f, attack: 10 * 1.3f);
                                    Fight(player, bossVVG, random);
                                    break;
                                case 1:
                                    Console.WriteLine("Ковальский");
                                    Scelet bossKoval = new Scelet ( hp: 50 * 2.5f, def: 10 * 1.4f, attack: 10 * 1.3f );
                                    Fight(player, bossKoval, random);
                                    break;
                                case 2:
                                    Console.WriteLine("Архимаг(МОГ) C++");
                                    Mage bossMOG = new Mage(hp: 50 * 1.8f, def: 10 * 1.1f, attack: 10 * 1.6f);
                                    Fight(player, bossMOG, random);
                                    break;
                                case 3:
                                    Console.WriteLine("Пестов С--");
                                    Pestov bossPestov = new Pestov();
                                    Fight(player, bossPestov, random);
                                    break;
                            }
                            
                        }
                        else if (random.Next(2) == 0)
                        {
                            Console.WriteLine("Вы нашли сундук!");
                            if (random.Next(2) == 0)
                            {
                                Console.WriteLine($"Найдено Лечебное зелье!");
                                Console.WriteLine($"Выпить его?");
                                Console.WriteLine($"1. Да");
                                Console.WriteLine($"2. Нет");
                                
                                string YesNo = Console.ReadLine();
                                if (YesNo == null)
                                {
                                    return;
                                }

                                if (!int.TryParse(YesNo, out int Chto))
                                {
                                    Console.WriteLine("Нужно ввести числоaaaaaaa!");
                                    continue;
                                }

                                switch (Chto)
                                {
                                    case 1:

                                        Console.WriteLine("Вы Выпили зелье и восстановили 50HP!");
                                        player.Heal();
                                        break;
                                    case 2:
                                        Console.WriteLine("Вы выбросили зелье,,,,,удачи.....");
                                        break;
                                }
                            }
                            else
                            {
                                if (random.Next(2) == 0)
                                {
                                    Weapon items = weapons[random.Next(weapons.Count)];
                                    Console.WriteLine($"Найдено: {items.Name}, Атака: {items.Attack}");
                                    Console.WriteLine($"Ваша статистика: {player.NowWeapon.Name}, Атака: {player.NowWeapon.Attack}");

                                    Console.WriteLine($"Взять предмет?");
                                    Console.WriteLine($"1. Да");
                                    Console.WriteLine($"2. Нет");
                                    string YesNo = Console.ReadLine();
                                    if (YesNo == null)
                                    {
                                        return;
                                    }

                                    if (!int.TryParse(YesNo, out int Chto))
                                    {
                                        Console.WriteLine("Нужно ввести числоaaaaaaa!");
                                        continue;
                                    }

                                    switch (Chto)
                                    {
                                        case 1:

                                            Console.WriteLine("Вы взяли новый предмет");
                                            player.EquipWeapon(items);
                                            break;
                                        case 2:
                                            Console.WriteLine("Вы выбросили предмет");
                                            break;
                                    }

                                    continue;
                                }
                                else
                                {
                                    Armor item = armors[random.Next(armors.Count)];
                                    Console.WriteLine($"Найдено: {item.Name}, Защита: {item.Defence}");
                                    Console.WriteLine(
                                        $"Ваша статистика: {player.NowArmor.Name}, Защита: {player.NowArmor.Defence}");

                                    Console.WriteLine($"Взять предмет?");
                                    Console.WriteLine($"1. Да");
                                    Console.WriteLine($"2. Нет");
                                    string YesNo = Console.ReadLine();
                                    if (YesNo == null)
                                    {
                                        return;
                                    }

                                    if (!int.TryParse(YesNo, out int Chto))
                                    {
                                        Console.WriteLine("Нужно ввести числоaaaaaaa!");
                                        continue;
                                    }

                                    switch (Chto)
                                    {
                                        case 1:

                                            Console.WriteLine("Вы взяли новый предмет");
                                            player.EquipArmor(item);
                                            break;
                                        case 2:
                                            Console.WriteLine("Вы выбросили предмет");
                                            break;
                                    }

                                }
                            }

                            continue;
                        }
                        else
                        {
                            Enemy enemy;

                            switch (random.Next(3))
                            {
                                case 0:
                                    enemy = new Goblin();
                                    break;
                                case 1:
                                    enemy = new Scelet();
                                    break;
                                default:
                                    enemy = new Mage();
                                    break;
                            }

                            Console.WriteLine($"Вы встретили {enemy.Name}!");
                            Fight(player, enemy, random);
                        }

                        break;
                    }

                    case 2:
                        Console.WriteLine($"Оружие: {player.NowWeapon.Name}, атака: {player.Attack}");

                        Console.WriteLine($"Доспехи: {player.NowArmor.Name}, защита: {player.Defence}");
                        break;

                    case 0:
                        k = false;
                        break;

                    default:
                        Console.WriteLine("Неверный пункт меню");
                        break;
                }
            }

            if (player.HP <= 0)
                Console.WriteLine("Вы погибли. Игра окончена!");
        }
    }

    abstract class Essence
    {
        public float HP { get; set; }

        public Essence(float hp)
        {
            HP = hp;
        }
    }


    class Player : Essence
    {
        public Weapon NowWeapon { get; private set; }
        public Armor NowArmor { get; private set; }

        public bool IsFrozen { get; set; }

        public Player(Weapon nowWeapon, Armor nowArmor, bool isFrozen) : base(300)
        {
            NowWeapon = nowWeapon;
            NowArmor = nowArmor;
            IsFrozen = isFrozen;
        }

        public float Attack
        {
            get { return NowWeapon.Attack; }
        }

        public float Defence
        {
            get { return NowArmor.Defence; }
        }


        public void EquipWeapon(Weapon weapon)
        {
            NowWeapon = weapon;
        }

        public void EquipArmor(Armor armor)
        {
            NowArmor = armor;
        }

        public void GetDamage(float damage)
        {
            HP -= damage;
        }

        public void Heal()
        {
            HP += 50;
        }

        public float DamageOfPlayer(float damage, Random random, bool IsDefense, bool IgnoresDefence)
        {
            if (IsDefense)
            {
                if (random.Next(100) < 40)
                    return 0f;
                if (!IgnoresDefence)
                {
                    float block = Defence * random.Next(70, 101) / 100f;
                    damage -= block;
                }
            }

            return Math.Max(0f, damage);
        }
        
    }

    class Weapon
    {
        public string Name { get; private set; }
        public float Attack { get; private set; }

        public Weapon(string name, float attack)
        {
            Name = name;
            Attack = attack;
        }
        
    }

    class Armor
    {
        public string Name { get; private set; }
        public float Defence { get; private set; }

        public Armor(string name, float defence)
        {
            Name = name;
            Defence = defence;
        }
    }



    class Enemy : Essence
    {
        public string Name { get; private set; }
        public float Defence { get; private set; }
        public float Attack { get; private set; }

        public Enemy(string name, float hp, float def, float attack)
            : base(hp)
        {
            Name = name;
            Defence = def;
            Attack = attack;
        }

        public virtual void GetDamage(float damage)
        {
            float finalDamage = Math.Max(0f, damage - Defence);
            HP = Math.Max(0f, HP - finalDamage);

            Console.WriteLine($"Враг получил урон! -{finalDamage} HP");
        }

        public virtual void DoAttack(
            Player player, bool isDefending, Random random)
        {
            float damage = player.DamageOfPlayer(
                Attack, random, isDefending, false);

            player.GetDamage(damage);
            Console.WriteLine($"Вы получили {damage} урона!");
        }
    }

    class Scelet : Enemy
    {
        public Scelet(float hp = 50, float def = 10, float attack = 10) : base("Скелет", hp, def, attack)
        {
        }

        public override void DoAttack(Player player, bool isDefending, Random random)
        {
            float damage = player.DamageOfPlayer(Attack, random, isDefending, true);

            player.GetDamage(damage);
            Console.WriteLine($"Скелет нанёс вам {damage} урона!");
        }
    }

    class Goblin : Enemy
    {
        public Goblin(float hp = 50, float def = 10, float attack = 10) : base("Гоблин", hp, def, attack)
        {

        }

        public override void DoAttack(Player player, bool isDefending, Random random)
        {
            float attackDamage = Attack;

            if (random.Next(100) < 10)
            {
                attackDamage *= 2f;
                Console.WriteLine("Критический удар!");
            }

            float damage = player.DamageOfPlayer(attackDamage, random, isDefending, false);

            player.GetDamage(damage);
            Console.WriteLine($"Гоблин нанёс вам {damage} урона!");
        }
    }

    class Mage : Enemy
    {

        public Mage(float hp = 50, float def = 10, float attack = 10) : base("Маг", hp, def, attack)
        {
        }

        public override void DoAttack(Player player, bool isDefending, Random random)
        {
            float damage = player.DamageOfPlayer(Attack, random, isDefending, false);

            player.GetDamage(damage);
            Console.WriteLine($"Ма(о)г нанёс вам {damage} урона!");

            if (player.HP > 0 && random.Next(100) < 10)
            {
                player.IsFrozen = true;
                Console.WriteLine("Маг отправил вас в теневой бан!");
            }
        }
    }

    class Pestov : Scelet //ну а как тут еще ему заморозку впихнуть, пикми класс пестов
    {
        public Pestov() : base(65, 6, 18)
        {
            
        }

        public override void DoAttack(Player player, bool isDefending, Random random)
        {
            base.DoAttack(player, isDefending, random);

            if (player.HP > 0 && random.Next(100) < 25)
            {
                player.IsFrozen = true;
                Console.WriteLine("Cкелето-Пестово отправил вас в теневой бан!");
            }
        }
    }
}






//S — каждый класс занимается своим делом.
// O — добавляем возможности, не переписывая работающий код.
// L — наследник заменяет родителя без поломок.
// I — интерфейсы содержат только нужные методы.
// D — важные функции зависимы от интерфейсов, а не от конкретных классов.