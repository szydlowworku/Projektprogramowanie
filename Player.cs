class Player : Character
{
	private Dictionary<ConsoleKey, Point> directions;
	public Point respawnPosition;

	public Player(string visuals, Point position) : base(visuals, position)
	{
		respawnPosition = new Point(position.X, position.Y);
		directions = new Dictionary<ConsoleKey, Point>()
		{
			[ConsoleKey.D] = new Point(1, 0),
			[ConsoleKey.A] = new Point(-1, 0),
			[ConsoleKey.W] = new Point(0, -1),
			[ConsoleKey.S] = new Point(0, 1),
		};
	}

    public override Point GetDirection()
    {
    	ConsoleKeyInfo keyInfo = Console.ReadKey(true);
		Point direction = directions.GetValueOrDefault(keyInfo.Key, new Point(0, 0));
        
		return direction;
    }

	public void Respawn()
	{
		MoveTo(respawnPosition.X, respawnPosition.Y);
	}
}