using System.Numerics;
using Raylib_cs;

internal class DimMenu : DimView
{
    public List<DimMenuItem> MenuItems { get; private set; } = [];
    public int SelectedIndex { get; private set; } = 0;

    private Font menuFont;
    private int itemHeight => menuFont.BaseSize + 6;

    internal DimMenu(Rect bounds, Font font, Dictionary<string, Action> items) : base(bounds)
    {
        menuFont = font;
        foreach (var kvp in items)
        {
            MenuItems.Add(new DimMenuItem(kvp.Key, kvp.Value));
        }
    }

    internal void AddItem(string label, Action action)
    {
        MenuItems.Add(new DimMenuItem(label, action));
    }

    internal override void Draw()
    {
        for (int i = 0; i < MenuItems.Count; i++)
        {
            MenuItems[i].Draw(new Vector2(Bounds.position.X, Bounds.position.Y + i * itemHeight), menuFont, i == SelectedIndex);
        }

        DimLib.DrawRect(new Rect { position = new Vector2(Bounds.position.X + Bounds.size.X - 1, Bounds.position.Y + 4), size = new Vector2(1, Bounds.size.Y - 8) }, Colors.PinStripe);
    }

    internal void Click(Vector2 mousePosition)
    {
        for (int i = 0; i < MenuItems.Count; i++)
        {
            var itemBounds = new Rect
            {
                position = new Vector2(Bounds.position.X, Bounds.position.Y + i * itemHeight),
                size = new Vector2(Bounds.size.X, itemHeight)
            };

            if (itemBounds.Contains(mousePosition))
            {
                SelectedIndex = i;
                MenuItems[i].Action?.Invoke();
                break;
            }
        }
    }
    
}

internal class DimMenuItem
{
    public string Label { get; set; } = string.Empty;
    public Action? Action { get; private set; }

    internal DimMenuItem(string Label, Action? Action)
    {     
        this.Label = Label;
        this.Action = Action;
    }

    internal void Draw(Vector2 position, Font font, bool selected)
    {
        DimLib.DrawRect(new Rect { position = position, size = new Vector2(3, font.BaseSize) }, selected ? Colors.MenuItemSelected : Colors.Panel);
        Raylib.DrawTextEx(font, Label, new Vector2(position.X + 8, position.Y), 20, 1, selected ? Colors.MenuTextSelected : Colors.MenuText);
    }
}