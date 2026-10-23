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
        DimLib.DrawRect(Bounds, Colors.Panel, outline: true);
        Raylib.DrawTextEx(Font, Title, new Vector2(Bounds.position.X + padding, Bounds.position.Y + padding), DimWindow.fontSize, 1, Colors.ConsoleText);
        
        Menu.Draw();
    }
}