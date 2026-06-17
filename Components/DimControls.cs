using System.Numerics;
using Raylib_cs;

internal class DimControls : DimView
{
    private readonly DimMenu menu;

    internal DimControls(Rect bounds, Console console, Font font) : base(bounds, console)
    {
        const int padding = 6;

        var labels = Enum.GetValues<Enumerations.BuildingCategory>().Select(e => e.ToString());
        var width = labels.Max(label => Raylib.MeasureText(label, 20)) + padding;

        menu = new DimMenu(new Rect
        {
            position = new Vector2(bounds.position.X + padding, bounds.position.Y + padding),
            size = new Vector2(width, bounds.size.Y - 2 * padding)
        }, 
        console,
        font,
        labels.ToDictionary(e => e, e => (Action)(() => { }))
        );

        console.Write($"Controls mounted at {bounds}", debug: true);
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

    internal void Click(Vector2 mousePosition)
    {
        if (menu.Bounds.Contains(mousePosition))
        {
            menu.Click(mousePosition);
        }
    }
}