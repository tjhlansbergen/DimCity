using System.Numerics;
using Raylib_cs;

internal class DimConsole : DimView
{
    private static Font consoleFont;
    private const int maxLines = 6;
    private int offset = 0;

   
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

        // Draw the console text
        var textPosition = new Vector2(consolePanel.position.X + 6, consolePanel.position.Y + 6);
        Raylib.DrawTextEx(consoleFont, string.Join("\n", Console.Read(maxLines, offset)), textPosition, DimWindow.fontSize, 1, Colors.ConsoleText);

        // draw the up and down arrows on top of the text
        var size = 16;
        var margin = 6;    
        Icons.UpArrow(new Rect
        {
            position = new Vector2(consolePanel.position.X + consolePanel.size.X - size - margin, consolePanel.position.Y + margin),
            size = new Vector2(size, size)
        }, false);
        Icons.DownArrow(new Rect
        {
            position = new Vector2(consolePanel.position.X + consolePanel.size.X - size - margin, consolePanel.position.Y + consolePanel.size.Y - size - margin),
            size = new Vector2(size, size)
        }, false);
        
        
    }

    internal void Click(Vector2 mousePosition)
    {
        offset = Math.Clamp(offset + (mousePosition.Y - Bounds.position.Y < Bounds.size.Y / 2 ? 1 : -1), 0 - maxLines + 1, Console.Count - maxLines);
    }
}

