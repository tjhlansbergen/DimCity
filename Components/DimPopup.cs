using System.Numerics;
using Raylib_cs;

internal class DimPopup : DimView
{
    public bool Active { get; set; } = false;
    internal DimMenu Menu { get; private set; }
    internal string Title { get; private set; }
    internal Font Font { get; private set; }

    private Action<string>? _callback;

    const int padding = 12;
    const int verticalMargin = 30;
    const int buttonSpacing = 8;
    const int buttonPadding = 8;

    internal DimPopup(Rect bounds, Console console, Font font, string title, SortedSet<string> items) : base(bounds, console)
    {
        console.Write($"Popup mounted at {bounds}", debug: true);

        Title = title;
        Font = font;
        Menu = new DimMenu(new Rect
        {
            position = new Vector2(bounds.position.X + padding, bounds.position.Y + padding + verticalMargin),
            size = new Vector2(bounds.size.X - 2 * padding, bounds.size.Y - 2 * padding - (verticalMargin * 2))
        },
        console,
        font,
        items
        );
    }

    public void Show(Action<string> callback)
    {
        Active = true;
        _callback = callback;
    }

    internal override void Draw()
    {
        // 'window' 
        DimLib.DrawRect(Bounds, Colors.Panel, outline: true);

        // title
        Raylib.DrawTextEx(Font, Title, new Vector2(Bounds.position.X + padding, Bounds.position.Y + padding), DimWindow.fontSize, 1, Colors.ConsoleText);
        
        // options
        Menu.Draw();

        // cancel and ok buttons
        var cancelTextSize = Raylib.MeasureTextEx(Font, "Cancel", DimWindow.fontSize, 1);
        var okTextSize = Raylib.MeasureTextEx(Font, "OK", DimWindow.fontSize, 1);
        var buttonHeight = Font.BaseSize + 2 * buttonPadding;
        var okWidth = okTextSize.X + 2 * buttonPadding;
        var cancelWidth = cancelTextSize.X + 2 * buttonPadding;
        var buttonY = Bounds.position.Y + Bounds.size.Y - padding - buttonHeight;
        var okBounds = new Rect
        {
            position = new Vector2(Bounds.position.X + Bounds.size.X - padding - okWidth, buttonY),
            size = new Vector2(okWidth, buttonHeight)
        };
        var cancelBounds = new Rect
        {
            position = new Vector2(okBounds.position.X - buttonSpacing - cancelWidth, buttonY),
            size = new Vector2(cancelWidth, buttonHeight)
        };

        DrawButton("Cancel", cancelBounds, cancelTextSize);
        DrawButton("OK", okBounds, okTextSize);
    }

    private void DrawButton(string label, Rect bounds, Vector2 textSize)
    {
        DimLib.DrawRect(bounds, Colors.Panel, outline: true);
        Raylib.DrawTextEx(
            Font,
            label,
            new Vector2(bounds.position.X + buttonPadding, bounds.position.Y + (bounds.size.Y - textSize.Y) / 2),
            DimWindow.fontSize,
            1,
            Colors.MenuText);
    }

    internal void Click(Vector2 mousePosition)
    {
        if (Menu.Bounds.Contains(mousePosition))
        {
            Menu.Click(mousePosition);
            return;
        }

        var cancelTextSize = Raylib.MeasureTextEx(Font, "Cancel", DimWindow.fontSize, 1);
        var okTextSize = Raylib.MeasureTextEx(Font, "OK", DimWindow.fontSize, 1);
        var buttonHeight = Font.BaseSize + 2 * buttonPadding;
        var okWidth = okTextSize.X + 2 * buttonPadding;
        var cancelWidth = cancelTextSize.X + 2 * buttonPadding;
        var buttonY = Bounds.position.Y + Bounds.size.Y - padding - buttonHeight;
        var okBounds = new Rect
        {
            position = new Vector2(Bounds.position.X + Bounds.size.X - padding - okWidth, buttonY),
            size = new Vector2(okWidth, buttonHeight)
        };
        var cancelBounds = new Rect
        {
            position = new Vector2(okBounds.position.X - buttonSpacing - cancelWidth, buttonY),
            size = new Vector2(cancelWidth, buttonHeight)
        };

        if (cancelBounds.Contains(mousePosition))
        {
            Active = false;
            return;
        }

        if (okBounds.Contains(mousePosition))
        {
            Active = false;
            _callback?.Invoke(Menu.SelectedLabel);
            return;
        }
    }
}