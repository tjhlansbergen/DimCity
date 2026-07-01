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
}