using System.Numerics;
using Raylib_cs;

internal class DimMap : DimView
{
    public Vector2 Offset { get; set; } = new(0, 0);
    private int Zoom { get; set; } = minZoom;

    private Dictionary<Vector2, Tile> tiles;
    private readonly Dictionary<Vector2, (bool Existed, Tile? Tile)> lastChangedTiles = [];
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
            // get tile bounds
            var tileBounds = new Rect
            {
                position = new Vector2(tileWithLocation.Key.X * Zoom + Offset.X, tileWithLocation.Key.Y * Zoom + Offset.Y),
                size = new Vector2(Zoom, Zoom)
            };

            // check if the tile is within the visible bounds of the map
            if (!Bounds.Intersects(tileBounds)) continue;

            // draw
            DrawTile(tileBounds, tileWithLocation.Value);
        }
    }

    private static void DrawTile(Rect tileBounds, Tile tile)
    {
        //todo: move these out of here

        if (BuildingTypes.CategoryForBuildingType(tile.BuildingType) == BuildingTypes.BuildingCategory.Zoning)
        {
            DimLib.DrawRectWithOutline(
                tileBounds,
                fillColor: Colors.Background,
                outlineColor: BuildingTypes.ColorForBuildingType(tile.BuildingType),
                1
            );
            return;
        }

        if (tile.BuildingType == BuildingTypes.BuildingType.Power)
        {
            DimLib.DrawCircle(tileBounds, BuildingTypes.ColorForBuildingType(tile.BuildingType));
            return;
        }

        if (tile.BuildingType == BuildingTypes.BuildingType.Rail)
        {
            DimLib.DrawRect(tileBounds.Shrink(1), BuildingTypes.ColorForBuildingType(tile.BuildingType));    
            return;
        }

        DimLib.DrawRect(tileBounds, BuildingTypes.ColorForBuildingType(tile.BuildingType));
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
            SetTile(position, new Tile { BuildingType = selectedBuildingType });

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
            overlay[position] = color;
        }
    }

    internal void BeginBuild(BuildingTypes.BuildingType selectedBuildingType, Vector2 mousePosition)
    {
        BeginUndoAction();
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
                SetTile(position, new Tile { BuildingType = activeBuildType.Value });
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
            RemoveTile(position);
        }
    }

    internal void BeginUndoAction()
    {
        lastChangedTiles.Clear();
    }

    internal bool Undo()
    {
        if (lastChangedTiles.Count == 0) return false;

        foreach (var (position, original) in lastChangedTiles)
        {
            if (original.Existed)
            {
                tiles[position] = original.Tile!;
            }
            else
            {
                tiles.Remove(position);
            }
        }

        lastChangedTiles.Clear();
        return true;
    }

    private void SetTile(Vector2 position, Tile tile)
    {
        RecordTileBeforeChange(position);
        tiles[position] = tile;
    }

    private void RemoveTile(Vector2 position)
    {
        RecordTileBeforeChange(position);
        tiles.Remove(position);
    }

    private void RecordTileBeforeChange(Vector2 position)
    {
        if (lastChangedTiles.ContainsKey(position)) return;

        var existed = tiles.TryGetValue(position, out var tile);
        lastChangedTiles[position] = (existed, existed ? new Tile { BuildingType = tile!.BuildingType } : null);
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
