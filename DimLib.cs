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
}

internal static class DimLib
{
    internal static void DrawRect(Rect rect, Color color)
    {
        Raylib.DrawRectangleV(rect.position, rect.size, color);
    }

    internal static void DrawRectWithOutline(Rect rect, Color fillColor, Color outlineColor, int outlineThickness)
    {
        Raylib.DrawRectangleV(rect.position, rect.size, fillColor);
        Raylib.DrawRectangleLinesEx(new Rectangle(rect.position.X, rect.position.Y, rect.size.X, rect.size.Y), outlineThickness, outlineColor);
    }
}