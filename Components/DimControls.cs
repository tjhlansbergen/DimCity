using System.Numerics;
using Raylib_cs;

internal class DimControls : DimView
{
    private readonly DimMenu menu;
    private readonly Dictionary<string, DimGrid> tabs = new();
    const int padding = 6;

    internal DimControls(Rect bounds, Console console, Font font) : base(bounds, console)
    {
        var split = Enum.GetNames<Enumerations.BuildingCategory>().Max(label => Raylib.MeasureText(label, 20)) + padding;

        tabs = BuildTabs(bounds, console, font, split);
        
        menu = new DimMenu(new Rect
        {
            position = new Vector2(bounds.position.X + padding, bounds.position.Y + padding),
            size = new Vector2(split, bounds.size.Y - 2 * padding)
        }, 
        console,
        font,
        tabs.ToDictionary(e => e.Key, e => (Action)(() => {console.Write($"Selected {e.Key}"); }))
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
        tabs[menu.SelectedLabel].Draw();
    }

    internal void Click(Vector2 mousePosition)
    {
        if (menu.Bounds.Contains(mousePosition))
        {
            menu.Click(mousePosition);
        }

        tabs[menu.SelectedLabel].Click(mousePosition);
    }

    internal static Dictionary<string, DimGrid> BuildTabs(Rect bounds, Console console, Font font, int split)
    {
        var result = new Dictionary<string, DimGrid>();

        var itemBounds = new Rect
        {
            position = new Vector2(bounds.position.X + split + 2 * padding, bounds.position.Y + padding),
            size = new Vector2(bounds.size.X - split - 3 * padding, bounds.size.Y - 2 * padding)
        };

        foreach (var label in Enum.GetValues<Enumerations.BuildingCategory>())
        {


            switch (label)
            {          
                case Enumerations.BuildingCategory.Transportation:
                    result.Add(Enum.GetName(label)!, new Transportation(itemBounds, console, font));
                    break;
                case Enumerations.BuildingCategory.Utilities:
                    result.Add(Enum.GetName(label)!, new DimGrid(itemBounds, console, font, new()));
                    break;
                case Enumerations.BuildingCategory.Zoning:
                    result.Add(Enum.GetName(label)!, new DimGrid(itemBounds, console, font, new()));
                    break;
            }
        }

        return result;
    }
}