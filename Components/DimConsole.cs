using System.Numerics;
using Raylib_cs;

internal class DimConsole : DimView
{
    private static Font consoleFont;
    private const int maxLines = 6;

   
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
        var labelPosition = new Vector2(consolePanel.position.X + consolePanel.size.X - Raylib.MeasureText(label, DimWindow.fontSize), consolePanel.position.Y + consolePanel.size.Y - 24);
        var textPosition = new Vector2(consolePanel.position.X + 6, consolePanel.position.Y + 6);

        Raylib.DrawTextEx(Raylib.GetFontDefault(), label, labelPosition, DimWindow.fontSize, 1, Color.Black);
        Raylib.DrawTextEx(consoleFont, string.Join("\n", Console.Read(maxLines)), textPosition, DimWindow.fontSize, 1, Colors.ConsoleText);

    }
}

