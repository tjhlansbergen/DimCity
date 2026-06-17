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
}

internal static class DimLib
{
    internal static void DrawRect(Rect rect, Color color)
    {
        Raylib.DrawRectangleV(rect.position, rect.size, color);
    }
}