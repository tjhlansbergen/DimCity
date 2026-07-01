using Raylib_cs;

internal static class Icons
{
    // Transportation

    internal static void Rail(Rect bounds, bool selected)
    {
        DimLib.DrawRectWithOutline(bounds, Colors.Background, Colors.Rail, 1);
    }
}