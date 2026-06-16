using System.Numerics;
using Raylib_cs;

internal class DimControls : DimView
{
    private readonly DimMenu menu;

    internal DimControls(Rect bounds, Font font) : base(bounds)
    {
        menu = new DimMenu(new Rect
        {
            position = new Vector2(bounds.position.X + 6, bounds.position.Y + 6),
            size = new Vector2(bounds.size.X - 12, bounds.size.Y - 12)
        }, font);
    }

    internal override void Draw()
    {
        var controlsPanel = new Rect
        {
            position = Bounds.position,
            size = Bounds.size
        };

        DimLib.DrawRect(controlsPanel, Colors.Panel);
        menu.Draw();
    }
}