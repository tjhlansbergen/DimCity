using System.Numerics;
using Raylib_cs;

internal class DimMap : DimView
{
    public Vector2 Offset { get; set; } = new(0, 0);
    private int Zoom { get; set; } = minZoom;

    private Dictionary<Vector2, Tile> tiles;
    private Dictionary<Vector2, Color> overlay;
    private BuildingTypes.BuildingType? activeBuildType;
    private Vector2 buildStart;

    private const int minZoom = 4;
    private const int maxZoom = 32;

    internal DimMap(Rect bounds, Console console, DimCity city) : base(bounds, console)
    {
        tiles = city.Tiles;
        overlay = [];
        console.Write($"Map mounted at {bounds}", debug: true);
    }

    internal override void Draw()
    {
        DrawTiles();
        DrawOverlay();
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

    private void DrawOverlay()
    {
        foreach (var pixel in overlay)
        {
            // get bounds
            var pixelBounds = new Rect
            {
                position = new Vector2(pixel.Key.X * Zoom + Offset.X, pixel.Key.Y * Zoom + Offset.Y),
                size = new Vector2(Zoom, Zoom)
            };
           
            // check if the pixel is within the visible bounds of the map
            if (!Bounds.Intersects(pixelBounds)) continue; 

            // draw
            DimLib.DrawRect(pixelBounds, pixel.Value);
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
        var category = BuildingTypes.CategoryForBuildingType(selectedBuildingType);
        if (category == BuildingTypes.BuildingCategory.Terraform)
        {
            var position = MouseToTile(mousePosition);
            if (PositionFree(position))
            {
                tiles[position] = new Tile { BuildingType = selectedBuildingType };
            }

            return;
        }

        if (activeBuildType != selectedBuildingType) return;

        var end = MouseToTile(mousePosition);
        overlay.Clear();

        IEnumerable<Vector2> shape = category switch
        {
            BuildingTypes.BuildingCategory.Utilities or BuildingTypes.BuildingCategory.Transportation => DimLib.LineTiles(buildStart, end),
            BuildingTypes.BuildingCategory.Zoning => DimLib.RectangleTiles(buildStart, end),
            _ => []
        };

        var color = BuildingTypes.ColorForBuildingType(selectedBuildingType);
        foreach (var position in shape)
        {
            if (PositionFree(position))
            {
                overlay[position] = color;
            }
        }
    }

    internal void BeginBuild(BuildingTypes.BuildingType selectedBuildingType, Vector2 mousePosition)
    {
        overlay.Clear();
        if (BuildingTypes.CategoryForBuildingType(selectedBuildingType) == BuildingTypes.BuildingCategory.Terraform)
        {
            activeBuildType = null;
            return;
        }

        activeBuildType = selectedBuildingType;
        buildStart = MouseToTile(mousePosition);
        Build(selectedBuildingType, mousePosition);
    }

    internal void CommitBuild()
    {
        if (activeBuildType.HasValue)
        {
            foreach (var position in overlay.Keys)
            {
                if (PositionFree(position))
                {
                    tiles[position] = new Tile { BuildingType = activeBuildType.Value };
                }
            }
        }

        activeBuildType = null;
        overlay.Clear();
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
