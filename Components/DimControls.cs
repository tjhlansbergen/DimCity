using System.Numerics;
using Raylib_cs;

internal class DimControls : DimView
{
    public DimMenu Menu { get; private set; }
    public Dictionary<string, DimGrid> Tabs { get; private set; } = [];
    const int padding = 6;

    internal DimControls(Rect bounds, Console console, Font font, Action<Enumerations.BuildingType> onSelectionChanged) : base(bounds, console)
    {
        var split = Enum.GetNames<Enumerations.BuildingCategory>().Max(label => Raylib.MeasureText(label, 20)) + padding;

        Tabs = BuildTabs(bounds, console, font, split, onSelectionChanged);

        Menu = new DimMenu(new Rect
        {
            position = new Vector2(bounds.position.X + padding, bounds.position.Y + padding),
            size = new Vector2(split, bounds.size.Y - 2 * padding)
        },
        console,
        font,
        Tabs.ToDictionary(e => e.Key, e => (Action)(() => { console.Write($"Selected {(e.Key == "^" ? "Home" : e.Key)}"); }))
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

        Menu.Draw();
        Tabs[Menu.SelectedLabel].Draw();
    }

    internal void Click(Vector2 mousePosition)
    {
        if (Menu.Bounds.Contains(mousePosition))
        {
            Menu.Click(mousePosition);
        }

        Tabs[Menu.SelectedLabel].Click(mousePosition);
    }

    internal static Dictionary<string, DimGrid> BuildTabs(Rect bounds, Console console, Font font, int split, Action<Enumerations.BuildingType> _onSelectionChanged)
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
                case Enumerations.BuildingCategory.None:
                    result.Add("^", new None(itemBounds, console, font, _onSelectionChanged));
                    break;
                case Enumerations.BuildingCategory.Terraform:
                    result.Add(Enum.GetName(label)!, new Terraform(itemBounds, console, font, _onSelectionChanged));
                    break;
                case Enumerations.BuildingCategory.Transportation:
                    result.Add(Enum.GetName(label)!, new Transportation(itemBounds, console, font, _onSelectionChanged));
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