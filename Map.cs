
using System.Runtime.Serialization;

class Map
{
    string[] mapData = new string[]{
        "#######################################",
        "#.....................................#",
        "#....................#...#...#...#....#",
        "#.......#.............................#",
        "#.....#####...........#################",
        "#.......#.............................#",
        "#.....................##########......#",
        "#.....................................#",
        "#######################################",
    };
    
    private Point origin = new Point(0, 0);
    
    public void Display(Point mapOrigin)
    {
        int sizeY = mapData.Length;
        int sizeX = mapData[0].Length;

        foreach (string row in mapData)
        {
            if (row.Length > sizeX)
            {
                sizeX = row.Length;
            }
        }

        int drawingWidth = sizeX + mapOrigin.X;
        int drawingHeight = sizeY + mapOrigin.Y;

        if (drawingWidth >= Console.BufferWidth || drawingHeight >= Console.BufferHeight)
        {
            throw new WindowToSmallToDrawException(new Point(drawingWidth, drawingHeight));
        }
        
        origin = mapOrigin;
        Console.CursorTop = mapOrigin.Y;
        foreach (string row in mapData)
        {
            Console.CursorLeft = mapOrigin.X;
            Console.WriteLine(row);
        }
    }

    internal void DrawSomethingAt(string visuals, Point position)
    {
        SetCursorPositionWithOrigin(position);
        Console.Write(visuals);
    }

    private void SetCursorPositionWithOrigin(Point position)
    {
        int x = position.X + origin.X;
        int y = position.Y + origin.Y;

        Console.SetCursorPosition(x, y);
    }

    public void RedrawCell(Point position)
    {
        char cell = GetCellAt(position);

        SetCursorPositionWithOrigin(position);
        Console.Write(cell);
    }

    private char GetCellAt(Point position)
    {
        string row = mapData[position.Y];
        char cell = row[position.X];
        return cell;
    }

    internal bool IsPositionCorrect(Point position)
    {
        if (position.Y >= 0 && position.Y < mapData.Length
            && position.X >= 0 && position.X < mapData[position.Y].Length)
        {
            char cell = GetCellAt(position);
            if (cell != '#')
            {
                if (cell == '¤')
                {
                    cell = '.';
                    Console.WriteLine("gówno");
                }

                return true;
            }
        }

        return false;
    }
}