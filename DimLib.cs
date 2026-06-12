using Raylib_cs;

internal struct Rect { public int x, y, width, height; }

internal static class DimLib
{
    internal static void DrawRect(Rect rect, Color color)
    {
        Raylib.DrawRectangle(rect.x, rect.y, rect.width, rect.height, color);
    }
}