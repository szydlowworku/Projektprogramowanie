using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GraNaZaliczenie
{
    class Program
    {
        static int worldWidth = 40;
        static int worldHeight = 20;

        static char[,] world = new char[worldHeight, worldWidth];
        static char player = '@';
        static char wall = '#';
        static char item = '*';
        static char npc = 'N';
        static char emptySpace = ' ';
        static char house = 'H';
        static char keyItem = 'K';
        static char basement = 'B';

        static int playerX = worldWidth / 2;
        static int playerY = worldHeight / 2;

        static int playerHealth = 10;
        static int score = 0;
        static Random random = new Random();

        static Dictionary<Tuple<int, int>, int> npcHealth = new Dictionary<Tuple<int, int>, int>();
        static List<string> activeMissions = new List<string>();
        static List<string> inventory = new List<string>();

        static int level = 1;
        static Dictionary<int, string> levelNames = new Dictionary<int, string>()
        {
            { 1, "Targ" },
            { 2, "Obszar mieszkalny" },
            { 3, "Dom" },
            { 4, "Piwnica" }
        };

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            InitializeWorld();

            while (true)
            {
                DrawWorld();
                var key = Console.ReadKey(true).Key;
                MovePlayer(key);
                UpdateNPCs();

                if (score >= 100 && level == 1 && activeMissions.Contains("Reach 100 gold"))
                {
                    Console.Clear();
                    Console.WriteLine("Gratulacje! Osiągnąłeś 100 zł i ukończyłeś misję!");
                    Console.ReadKey(true);
                    level++;
                    InitializeWorld();
                    score = 0;
                    playerHealth = 10;
                    activeMissions.Remove("Reach 100 gold");
                }

                if (playerHealth <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Zostałeś pokonany! Koniec gry.");
                    Console.WriteLine($"Twoja końcowa punktacja: {score}");
                    Thread.Sleep(1200);
                    break;
                }
            }
        }

        static void InitializeWorld()
        {
            npcHealth.Clear();
            activeMissions.Clear();

            ShowLevelIntro(level);

            for (int y = 0; y < worldHeight; y++)
            {
                for (int x = 0; x < worldWidth; x++)
                {
                    if (x == 0 || x == worldWidth - 1 || y == 0 || y == worldHeight - 1)
                    {
                        world[y, x] = wall;
                    }
                    else
                    {
                        world[y, x] = emptySpace;
                    }
                }
            }

            if (level == 1 && !activeMissions.Contains("Reach 100 gold"))
            {
                activeMissions.Add("Reach 100 gold");
            }

            bool inHouse = false;

            if (level == 2)
            {
                AddHouse();
                AddKey();
                inHouse = false;
            }
            else if (level == 3)
            {
                AddBasement();
                inHouse = true;
                activeMissions.Add("Posprzątaj dom przed wejściem do piwnicy");
                AddRandomObjects(item, 10);
            }
            else if (level == 4)
            {
                AddFinalNPC();
                activeMissions.Add("Idź do komputera w rogu");
            }

            if (level != 4 && level != 3)
            {
                AddRandomObjects(item, 10);
            }

            if (!inHouse && level != 4)
            {
                AddRandomNPCs(5);
            }
        }

        static void ShowLevelIntro(int level)
        {
            Console.Clear();
            string introText = "";
            switch (level)
            {
                case 1:
                    introText = "Pewnego dnia ojciec wysłał cię po warzywa na targ, jednak miałeś dziurawe kieszenie i zgubiłeś 100 złotych które dostałeś. Musisz znaleźć sposób na zarobienie pieniędzy i wrócić do domu.";
                    break;
                case 2:
                    introText = "Po ciężko wywalczonych zakupach, udałeś się do domu i zaraz przed nim przypomniałeś sobie, że w tej samej (dziurawej) kieszeni miałeś klucze, których oczywiście już nie masz. Znajdź klucz do domu.";
                    break;
                case 3:
                    introText = "Styrany wróciłeś do domu, już miałeś kierować się do swojego pokoju, jednak ojciec ci nie odpuszcza i każe sprzątać cały dom. Posprzątaj dom.";
                    break;
                case 4:
                    introText = "W końcu dostałeś się do swojej jamy. Włącz tego kompa i odpal fortnita.";
                    break;
            }
            Console.WriteLine(introText);
            Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey(true);
        }

        static void AddHouse()
        {
            int houseStartX = random.Next(5, worldWidth - 10);
            int houseStartY = random.Next(5, worldHeight - 10);

            for (int y = houseStartY; y < houseStartY + 4; y++)
            {
                for (int x = houseStartX; x < houseStartX + 6; x++)
                {
                    world[y, x] = house;
                }
            }

            world[houseStartY + 1, houseStartX] = emptySpace;
            activeMissions.Add("Znajdź klucz do domu");
        }

        static void AddBasement()
        {
            int basementStartX = random.Next(5, worldWidth - 10);
            int basementStartY = random.Next(5, worldHeight - 10);

            for (int y = basementStartY; y < basementStartY + 4; y++)
            {
                for (int x = basementStartX; x < basementStartX + 6; x++)
                {
                    world[y, x] = basement;
                }
            }

            world[basementStartY + 1, basementStartX] = emptySpace;
        }

        static void AddKey()
        {
            int x, y;
            do
            {
                x = random.Next(1, worldWidth - 1);
                y = random.Next(1, worldHeight - 1);
            } while (world[y, x] != emptySpace);

            world[y, x] = keyItem;
        }

        static void AddRandomObjects(char obj, int count)
        {
            int added = 0;
            while (added < count)
            {
                int x = random.Next(1, worldWidth - 1);
                int y = random.Next(1, worldHeight - 1);
                if (world[y, x] == emptySpace)
                {
                    world[y, x] = obj;
                    added++;
                }
            }
        }

        static void AddFinalNPC()
        {
            int npcX = worldWidth - 2;
            int npcY = worldHeight - 2;
            world[npcY, npcX] = npc;
        }

        static void AddRandomNPCs(int count)
        {
            int added = 0;
            while (added < count)
            {
                int x = random.Next(1, worldWidth - 1);
                int y = random.Next(1, worldHeight - 1);

                if (!(level == 3 && IsInBasementArea(x, y)))
                {
                    if (world[y, x] == emptySpace)
                    {
                        world[y, x] = npc;
                        npcHealth[new Tuple<int, int>(x, y)] = 3;
                        added++;
                    }
                }
            }
        }

        static bool IsInBasementArea(int x, int y)
        {
            int basementStartX = worldWidth / 2 - 3;
            int basementStartY = worldHeight / 2 - 2;
            int basementWidth = 6;
            int basementHeight = 4;

            return x >= basementStartX && x < basementStartX + basementWidth &&
                   y >= basementStartY && y < basementStartY + basementHeight;
        }

        static void DrawWorld()
        {
            Console.Clear();
            for (int y = 0; y < worldHeight; y++)
            {
                for (int x = 0; x < worldWidth; x++)
                {
                    if (x == playerX && y == playerY)
                    {
                        Console.Write(player);
                    }
                    else
                    {
                        Console.Write(world[y, x]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Poziom: {levelNames[level]}, Zdrowie: {playerHealth}, Punkty: {score}");
            Console.WriteLine("Misje aktywne:");
            foreach (var mission in activeMissions)
            {
                Console.WriteLine($"- {mission}");
            }
            Console.WriteLine("Wybór klawisza: [W/A/S/D]");
        }

        static void MovePlayer(ConsoleKey key)
        {
            int newX = playerX;
            int newY = playerY;

            switch (key)
            {
                case ConsoleKey.W: newY--; break;
                case ConsoleKey.S: newY++; break;
                case ConsoleKey.A: newX--; break;
                case ConsoleKey.D: newX++; break;
            }

            if (IsValidMove(newX, newY))
            {
                if (world[newY, newX] == item)
                {
                    world[newY, newX] = emptySpace;
                    score += 10;
                    Console.WriteLine("Znalazłeś przedmiot! Punkty: +10");
                }
                else if (world[newY, newX] == npc)
                {
                    HandleNPCInteraction(newX, newY);
                }
                else if (world[newY, newX] == keyItem)
                {
                    world[newY, newX] = emptySpace;
                    inventory.Add("Key");
                    Console.WriteLine("Znalazłeś klucz do domu!");
                }
                else if (world[newY, newX] == basement)
                {
                    if (inventory.Contains("Key"))
                    {
                        Console.WriteLine("Wszedłeś do piwnicy!");
                        level++;
                        InitializeWorld();
                    }
                    else
                    {
                        Console.WriteLine("Nie masz klucza do piwnicy!");
                    }
                }
                playerX = newX;
                playerY = newY;
            }
        }

        static void HandleNPCInteraction(int npcX, int npcY)
        {
            Console.WriteLine("Rozmawiasz z NPC...");
            if (npcHealth[new Tuple<int, int>(npcX, npcY)] > 0)
            {
                npcHealth[new Tuple<int, int>(npcX, npcY)]--;
                Console.WriteLine("NPC został zaatakowany! Zdrowie NPC: " + npcHealth[new Tuple<int, int>(npcX, npcY)]);
                if (npcHealth[new Tuple<int, int>(npcX, npcY)] <= 0)
                {
                    world[npcY, npcX] = emptySpace;
                    score += 20;
                    Console.WriteLine("NPC został pokonany! Punkty: +20");
                }
            }
        }

        static void UpdateNPCs()
        {
            // Możliwość dodania logiki dla NPC
        }

        static bool IsValidMove(int x, int y)
        {
            return x >= 0 && x < worldWidth && y >= 0 && y < worldHeight && world[y, x] != wall;
        }
    }
}
