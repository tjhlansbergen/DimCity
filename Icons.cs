using Raylib_cs;
using System.Numerics;

internal static class Icons
{
    // Transportation
    internal static void Rail(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, selected ? Colors.PinStripe : Colors.PinStripeModerate, 1);
        _transport(bounds, Colors.Rail);
    }

    internal static void Road(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, selected ? Colors.PinStripe : Colors.PinStripeModerate, 1);
        _transport(bounds, Colors.Road);
    }

    private static void _transport(Rect bounds, Color color)
    {
        int thickness = 6;

        DimLib.DrawRect(new Rect
        {
            position = new Vector2(bounds.position.X + 1, bounds.position.Y + bounds.size.Y / 2 - thickness / 2),
            size = new Vector2(bounds.size.X - 2, thickness)
        }, color);
    }


    internal static void Selection(Rect bounds, bool selected)
    {


        // dashed outline
        var outlineColor = selected ? Colors.PinStripe : Colors.PinStripeModerate;
        float dashLen = 6f;
        float gapLen = 4f;
        float thickness = 2f;

        float left = bounds.position.X + 0.5f;
        float top = bounds.position.Y + 0.5f;
        float right = bounds.position.X + bounds.size.X - 0.5f;
        float bottom = bounds.position.Y + bounds.size.Y - 0.5f;

        // top edge
        for (float x = left; x < right; x += dashLen + gapLen)
        {
            float x2 = System.Math.Min(x + dashLen, right);
            Raylib.DrawLineEx(new Vector2(x, top), new Vector2(x2, top), thickness, outlineColor);
        }
        // bottom edge
        for (float x = left; x < right; x += dashLen + gapLen)
        {
            float x2 = System.Math.Min(x + dashLen, right);
            Raylib.DrawLineEx(new Vector2(x, bottom), new Vector2(x2, bottom), thickness, outlineColor);
        }
        // left edge
        for (float y = top; y < bottom; y += dashLen + gapLen)
        {
            float y2 = System.Math.Min(y + dashLen, bottom);
            Raylib.DrawLineEx(new Vector2(left, y), new Vector2(left, y2), thickness, outlineColor);
        }
        // right edge
        for (float y = top; y < bottom; y += dashLen + gapLen)
        {
            float y2 = System.Math.Min(y + dashLen, bottom);
            Raylib.DrawLineEx(new Vector2(right, y), new Vector2(right, y2), thickness, outlineColor);
        }
    }
}