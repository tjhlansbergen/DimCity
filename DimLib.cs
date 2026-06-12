using Raylib_cs;
using System.Numerics;

internal struct Rect { internal Vector2 position, size; }

internal static class DimLib
{
    internal static void DrawRect(Rect rect, Color color)
    {
        Raylib.DrawRectangleV(rect.position, rect.size, color);
    }
}