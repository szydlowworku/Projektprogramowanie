[Serializable]
internal class WindowToSmallToDrawException : Exception
{
    public Point ExpectedSize { get; private set;}

    public WindowToSmallToDrawException(Point expectedSize) : base($"Minimum required window size is ({expectedSize.X}, {expectedSize.Y})")
    {
        ExpectedSize = expectedSize;
    }
}