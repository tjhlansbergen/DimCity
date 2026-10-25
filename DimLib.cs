using Raylib_cs;
using System.Numerics;

internal struct Rect
{
    internal Vector2 position, size;

    internal readonly bool Contains(Vector2 point)
    {
        return point.X >= position.X && point.X <= position.X + size.X && point.Y >= position.Y && point.Y <= position.Y + size.Y;
    }

    public readonly override string ToString()
    {
        return $"Position: ({position.X}, {position.Y}), Size: ({size.X}, {size.Y})";
    }

    internal readonly Rect Shrink(int padding)
    {
        return new Rect
        {
            position = new Vector2(position.X + padding, position.Y + padding),
            size = new Vector2(size.X - (2*padding), size.Y - (2*padding)),
        };
    }

    internal readonly Rect Move(Vector2 offset)
    {
        return new Rect
        {
            position = new Vector2(position.X + offset.X, position.Y + offset.Y),
            size = size,
        };
    }

    internal readonly bool Intersects(Rect tileBounds)
    {
        return !(tileBounds.position.X > position.X + size.X || 
                 tileBounds.position.X + tileBounds.size.X < position.X || 
                 tileBounds.position.Y > position.Y + size.Y ||
                 tileBounds.position.Y + tileBounds.size.Y < position.Y);
    }

    internal readonly Vector2 Center()
    {
        return new Vector2(position.X + (size.X / 2), position.Y + (size.Y / 2));
    }
}

internal static class DimLib
{
    internal static void DrawRect(Rect rect, Color color, bool outline = false)
    {
        Raylib.DrawRectangleV(rect.position, rect.size, color);
        if (outline)
        {
            Raylib.DrawRectangleLinesEx(new Rectangle(rect.position.X, rect.position.Y, rect.size.X, rect.size.Y), 1, Colors.PinStripe);
        }
    }

    internal static void DrawRectWithOutline(Rect rect, Color fillColor, Color outlineColor, int outlineThickness)
    {
        if (fillColor != Colors.Background)
        {
            Raylib.DrawRectangleV(rect.position, rect.size, fillColor);    
        }
        
        Raylib.DrawRectangleLinesEx(new Rectangle(rect.position.X, rect.position.Y, rect.size.X, rect.size.Y), outlineThickness, outlineColor);        
    }

    internal static void DrawCircle(Rect rect, Color fillColor)
    {
        Raylib.DrawCircleV(rect.Center(), (rect.size.X / 2) -1, fillColor);
    }

    internal static IEnumerable<Vector2> LineTiles(Vector2 start, Vector2 end)
    {
        var x = (int)start.X;
        var y = (int)start.Y;
        var endX = (int)end.X;
        var endY = (int)end.Y;
        var deltaX = Math.Abs(endX - x);
        var stepX = x < endX ? 1 : -1;
        var deltaY = -Math.Abs(endY - y);
        var stepY = y < endY ? 1 : -1;
        var error = deltaX + deltaY;

        while (true)
        {
            yield return new Vector2(x, y);
            if (x == endX && y == endY) yield break;

            var doubledError = 2 * error;
            if (doubledError >= deltaY)
            {
                error += deltaY;
                x += stepX;
            }
            if (doubledError <= deltaX)
            {
                error += deltaX;
                y += stepY;
            }
        }
    }

    internal static IEnumerable<Vector2> RectangleTiles(Vector2 start, Vector2 end)
    {
        var minX = (int)Math.Min(start.X, end.X);
        var maxX = (int)Math.Max(start.X, end.X);
        var minY = (int)Math.Min(start.Y, end.Y);
        var maxY = (int)Math.Max(start.Y, end.Y);

        for (var x = minX; x <= maxX; x++)
        {
            for (var y = minY; y <= maxY; y++)
            {
                yield return new Vector2(x, y);
            }
        }
    }
}