class Door
{
    public string Visuals { get; private set; }
    public Point Position { get; private set; }

    public Door(string visuals, Point position)
    {
        Visuals = visuals;
        Position = position;
    }
}