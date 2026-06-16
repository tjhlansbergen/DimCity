using Raylib_cs;

internal class DimMenu : DimView
{
    private Font menuFont;

    internal DimMenu(Rect bounds, Font font) : base(bounds)
    {
        menuFont = font;
    }

    internal override void Draw()
    {
        Raylib.DrawTextEx(menuFont, "Zoning", Bounds.position, 20, 1, Colors.MenuText);

    }
}