using System.Numerics;
using Raylib_cs;

internal class DimMap : DimView
{
    public Vector2 Offset { get; set; } = new(0, 0);
  
    internal DimMap(Rect bounds, Console console) : base(bounds, console)
    {
        console.Write($"Map mounted at {bounds}", debug: true);
    }

    internal override void Draw()
    {
        DrawTile();     // temporary, just to show that the map is moving when you click and drag
    }
       
    private void DrawTile()
    {
        var bounds = new Rect
        {
            position = new Vector2(10 + Offset.X, 100 + Offset.Y),
            size = new Vector2(10, 10)
        };

        DimLib.DrawRect(bounds, Color.FromHSV(140, 1.00f, 1.00f));

    }
}

