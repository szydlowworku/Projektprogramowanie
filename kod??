//Poruszanie się postaci po ekranie przez input z klawiatury
public class Player

    private Dictionary<ConsoleKey, Point> directions;
    public Player Player(ConsoleKey key, Point direction)
    {
        directions = new Dictionary<ConsoleKey, Point>()
        {
            [ConsoleKey.A] = new Point(-1,0)
            [ConsoleKey.W] = new Point(0,-1)
            [ConsoleKey.S] = new Point(0,1)
            [ConsoleKey.D] = new Point(1,0)

        };
    }
     protected override Point GetDirection()
    {
    	ConsoleKeyInfo keyInfo = Console.ReadKey(true);
		Point direction = directions.GetValueOrDefault(keyInfo.Key, new Point(0, 0));
        
		return direction;
    }
}

//Ograniczenie poruszania się np. przez nieprzenikalne ściany

protected Point GetDirection()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        Point direction = directions.GetValueOrDefault(keyInfo.Key, new Point(0, 0));
        return direction;
    }

//sprawdzic czy pozycja jest dozwolona (nieprzenikalne sciany)
    public void Move()
    {
        Point direction = GetDirection();
        Point newPosition = new Point(Position.X + direction.X, Position.Y + direction.Y);

        if (IsPositionCorrect(newPosition))
        {
            Position = newPosition;
        }
    }
//sprawdzic czy pozycja jest w granicach mapy
// zamiast w ma byc znak odpowiadajacy za pole do poruszania sie
// zamiast z ma byc znak oznaczajacy sciane
    private bool IsPositionCorrect(Point newPosition)
    {
        if (newPosition.X < 0 || newPosition.X >= map.GetLength(w) ||
            newPosition.Y < 0 || newPosition.Y >= map.GetLength(z))
        {
            return false; 
        }

        return map[newPosition.X, newPosition.Y] == 0;  
    }
}

//Losowe rozmieszczenie przedmiotów i NPC
// enemy  dziedziczy wszystko z klasy character (trzeba zrobic kod na character)
public class Enemy : Character
{
    private Random rng;

	public Enemy(string visuals, Point position) : base(visuals, position)
	{
        rng = new Random();
	}

    protected override Point GetDirection()
    {
        return new Point(rng.Next(-1, 2), rng.Next(-1, 2));
    }
}
public class Item
{
    public int Name {get; private set;}
    public Point Position {get; private set; }

    public Item(int name, Point position)
    {
        Name = name;
        Position = position;
    }
}
public class ItemSpawner
{
    private Random rng;
    private int[,] map;
    private List<Item> items;

    public ItemSpawner(int[,] map)
    {
        this.map = map;
        rng = new Random();
        items = new List<Item>();
    }

    public void SpawnItem(string itemName)
    {
        Point spawnPosition;
        do
        {
            int x = rng.Next(0, map.GetLength(0));
            int y = rng.Next(0, map.GetLength(1));
            spawnPosition = new Point(x, y);
        }
        while (!IsPositionValid(spawnPosition));

        Item newItem = new Item(itemName, spawnPosition);
        items.Add(newItem);
    }

    private bool IsPositionValid(Point position)
    {
        if (position.X < 0 || position.X >= map.GetLength(0) ||
            position.Y < 0 || position.Y >= map.GetLength(1))
        {
            return false;  // Pozycja poza granicami mapy
        }

        return map[position.X, position.Y] == 0;  // Pozycja dozwolona, jeśli nie jest ścianą
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
