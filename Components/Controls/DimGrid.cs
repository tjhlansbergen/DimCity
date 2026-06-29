using System.Numerics;
using Raylib_cs;

internal class DimGrid : DimView
{
    public Dictionary<int, DimGridItem> Items { get; private set; } = [];
    public int SelectedIndex { get; private set; } = 0;
    public string SelectedLabel => Items[SelectedIndex].Label;

    private Font menuFont;
    private int itemHeight => menuFont.BaseSize + 6;

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
            Items[i].Draw(new Vector2(Bounds.position.X, Bounds.position.Y + i * itemHeight), menuFont, i == SelectedIndex);
        }
    }

    internal void Click(Vector2 mousePosition)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            var itemBounds = new Rect
            {
                position = new Vector2(Bounds.position.X, Bounds.position.Y + i * itemHeight),
                size = new Vector2(Bounds.size.X, itemHeight)
            };

            if (itemBounds.Contains(mousePosition))
            {
                SelectedIndex = i;
                Items[i].Action?.Invoke();
                break;
            }
        }
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

    internal void Draw(Vector2 position, Font font, bool selected)
    {
        // todo: draw icon
        Raylib.DrawTextEx(font, Label, new Vector2(position.X + 8, position.Y), 20, 1, selected ? Colors.MenuTextSelected : Colors.MenuText);
    }
}