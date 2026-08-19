using System.Numerics;
using Raylib_cs;

internal class DimMap : DimView
{
    public Vector2 Offset { get; set; } = new(0, 0);
    private int Zoom { get; set; } = 1;

    private const int minZoom = 1;
    private const int maxZoom = 6;
  
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
        for (int x = 1; x < 6; x++)
        {

            var bounds = new Rect
            {
                position = new Vector2(x * 32 + Offset.X, 50 + Offset.Y),
                size = new Vector2(Zoom*4, Zoom*4)
            };


            // placeholder
            DimLib.DrawRect(bounds, Color.FromHSV(140, 1.00f, 1.00f));
            
        }

    }

    public void ZoomIn()
    {
        if (Zoom < maxZoom) Zoom++;
    }

    public void ZoomOut()
    {
        if (Zoom > minZoom) Zoom--;
    }
}

