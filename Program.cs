using System;

public class Program
{
	public static void Main()
	{
		Random random = new Random();
		Console.ResetColor();
		Console.CursorVisible = false;
		Console.Clear();
		Player hero = new Player("▄", new Point(random.Next(1, 8), random.Next(1, 4)));
        Enemy troll = new Enemy("█", new Point(random.Next(14, 18), random.Next(5, 8)));
		Collectible key = new Collectible("¤", new Point(20, 5));
		Door door= new Door("-", new Point(37,2));

		bool hasKey = false;

		Map map = new Map();
        Point mapOrigin = new Point(3, 5);

		int round = 1;

		try
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			map.Display(mapOrigin);
			Console.ForegroundColor = ConsoleColor.Green;
			map.DrawSomethingAt(hero.Visuals, hero.Position);
			Console.ForegroundColor = ConsoleColor.Red;
			map.DrawSomethingAt(troll.Visuals, troll.Position);
			Console.ForegroundColor = ConsoleColor.Yellow;
			map.DrawSomethingAt(door.Visuals, door.Position);
			Console.ResetColor();
			map.DrawSomethingAt(key.Visuals, key.Position);

			bool coinCollected = false;

			while (true)
			{
				Point nextPosition = hero.GetNextPosition();

				if (map.IsPositionCorrect(nextPosition))
				{
					Console.ForegroundColor = ConsoleColor.Green;
					hero.MoveTo(nextPosition.X, nextPosition.Y);
					map.RedrawCell(hero.PreviousPosition);
					map.DrawSomethingAt(hero.Visuals, hero.Position);
					Console.ResetColor();
				}

				if (hero.Position.X == door.Position.X && hero.Position.Y == door.Position.Y && hasKey)
				{
					Console.SetCursorPosition(2, 4);
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.WriteLine("dobra nara spierdalam");
					Console.ResetColor();
					break; 
				}

				if (true) 
				{
					Console.SetCursorPosition(2, 2);
					Console.WriteLine("has key: " + hasKey);
					Console.SetCursorPosition(2, 3);
					Console.WriteLine("Round: " + round);
				}

				int distanceX = Math.Abs(hero.Position.X - troll.Position.X);
				int distanceY = Math.Abs(hero.Position.Y - troll.Position.Y);

				if ((distanceX == 1 && distanceY == 0) || (distanceX == 0 && distanceY == 1))
				{
					Console.SetCursorPosition(2, 0);
					Console.WriteLine("o kurwa diabeł");
					Console.ReadKey(true);
				}
				else
				{
					Console.SetCursorPosition(2, 0);
					Console.WriteLine("                                 ");
				}
				
				nextPosition = troll.GetNextPosition();
				if (map.IsPositionCorrect(nextPosition))
				{
					troll.MoveTo(nextPosition.X, nextPosition.Y);
					map.RedrawCell(troll.PreviousPosition);
					Console.ForegroundColor = ConsoleColor.Red;
					map.DrawSomethingAt(troll.Visuals, troll.Position);
					Console.ResetColor();
				}

				distanceX = Math.Abs(hero.Position.X - troll.Position.X);
				distanceY = Math.Abs(hero.Position.Y - troll.Position.Y);

				if (hero.Position.X == key.Position.X && hero.Position.Y == key.Position.Y && !coinCollected)
                {
                    coinCollected = true;
                    Console.SetCursorPosition(2, 0);
                    Console.WriteLine("masz tu śrubke wkręć se w dupke");
					Console.ForegroundColor = ConsoleColor.Green;
                    map.RedrawCell(key.Position);
					map.DrawSomethingAt(hero.Visuals, key.Position);
					hasKey = true;
					Console.ResetColor();
                }

				if ((distanceX == 1 && distanceY == 0) || (distanceX == 0 && distanceY == 1))
				{
					Console.SetCursorPosition(2, 1);
					Console.WriteLine("jak ja ciebie jebne");
				}
				else
				{
					Console.SetCursorPosition(2, 1);
					Console.WriteLine("                                 ");
				}

				if ((distanceX == 0 && distanceY == 0) || (distanceX == 0 && distanceY == 0)) 
				{
					Console.SetCursorPosition(2, 1);
					Console.WriteLine("np umarło ci sie sory");
					break;
				}

	

				round++;
			}
		}
		catch (WindowToSmallToDrawException ex)
		{
			// Console.WriteLine($"Minimum required window size is ({ex.ExpectedSize.X}, {ex.ExpectedSize.Y})");
			Console.WriteLine(ex.Message);
			Console.WriteLine("Terminal window is to small to draw map. Make it bigger and restart the game.");
			Console.WriteLine("Press any key to close...");
            Console.ReadKey(true);
		}
		
	}
}
