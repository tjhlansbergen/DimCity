using System.Numerics;
using Raylib_cs;

internal class DimMap : DimView
{
    public Vector2 Offset { get; set; } = new(0, 0);
    private int Zoom { get; set; } = minZoom;

    private const int minZoom = 4;
    private const int maxZoom = 32;
  
    internal DimMap(Rect bounds, Console console) : base(bounds, console)
    {
        console.Write($"Map mounted at {bounds}", debug: true);
    }

    internal override void Draw()
    {
        DrawTiles();
    }
       
    private void DrawTiles()
    {
        // todo: move
        var tiles = new DimCity().Tiles;

        foreach (var tile in tiles)
        {
            var bounds = new Rect
            {
                position = new Vector2(tile.Key.X * Zoom + Offset.X, tile.Key.Y * Zoom + Offset.Y),
                size = new Vector2(Zoom, Zoom)
            };

            // placeholder
            DimLib.DrawRect(bounds, Colors.Water);
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

