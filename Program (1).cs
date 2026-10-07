using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string enemyName1 = "Дикий упырь";
            string enemyName2 = "Древний оборотень";
            string enemyName3 = "Высший вампир";

            int maxSanity = 150;       
            int sanity = 150;
            int maxSilver = 50;        
            int silver = 50;
            int maxRecovery = 3;    
            int recovery = 3;

            int enemyMaxHp1 = 50; int enemyMinDmg1 = 5; int enemyMaxDmg1 = 10;
            int enemyMaxHp2 = 75; int enemyMinDmg2 = 10; int enemyMaxDmg2 = 15;
            int enemyMaxHp3 = 100; int enemyMinDmg3 = 15; int enemyMaxDmg3 = 20;

            Random rnd = new Random(); 

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" - - - Начинается бой - - -");
            Console.ResetColor();
            Console.WriteLine("Вы - охотник на нечисть (Возьмак Ривийский). Ваша цель истребить всех монстров.\n");

            bool defending = false;

            for (int wave = 1; wave <= 3; wave++)
            {
                
                string enemyName;
                int enemyHp;
                int enemyMaxHp;
                int enemyMinDmg;
                int enemyMaxDmg;

                if (wave == 1)
                {
                    enemyName = enemyName1;
                    enemyHp = enemyMaxHp1;
                    enemyMaxHp = enemyMaxHp1;
                    enemyMinDmg = enemyMinDmg1;
                    enemyMaxDmg = enemyMaxDmg1;
                }
                else if (wave == 2)
                {
                    enemyName = enemyName2;
                    enemyHp = enemyMaxHp2;
                    enemyMaxHp = enemyMaxHp2;
                    enemyMinDmg = enemyMinDmg2;
                    enemyMaxDmg = enemyMaxDmg2;
                }
                else
                {
                    enemyName = enemyName3;
                    enemyHp = enemyMaxHp3;
                    enemyMaxHp = enemyMaxHp3;
                    enemyMinDmg = enemyMinDmg3;
                    enemyMaxDmg = enemyMaxDmg3;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n- - - Волна {wave}: {enemyName}. ({enemyMaxHp} HP) - - -\n");
                Console.ResetColor();

                while (sanity > 0 && enemyHp > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("\n- - - ХАРАКТЕРИСТИКИ ВОЗЬМАКА - - -\n");
                    Console.ResetColor();

                    Console.Write("Рассудок: ");
                    for (int i = 0; i < maxSanity; i += 5)
                        Console.Write(i < sanity ? "#" : "-");
                    Console.WriteLine($" ({Math.Max(0, sanity)}/{maxSanity})");

                    Console.Write("Серебро:  ");
                    for (int i = 0; i < maxSilver; i += 5)
                        Console.Write(i < silver ? "#" : "-");
                    Console.WriteLine($" ({Math.Max(0, silver)}/{maxSilver})");

                    Console.Write("Восстановление серебра: ");
                    for (int i = 0; i < maxRecovery; i++)
                        Console.Write(i < recovery ? "#" : "-");
                    Console.WriteLine($" ({recovery} шт.)");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n- - - МОНСТРЫ - - -\n");
                    Console.ResetColor();

                    Console.Write($"{enemyName}: ");
                    for (int i = 0; i < enemyMaxHp; i += 5)
                        Console.Write(i < enemyHp ? "#" : "-");
                    Console.WriteLine($" ({Math.Max(0, enemyHp)}/{enemyMaxHp})");

                    int action;
                    bool isValid;

                    do
                    {
                        Console.WriteLine("\nДействия:");
                        Console.WriteLine("1 — Удар освящённым клинком");
                        Console.WriteLine("2 — Обряд изгнания (15 серебра)");
                        Console.WriteLine("3 — Молитва-оборона (урон вдвое меньше)");
                        Console.WriteLine("4 — Восстановление серебра (+25 серебра)");
                        Console.Write("Ваш выбор: ");

                        isValid = int.TryParse(Console.ReadLine(), out action) && action >= 1 && action <= 4;

                        if (!isValid)
                            Console.WriteLine("Ошибка: введите число от 1 до 4!");
                        else if (action == 2 && silver < 15)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Недостаточно серебра! Выберите другое действие.");
                            isValid = false;
                            Console.ResetColor();
                        }
                        else if (action == 4 && recovery == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Восстановление серебра закончилось! Выберите другое действие.");
                            isValid = false;
                        }
                    } while (!isValid);

                    switch (action)
                    {
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            int dmg = rnd.Next(10, 18);
                            enemyHp = enemyHp - dmg;
                            Console.WriteLine($"\n>> Клинок нанёс {dmg} урона.");
                            Console.ResetColor();
                            break;

                        case 2:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            int special = rnd.Next(25, 38);
                            silver = silver - 15;
                            enemyHp = enemyHp - special;
                            Console.WriteLine($"\n>> Обряд изгнания нанёс {special} урона. Потрачено 15 серебра.");
                            Console.ResetColor();
                            break;

                        case 3:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            defending = true;
                            Console.WriteLine("\n>> Молитва защитила вас.");
                            Console.ResetColor();
                            break;

                        case 4:
                            recovery--;
                            silver = silver + 25;
                            if (silver > maxSilver) silver = maxSilver;
                            Console.WriteLine($"\n>> Серебно восстановлено. Восстановление энергии осталось: {recovery}.");
                            break;
                    }

                    if (enemyHp <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n{enemyName} повержен!");

                       
                        silver = silver + 15;
                        if (silver > maxSilver) silver = maxSilver;
                        Console.WriteLine("Ваша награда 15 серебра!");
                        Console.ResetColor();
                        break;

                    }

                    int enemyDamage = rnd.Next(enemyMinDmg, enemyMaxDmg + 1);

                    if (defending)
                    {
                        enemyDamage = enemyDamage / 2;
                        Console.WriteLine($"\n>> {enemyName} нанёс {enemyDamage} урона (молитва ослабила удар).");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n>> {enemyName} нанёс {enemyDamage} урона.");
                    }

                    defending = false;
                    sanity = sanity - enemyDamage;

                  
                    if (sanity <= 0)
                    {
                        Console.WriteLine("\nВы умерли от потери рассудка... Игра окончена.");
                        break;
                    }
                }
            }
        }
    }
}