using System.Numerics;
using Raylib_cs;


internal class DimWindow
{
    internal int Width { get; private set; }
    internal int Height { get; private set; }

    internal Console console = new(6);

    internal DimConsole consolePanel;
    internal DimControls controlsPanel;
    private static Vector2 offset = new(0, 0);
    private static Font notoSansMono;
    private const int horizon = 150;

    internal DimWindow(bool fullscreen)
    {
        Raylib.InitWindow(1600, 900, "DimCity");
        if (fullscreen)
        {
            Raylib.ToggleFullscreen();
        }

        // this needs to happen after the window is initialized
        notoSansMono = Raylib.LoadFontEx("resources/NotoSansMono-Regular.ttf", 20, null, 0);

        Width = Raylib.GetScreenWidth();
        Height = Raylib.GetScreenHeight();

        var margin = 6;
        var bottomPanelSize = new Vector2(Width / 2 - margin - (margin / 2), horizon - margin);

        consolePanel = new DimConsole(new Rect
        {
            position = new Vector2(Width / 2 + margin / 2, Height - horizon),
            size = bottomPanelSize,
        }, console, notoSansMono);

        controlsPanel = new DimControls(new Rect
        {
            position = new Vector2(margin, Height - horizon),
            size = bottomPanelSize,
        }, console, notoSansMono);

        console.Write(string.Empty);
        console.Write("Welcome to DimCity!");
        console.Write("Click and hold to pan around.");
    }

    internal void Write(string message)
    {
        console.Write(message);
    }

    internal void Draw()
    {
        while (!Raylib.WindowShouldClose())
        {
            DispatchInput();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Colors.Background);

            DrawTile();

            controlsPanel.Draw();
            consolePanel.Draw();


            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private void DispatchInput()
    {
        // single click
        if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            if (Raylib.GetMouseX() < Width / 2)
            {
                HandleControlsInput();
            }
        }

        // click 'n drag
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            if (Raylib.GetMouseY() < Height - horizon)
            {
                HandleMapInput();
            }
        }
    }

    private void HandleMapInput()
    {
        offset += Raylib.GetMouseDelta();
    }

    private void HandleControlsInput()
    {
        controlsPanel?.Click(Raylib.GetMousePosition());
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