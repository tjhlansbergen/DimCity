using System.Numerics;
using Raylib_cs;

internal class DimConsole : DimView
{
    private static Font consoleFont;
    private const int maxLines = 6;

    private int index = 0;  // counterintuatively this counts from the 'bottom' 

    internal DimConsole(Rect bounds, Console console, Font font) : base(bounds, console)
    {
        consoleFont = font;
        console.Write($"Console mounted at {bounds}", debug: true);
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
        Raylib.DrawTextEx(consoleFont, string.Join("\n", Console.Read(index, maxLines)), textPosition, 20, 1, Colors.ConsoleText);

    }
}

