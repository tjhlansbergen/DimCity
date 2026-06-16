using System.Numerics;
using Raylib_cs;


internal class DimWindow
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    private static DimConsole? console;
    private static DimControls? controls;
    private static Vector2 offset = new(0, 0);

    internal DimWindow()
    {
        // nothing to see here yet
    }

    internal void Draw()
    {
        Raylib.InitWindow(1300, 700, "DimCity");
        Raylib.ToggleBorderlessWindowed();

        Width = Raylib.GetScreenWidth();
        Height = Raylib.GetScreenHeight();

        var margin = 6;
        var bottomPanelSize = new Vector2(Width / 2 - margin - (margin / 2), 150 - margin);

        controls = new DimControls(new Rect
        {
            position = new Vector2(margin, Height - 150),
            size = bottomPanelSize,
        });

        console = new DimConsole(new Rect
        {
            position = new Vector2(Width / 2 + margin / 2, Height - 150),
            size = bottomPanelSize,
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

            controls.Draw();
            console.Draw();


            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private static void DrawTile()
    {
        var bounds = new Rect
        {
            position = new Vector2(10 + offset.X, 100 + offset.Y),
            size = new Vector2(10, 10)
        };

        DimLib.DrawRect(bounds, Color.FromHSV(140, 1.00f, 1.00f));

    }
}