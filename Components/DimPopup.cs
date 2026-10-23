using System.Numerics;
using Raylib_cs;

internal class DimPopup : DimView
{
    internal DimMenu Menu { get; private set; }
    internal string Title { get; private set; }
    internal Font Font { get; private set; }

    private Action<string>? _callback;
    private DimButton CancelButton { get; }
    private DimButton OkButton { get; }

    const int padding = 12;
    const int verticalMargin = 30;
    const int buttonSpacing = 8;

    internal DimPopup(Rect bounds, Console console, Font font, string title, SortedSet<string> items, Action<string> callback) : base(bounds, console)
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

        _callback = callback;

        var buttonBottom = bounds.position.Y + bounds.size.Y - padding;
        OkButton = new DimButton("OK", font, bounds.position.X + bounds.size.X - padding, buttonBottom);
        CancelButton = new DimButton("Cancel", font, OkButton.Bounds.position.X - buttonSpacing, buttonBottom);
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
        CancelButton.Draw();
        OkButton.Draw();
    }

    internal void Click(Vector2 mousePosition)
    {
        if (Menu.Bounds.Contains(mousePosition))
        {
            Menu.Click(mousePosition);
            return;
        }

        if (CancelButton.Bounds.Contains(mousePosition))
        {
            _callback?.Invoke(string.Empty);
            return;
        }

        if (OkButton.Bounds.Contains(mousePosition))
        {
            _callback?.Invoke(Menu.SelectedLabel);
            return;
        }
    }
}