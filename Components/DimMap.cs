using System.Numerics;

internal class DimMap : DimView
{
    public Vector2 Offset { get; set; } = new(0, 0);
    private int Zoom { get; set; } = minZoom;

    private Dictionary<Vector2, Tile> tiles;

    private const int minZoom = 4;
    private const int maxZoom = 32;

    internal DimMap(Rect bounds, Console console, DimCity city) : base(bounds, console)
    {
        tiles = city.Tiles;
        console.Write($"Map mounted at {bounds}", debug: true);
    }

    internal override void Draw()
    {
        DrawTiles();
    }

    private void DrawTiles()
    {
        foreach (var tileWithLocation in tiles)
        {
            // check for building type
            if (!tileWithLocation.Value.BuildingType.HasValue) continue;

            // get tile bounds
            var tileBounds = new Rect
            {
                position = new Vector2(tileWithLocation.Key.X * Zoom + Offset.X, tileWithLocation.Key.Y * Zoom + Offset.Y),
                size = new Vector2(Zoom, Zoom)
            };

            // check if the tile is within the visible bounds of the map
            if (!Bounds.Intersects(tileBounds)) continue; 

            // draw
            DimLib.DrawRect(tileBounds, BuildingTypes.ColorForBuildingType(tileWithLocation.Value.BuildingType.Value));
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

    internal void Build(BuildingTypes.BuildingType selectedBuildingType, Vector2 mousePosition)
    {
        var position = MouseToTile(mousePosition);
        if (PositionFree(position))
        {
            tiles[position] = new Tile { BuildingType = selectedBuildingType };
        }

        // todo: utilities should build by creating lines between clicks
    }

    internal void Bulldoze(Vector2 mousePosition)
    {
        var position = MouseToTile(mousePosition);
        if (!PositionFree(position))
        {
            tiles.Remove(position);
        }
    }

    private Vector2 MouseToTile(Vector2 mousePosition)
    {
        var tileX = (int)((mousePosition.X - Offset.X) / Zoom);
        var tileY = (int)((mousePosition.Y - Offset.Y) / Zoom);
        return new Vector2(tileX, tileY);
    }

    private bool PositionFree(Vector2 position)
    {
        return !tiles.ContainsKey(position);
    }
}

