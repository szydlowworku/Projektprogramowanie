
abstract class Character
{
	public string Visuals { get; private set; }
	public Point Position { get; private set; }
	public Point PreviousPosition { get; private set; }

	public Character(string visuals, Point position)
	{
		Visuals = visuals;
		Position = new Point(position.X, position.Y);
		PreviousPosition = new Point(Position.X, Position.Y);
	}

	public void Display()
	{
		Console.SetCursorPosition(Position.X, Position.Y);
		Console.Write(Visuals);
	}

	public Point GetNextPosition()
	{
		Point point = new Point(Position.X, Position.Y);
        Point direction = GetDirection();
        point.X += direction.X;
        point.Y += direction.Y;

		return point;
	}

    public abstract Point GetDirection();

    public void MoveTo(int targetX, int targetY)
	{
		PreviousPosition.X = Position.X;
		PreviousPosition.Y = Position.Y;

		Position.X = targetX;
		Position.Y = targetY;
	}
}