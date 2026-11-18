using System.Numerics;
using Raylib_cs;
using static BuildingTypes;

internal class DimMap : DimView
{
    public Vector2 Offset { get; set; } = new(0, 0);
    private int Zoom { get; set; } = Constants.minZoom;

    private Dictionary<Vector2, Tile> tiles;
    private readonly Dictionary<Vector2, (bool Existed, Tile? Tile)> lastChangedTiles = [];
    private Dictionary<Vector2, Color> overlay;
    private BuildingTypes.BuildingType? activeBuildType;
    private Vector2 buildStart;



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
            tileWithLocation.Value.Draw(tileBounds, activeBuildType);
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
        if (Zoom < Constants.maxZoom) Zoom++;
    }

    public void ZoomOut()
    {
        if (Zoom > Constants.minZoom) Zoom--;
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

        IEnumerable<Vector2> shape = BuildingTypes.LinearBuildingCategories().Contains(category) 
            ? DimLib.LineTiles(buildStart, end) 
            : DimLib.RectangleTiles(buildStart, end);

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
                // zoning can only build on empty tiles.
                if (CategoryForBuildingType(activeBuildType.Value) != BuildingCategory.Zoning 
                || PositionFree(position))
                {
                    SetTile(position, new Tile { BuildingType = activeBuildType.Value });
                }

                // apply power to adjacent tiles if the building is a power plant
                if (activeBuildType.Value == BuildingTypes.BuildingType.Power)
                {
                    foreach (var adjacent in DimLib.ManhattenTiles(position, Constants.powerRadius))
                    {
                        if (tiles.TryGetValue(adjacent, out var adjacentTile))
                        {
                            adjacentTile.HasPower = true;       
                        }
                        else
                        {
                            tiles.Add(adjacent, new Tile { BuildingType = BuildingTypes.BuildingType.Select, HasPower = true });
                        }
                    }
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
            if (tiles[position].BuildingType == BuildingTypes.BuildingType.Power)
            {
                // remove power from adjacent tiles if the building is a power plant
                foreach (var adjacent in DimLib.ManhattenTiles(position, Constants.powerRadius))
                {
                    if (tiles.TryGetValue(adjacent, out var adjacentTile))
                    {
                        adjacentTile.HasPower = false;       
                    }
                }
            }

            tiles[position].BuildingType = BuildingTypes.BuildingType.Select;
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
