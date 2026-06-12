using System.Numerics;
using Raylib_cs;

internal static class Program
{
    private static readonly Console console = new();
    private static Font consoleFont;
    private static Vector2 offset = new(0, 0);



    public static void Main()
    {
        console.Write("Welcome to DimCity!");
        console.Write("Click and hold to pan around.");

        Raylib.InitWindow(1300, 700, "DimCity");
        Raylib.ToggleBorderlessWindowed();
        consoleFont = Raylib.LoadFontEx("resources/NotoSansMono-Regular.ttf", 20, null, 0);

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                offset += Raylib.GetMouseDelta();
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.FromHSV(0, 0, 0.25f));

            DrawTile();

            DrawConsole();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    static void DrawTile()
    {
        var bounds = new Rect
        {
            position = new Vector2(10 + offset.X, 100 + offset.Y),
            size = new Vector2(10, 10)
        };

        DimLib.DrawRect(bounds, Color.FromHSV(140, 1.00f, 1.00f));

    }

    static void DrawConsole()
    {
        var width = Raylib.GetScreenWidth();
        var height = Raylib.GetScreenHeight();

        var consolePanel = new Rect
        {
            position = new Vector2(3, height - 150),
            size = new Vector2(width - 6, 147)
        };

        DimLib.DrawRect(consolePanel, Color.FromHSV(0, 0, 0.15f));

        var label = "DimCity";
        var labelPosition = new Vector2(consolePanel.size.X - Raylib.MeasureText(label, 20), consolePanel.position.Y + consolePanel.size.Y - 24);
        var textPosition = new Vector2(consolePanel.position.X + 6, consolePanel.position.Y + 6);

        Raylib.DrawTextEx(Raylib.GetFontDefault(), label, labelPosition, 20, 1, Color.Black);
        Raylib.DrawTextEx(consoleFont, string.Join("\n", console.Read()), textPosition, 20, 1, Color.FromHSV(218, 0.80f, 0.89f));

    }
}