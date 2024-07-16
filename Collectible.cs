class Collectible
{
    public string Visuals { get; private set; }
    public Point Position { get; private set; }

    public Collectible(string visuals, Point position)
    {
        Visuals = visuals;
        Position = position;
    }
}
