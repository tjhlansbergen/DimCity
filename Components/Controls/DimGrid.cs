using System.Numerics;
using Raylib_cs;

internal class DimGrid : DimView
{
    public Dictionary<int, DimGridItem> Items { get; private set; } = [];
    public int SelectedIndex { get; private set; } = 0;
    public string SelectedLabel => Items[SelectedIndex].Label;

    private Font menuFont;
    private int itemHeight => menuFont.BaseSize + (2*padding);

    internal static readonly int padding = 10;

    internal DimGrid(Rect bounds, Console console, Font font, Dictionary<string, Action> items) : base(bounds, console)
    {
        menuFont = font;
        foreach (var kvp in items)
        {
            Items.Add(Items.Count, new DimGridItem(kvp.Key, kvp.Value));
        }

        console.Write($"Grid mounted at {bounds}", debug: true);
    }

    internal void AddItem(string label, Action action)
    {
        Items.Add(Items.Count, new DimGridItem(label, action));
    }

    internal override void Draw()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].Draw(ItemBounds(i), menuFont, i == SelectedIndex);
        }
    }

    internal void Click(Vector2 mousePosition)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (ItemBounds(i).Contains(mousePosition))
            {
                SelectedIndex = i;
                Items[i].Action?.Invoke();
                break;
            }
        }
    }

    private Rect ItemBounds(int i)
    {
        return new Rect
        {
            position = new Vector2(Bounds.position.X, Bounds.position.Y + i * itemHeight),
            size = new Vector2(Bounds.size.X, itemHeight)
        };
    }
}

internal class DimGridItem
{
    public string Label { get; set; } = string.Empty;
    public Action? Action { get; private set; }

    internal DimGridItem(string Label, Action? Action)
    {     
        this.Label = Label;
        this.Action = Action;
    }

    internal void Draw(Rect bounds, Font font, bool selected)
    {
        // todo: draw icon
        DimLib.DrawRectWithOutline(bounds.Shrink(DimGrid.padding), Colors.GridItem, Colors.PinStripe, 1);
        Raylib.DrawTextEx(font, Label, new Vector2(bounds.position.X + 8, bounds.position.Y), 20, 1, selected ? Colors.MenuTextSelected : Colors.MenuText);
    }
}