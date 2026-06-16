using System.Numerics;
using Raylib_cs;

internal class DimConsole
{
    private static Font consoleFont;

    private readonly Queue<string> buffer;

    public Rect Bounds { get; private set;}

    internal DimConsole(Rect bounds)
    {
        Bounds = bounds;
        consoleFont = Raylib.LoadFontEx("resources/NotoSansMono-Regular.ttf", 20, null, 0);
        buffer = new Queue<string>(10);
    }

    internal void Draw()
    {
        var consolePanel = new Rect
        {
            position = Bounds.position,
            size = Bounds.size
        };

        DimLib.DrawRect(consolePanel, Color.FromHSV(0, 0, 0.15f));

        var label = "DimCity";
        var labelPosition = new Vector2(consolePanel.size.X - Raylib.MeasureText(label, 20), consolePanel.position.Y + consolePanel.size.Y - 24);
        var textPosition = new Vector2(consolePanel.position.X + 6, consolePanel.position.Y + 6);

        Raylib.DrawTextEx(Raylib.GetFontDefault(), label, labelPosition, 20, 1, Color.Black);
        Raylib.DrawTextEx(consoleFont, string.Join("\n", [.. buffer]), textPosition, 20, 1, Color.FromHSV(218, 0.80f, 0.89f));

    }

    public void Write(string text)
    {
        buffer.Enqueue(text);
        if (buffer.Count > 10)
        {
            buffer.Dequeue();
        }
    }
}

