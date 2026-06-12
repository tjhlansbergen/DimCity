using Raylib_cs;

internal static class Program
{


    public static void Main()
    {

        Raylib.InitWindow(1300, 700, "DimCity");
        Raylib.ToggleBorderlessWindowed();

        while (!Raylib.WindowShouldClose())
        {
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
            x = 10,
            y = 100,
            width = 1,
            height = 1
        };

        DimLib.DrawRect(bounds, Color.FromHSV(140, 1.00f, 1.00f));
    }

    static void DrawConsole()
    {
        var width = Raylib.GetScreenWidth();
        var height = Raylib.GetScreenHeight();

        var console = new Rect
        {
            x = 3,
            y = height - 150,
            width = width - 6,
            height = 147
        };
        var consoleTextArea = new Rect
        {
            x = console.x + 6,
            y = console.y + 6,
            width = console.width - 12,
            height = console.height - 12
        };

        DimLib.DrawRect(console, Color.FromHSV(0, 0, 0.15f));
        
        Raylib.DrawText("DimCity", consoleTextArea.width - 60, consoleTextArea.y + consoleTextArea.height - 20, 20, Color.Black);
        
        Raylib.DrawText("Hi there", consoleTextArea.x, consoleTextArea.y, 12, Color.FromHSV(218, 0.80f, 0.89f));
        
    }
}