using System.Numerics;
using Raylib_cs;

internal static class Program
{
    private static DimConsole? console;
    private static DimControls? controls;
    private static Vector2 offset = new(0, 0);



    public static void Main()
    {


        Raylib.InitWindow(1300, 700, "DimCity");
        Raylib.ToggleBorderlessWindowed();

        var width = Raylib.GetScreenWidth();
        var height = Raylib.GetScreenHeight();
        var margin = 6;

        console = new DimConsole(new Rect
        {
            position = new Vector2(margin, height - 150),
            size = new Vector2(width / 2 - margin - (margin / 2), 150 - margin)
        });

        controls = new DimControls(new Rect
        {
            position = new Vector2(width / 2 + margin / 2, height - 150),
            size = new Vector2(width / 2 - margin - (margin / 2), 150 - margin)
        });

        console.Write("Welcome to DimCity!");
        console.Write("Click and hold to pan around.");

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                offset += Raylib.GetMouseDelta();
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.FromHSV(0, 0, 0.25f));

            DrawTile();

            console.Draw();
            controls.Draw();

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

    
}