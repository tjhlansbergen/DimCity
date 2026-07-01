using System.Data.Common;
using System.Numerics;
using Raylib_cs;

internal class DimGrid : DimView
{
    public Dictionary<int, DimGridItem> Items { get; private set; } = [];
    public int SelectedIndex { get; private set; } = 0;
    public string SelectedLabel => Items[SelectedIndex].Label;
    public int Columns { get; private set; }

    private Font menuFont;
    private Vector2 itemSize;

    public static readonly int padding = 4;
    const int margin = 4;

    internal DimGrid(Rect bounds, Console console, Font font, int columns = 3) : base(bounds, console)
    {
        menuFont = font;
        Columns = columns;

        itemSize = new Vector2((Bounds.size.X - ((columns + 1) * margin)) / columns, menuFont.BaseSize + (2 * padding) + (2 * margin));
        console.Write($"Grid mounted at {bounds}", debug: true);
    }

    internal void AddItem(string label, Action action, Action<Rect, bool> icon)
    {
        Items.Add(Items.Count, new DimGridItem(label, action, icon));
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
        int row = i / Columns;
        int col = i % Columns;

        return new Rect
        {
            position = new Vector2(Bounds.position.X + col * itemSize.X, Bounds.position.Y + row * itemSize.Y),
            size = new Vector2(itemSize.X, itemSize.Y)
        }.Shrink(margin);
    }
}

internal class DimGridItem
{
    public string Label { get; set; } = string.Empty;
    public Action<Rect, bool> Icon { get; set; } = (bounds, selected) => { };
    public Action? Action { get; private set; }

    internal DimGridItem(string Label, Action? Action, Action<Rect, bool> Icon)
    {     
        this.Label = Label;
        this.Action = Action;
        this.Icon = Icon;
    }

    internal void Draw(Rect bounds, Font font, bool selected)
    {
        var iconSize = bounds.size.Y - (2*DimGrid.padding);

        // tile
        DimLib.DrawRectWithOutline(bounds, Colors.GridItem, Colors.PinStripe, 1);
        
        // icon
        Icon(new Rect { 
            position = new Vector2(bounds.position.X + DimGrid.padding, bounds.position.Y + DimGrid.padding), 
            size = new Vector2(iconSize, iconSize) 
        }, selected);

        // label
        Raylib.DrawTextEx(font, Label, new Vector2(bounds.position.X + iconSize + (2* DimGrid.padding), bounds.position.Y + DimGrid.padding), DimWindow.fontSize, 1, selected ? Colors.MenuTextSelected : Colors.MenuText);
    }
}