using Raylib_cs;

namespace DimCity;

internal static class Program
{
    struct Rect
    {
        public int x, y, width, height;
    }

    public static void Main()
    {
        
        Raylib.InitWindow(1300, 700, "DimCity");
        Raylib.ToggleBorderlessWindowed();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.FromHSV(0, 0, 0.25f));

            Raylib.DrawText("DimCity", 12, 12, 20, Color.Black);


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
            
            Raylib.DrawRectangle(console.x, console.y, console.width, console.height, Color.FromHSV(0, 0, 0.15f));
            Raylib.DrawText("Hi there", consoleTextArea.x, consoleTextArea.y, 12, Color.FromHSV(218, 0.80f, 0.89f));


            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}