class Enemy : Character
{
    private Random rng;

	public Enemy(string visuals, Point position) : base(visuals, position)
	{
        rng = new Random();
	}

    public override Point GetDirection()
    {
        return new Point(rng.Next(-1, 2), rng.Next(-1, 2));
    }
}