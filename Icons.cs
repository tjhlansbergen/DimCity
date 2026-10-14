using Raylib_cs;
using System.Numerics;

internal static class Icons
{
    // Navigation
    internal static void UpArrow(Rect bounds, bool selected)
    {
        _chevron(bounds, selected, pointsUp: true);
    }

    internal static void DownArrow(Rect bounds, bool selected)
    {
        _chevron(bounds, selected, pointsUp: false);
    }

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

    // Terraform
    internal static void Water(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, selected ? Colors.PinStripe : Colors.PinStripeModerate, 1);
        _terraform(bounds, Colors.Water);
    }

    internal static void Mountains(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, selected ? Colors.PinStripe : Colors.PinStripeModerate, 1);
        _terraform(bounds, Colors.Mountains);
    }

    internal static void Forest(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, selected ? Colors.PinStripe : Colors.PinStripeModerate, 1);
        _terraform(bounds, Colors.Forest);
    }

    // Utilities
    internal static void Power(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, selected ? Colors.PinStripe : Colors.PinStripeModerate, 1);
        var color = Colors.Power;
        var points = new[]
        {
            new Vector2(bounds.position.X + bounds.size.X * 0.58f, bounds.position.Y + bounds.size.Y * 0.12f),
            new Vector2(bounds.position.X + bounds.size.X * 0.34f, bounds.position.Y + bounds.size.Y * 0.54f),
            new Vector2(bounds.position.X + bounds.size.X * 0.50f, bounds.position.Y + bounds.size.Y * 0.54f),
            new Vector2(bounds.position.X + bounds.size.X * 0.39f, bounds.position.Y + bounds.size.Y * 0.88f),
            new Vector2(bounds.position.X + bounds.size.X * 0.68f, bounds.position.Y + bounds.size.Y * 0.43f),
            new Vector2(bounds.position.X + bounds.size.X * 0.51f, bounds.position.Y + bounds.size.Y * 0.43f),
        };

        for (var i = 0; i < points.Length; i++)
        {
            Raylib.DrawLineEx(points[i], points[(i + 1) % points.Length], 3, color);
        }
    }

    // Zoning
    internal static void Residential(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, selected ? Colors.PinStripe : Colors.PinStripeModerate, 1);
        var color = Colors.Residential;
        var left = bounds.position.X + bounds.size.X * 0.22f;
        var right = bounds.position.X + bounds.size.X * 0.78f;
        var roof = bounds.position.Y + bounds.size.Y * 0.40f;
        var top = bounds.position.Y + bounds.size.Y * 0.40f;
        var bottom = bounds.position.Y + bounds.size.Y * 0.82f;
        var center = bounds.position.X + bounds.size.X / 2;

        Raylib.DrawLineEx(new Vector2(left, roof), new Vector2(center, bounds.position.Y + bounds.size.Y * 0.16f), 3, color);
        Raylib.DrawLineEx(new Vector2(center, bounds.position.Y + bounds.size.Y * 0.16f), new Vector2(right, roof), 3, color);
        DimLib.DrawRect(new Rect
        {
            position = new Vector2(left, top),
            size = new Vector2(right - left, bottom - top)
        }, color);
        DimLib.DrawRect(new Rect
        {
            position = new Vector2(center - bounds.size.X * 0.08f, bottom - bounds.size.Y * 0.22f),
            size = new Vector2(bounds.size.X * 0.16f, bounds.size.Y * 0.22f)
        }, Colors.Background);
    }

    internal static void Commercial(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, selected ? Colors.PinStripe : Colors.PinStripeModerate, 1);
        var left = bounds.position.X + bounds.size.X * 0.25f;
        var top = bounds.position.Y + bounds.size.Y * 0.16f;
        var width = bounds.size.X * 0.50f;
        var height = bounds.size.Y * 0.68f;
        DimLib.DrawRect(new Rect
        {
            position = new Vector2(left, top),
            size = new Vector2(width, height)
        }, Colors.Commercial);

        var windowSize = bounds.size.X * 0.10f;
        for (var row = 0; row < 3; row++)
        {
            for (var column = 0; column < 2; column++)
            {
                DimLib.DrawRect(new Rect
                {
                    position = new Vector2(left + bounds.size.X * (0.10f + column * 0.22f), top + bounds.size.Y * (0.12f + row * 0.17f)),
                    size = new Vector2(windowSize, windowSize)
                }, Colors.Background);
            }
        }
    }

    internal static void Industrial(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, selected ? Colors.PinStripe : Colors.PinStripeModerate, 1);
        var color = Colors.Industrial;
        var left = bounds.position.X + bounds.size.X * 0.15f;
        var top = bounds.position.Y + bounds.size.Y * 0.47f;
        var bottom = bounds.position.Y + bounds.size.Y * 0.82f;
        var right = bounds.position.X + bounds.size.X * 0.85f;

        DimLib.DrawRect(new Rect
        {
            position = new Vector2(left, top),
            size = new Vector2(right - left, bottom - top)
        }, color);
        DimLib.DrawRect(new Rect
        {
            position = new Vector2(bounds.position.X + bounds.size.X * 0.23f, bounds.position.Y + bounds.size.Y * 0.24f),
            size = new Vector2(bounds.size.X * 0.12f, bounds.size.Y * 0.30f)
        }, color);
        Raylib.DrawLineEx(
            new Vector2(left, top),
            new Vector2(bounds.position.X + bounds.size.X * 0.35f, bounds.position.Y + bounds.size.Y * 0.35f),
            3,
            color);
        Raylib.DrawLineEx(
            new Vector2(bounds.position.X + bounds.size.X * 0.35f, bounds.position.Y + bounds.size.Y * 0.35f),
            new Vector2(bounds.position.X + bounds.size.X * 0.48f, top),
            3,
            color);
        Raylib.DrawLineEx(
            new Vector2(bounds.position.X + bounds.size.X * 0.48f, top),
            new Vector2(bounds.position.X + bounds.size.X * 0.62f, bounds.position.Y + bounds.size.Y * 0.35f),
            3,
            color);
        Raylib.DrawLineEx(
            new Vector2(bounds.position.X + bounds.size.X * 0.62f, bounds.position.Y + bounds.size.Y * 0.35f),
            new Vector2(right, top),
            3,
            color);
    }

    private static void _transport(Rect bounds, Color color)
    {
        const int thickness = 6;

        DimLib.DrawRect(new Rect
        {
            position = new Vector2(bounds.position.X + 1, bounds.position.Y + bounds.size.Y / 2 - thickness / 2),
            size = new Vector2(bounds.size.X - 2, thickness)
        }, color);
    }

    private static void _terraform(Rect bounds, Color color)
    {
        const int padding = 4;

        DimLib.DrawRect(new Rect
        {
            position = new Vector2(bounds.position.X + padding, bounds.position.Y + padding),
            size = new Vector2(bounds.size.X - padding * 2, bounds.size.Y - padding * 2)
        }, color);
    }

    private static void _chevron(Rect bounds, bool selected, bool pointsUp)
    {
        var centerX = bounds.position.X + bounds.size.X / 2;
        var topY = bounds.position.Y + bounds.size.Y * (pointsUp ? 0.30f : 0.70f);
        var bottomY = bounds.position.Y + bounds.size.Y * (pointsUp ? 0.70f : 0.30f);
        var leftX = bounds.position.X + bounds.size.X * 0.20f;
        var rightX = bounds.position.X + bounds.size.X * 0.80f;
        var color = selected ? Colors.MenuTextSelected : Colors.PinStripeModerate;

        Raylib.DrawLineEx(new Vector2(leftX, bottomY), new Vector2(centerX, topY), 1.5f, color);
        Raylib.DrawLineEx(new Vector2(centerX, topY), new Vector2(rightX, bottomY), 1.5f, color);
    }

    internal static void Bulldoze(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, selected ? Colors.PinStripe : Colors.PinStripeModerate, 1);
        float padding = 4f;
        float thickness = 2f;

        // diagonal line from top-left to bottom-right
        Raylib.DrawLineEx(
            new Vector2(bounds.position.X + padding, bounds.position.Y + padding),
            new Vector2(bounds.position.X + bounds.size.X - padding, bounds.position.Y + bounds.size.Y - padding),
            thickness,
            Colors.Bulldoze);

        // diagonal line from bottom-left to top-right
        Raylib.DrawLineEx(
            new Vector2(bounds.position.X + padding, bounds.position.Y + bounds.size.Y - padding),
            new Vector2(bounds.position.X + bounds.size.X - padding, bounds.position.Y + padding),
            thickness,
            Colors.Bulldoze);
    }

    internal static void Select(Rect bounds, bool selected)
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