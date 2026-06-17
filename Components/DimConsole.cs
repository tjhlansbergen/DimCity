using System.Numerics;
using Raylib_cs;

internal class DimConsole : DimView
{
    private static Font consoleFont;
    private readonly Queue<string> buffer;

    internal DimConsole(Rect bounds, Font font, int bufferSize) : base(bounds)
    {
        consoleFont = font;
        buffer = new Queue<string>(bufferSize);
    }

    internal override void Draw()
    {
        var consolePanel = new Rect
        {
            position = Bounds.position,
            size = Bounds.size
        };

        DimLib.DrawRect(consolePanel, Colors.Panel);

        var label = "DimCity";
        var labelPosition = new Vector2(consolePanel.position.X + consolePanel.size.X - Raylib.MeasureText(label, 20), consolePanel.position.Y + consolePanel.size.Y - 24);
        var textPosition = new Vector2(consolePanel.position.X + 6, consolePanel.position.Y + 6);

        Raylib.DrawTextEx(Raylib.GetFontDefault(), label, labelPosition, 20, 1, Color.Black);
        Raylib.DrawTextEx(consoleFont, string.Join("\n", [.. buffer]), textPosition, 20, 1, Colors.ConsoleText);

    }

    public void Write(string text)
    {
        buffer.Enqueue(text);
        if (buffer.Count >= buffer.Capacity)
        {
            buffer.Dequeue();
        }
    }
}

