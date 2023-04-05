using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework;
using System.Linq;

namespace DimCity;

public interface IStateService
{
    int Size { get; }
    Orientation Direction { get; }
    int Zoom { get; }
    Point Cursor { get; }
    Point View { get; }
    bool MenuVisible { get; set; }
    Menu GameMenu { get; set; }

    string GetTileTextureName(Point coords);
    void MoveCursor(int byX, int by);
    void MoveView(int byX, int by);
    void RotateLeft();
    void RotateRight();
    void ZoomIn();
    void ZoomOut();
    void Build();
    void SaveGame();
}

public class StateService : IStateService
{
    private DimGame _game;
    private State _state;

    public Menu GameMenu { get; set; }

    public int Size { get => _state.Size; set => _state.Size = value; }
    public Orientation Direction { get => _state.Direction; set => _state.Direction = value; }
    public int Zoom { get => _state.Zoom; set => _state.Zoom = value; }
    public Point Cursor { get => new Point(_state.Cursor.Item1, _state.Cursor.Item2); set => _state.Cursor = Tuple.Create(value.X, value.Y); }
    public Point View { get => new Point(_state.View.Item1, _state.View.Item2); set => _state.View = Tuple.Create(value.X, value.Y); }
    public bool MenuVisible { get => _state.MenuVisible; set => _state.MenuVisible = value; }

    public StateService(DimGame game)
    {
        _game = game;
        _state = LoadGame() ?? NewGame();

        GameMenu = new Menu();
    }

    

    public void ZoomIn()
    {
        if (Zoom > 1) Zoom--;
    }
    public void ZoomOut()
    {
        if (Zoom < Constants.MAX_ZOOM) Zoom++;
    }

    public void RotateLeft()
    {
        switch (Direction)
        {
            case Orientation.NORTH:
                Direction = Orientation.WEST;
                break;
            case Orientation.EAST:
                Direction = Orientation.NORTH;
                break;
            case Orientation.SOUTH:
                Direction = Orientation.EAST;
                break;
            case Orientation.WEST:
                Direction = Orientation.SOUTH;
                break;
        }
    }
    public void RotateRight()
    {
        switch (Direction)
        {
            case Orientation.NORTH:
                Direction = Orientation.EAST;
                break;
            case Orientation.EAST:
                Direction = Orientation.SOUTH;
                break;
            case Orientation.SOUTH:
                Direction = Orientation.WEST;
                break;
            case Orientation.WEST:
                Direction = Orientation.NORTH;
                break;
        }
    }

    public void MoveCursor(int byX, int byY)
    {
        int xx = 0, yy = 0;

        switch (Direction)
        {
            case Orientation.NORTH:
                xx = byX;
                yy = byY;
                break;
            case Orientation.EAST:
                xx = byY;
                yy = 0-byX;
                break;
            case Orientation.SOUTH:
                xx = 0-byX;
                yy = 0-byY;
                break;
            case Orientation.WEST:
                xx = 0-byY;
                yy = byX;
                break;
        }

        var dest = Cursor;
        dest.X += xx;
        dest.Y += yy;

        if (new Rectangle(new Point(0,0), new Point(Size, Size)).Contains(dest)) Cursor = dest;
    }

    public void MoveView(int byX, int byY)
    {
        var dest = View;
        dest.X += byX;
        dest.Y += byY;

        var x = _game.Resolution.X;
        var y = _game.Resolution.Y;
        var bounds = new Rectangle(0-x,0-(y*2), x*2, y*3);
        bounds.Inflate(-100*Zoom, -100*Zoom);
        if (bounds.Contains(dest) || !bounds.Contains(View)) View = dest;
    }

    public string GetTileTextureName(Point coords)
    {
        var index = Utils.PointToIndex(coords, Size);
        return _state.Tiles[index].TextureName;
    }

    public void Build()
    {
        var tileName = GameMenu.GetSelectedTileName();

        if (MenuVisible) return;
        if (tileName == null) return;
        
        var index = Utils.PointToIndex(Cursor, Size);
        _state.Tiles[index] = new Tile { TextureName = tileName };
    }

    public void SaveGame()
    {
        JsonSerializerOptions options = new () { WriteIndented = true };
        var json = JsonSerializer.Serialize(_state, options);
        File.WriteAllText(Constants.SAVE_PATH, json);
    }

    private State LoadGame()
    {
        try
        {
            var state = JsonSerializer.Deserialize<State>(File.ReadAllText(Constants.SAVE_PATH));
            System.Console.WriteLine($"Loaded game from: {Constants.SAVE_PATH}");
            return state;
        }
        catch
        {
            System.Console.WriteLine($"Unable to load from: {Constants.SAVE_PATH}");
            return null;
        }
    }

    private State NewGame()
    {
        var state = new State();
        state.Size = Constants.SIZE;
        state.Cursor = Tuple.Create(Constants.SIZE / 2, Constants.SIZE / 2);
        state.View = Tuple.Create(0,0);
        state.Tiles = Enumerable.Range(0, Constants.SIZE * Constants.SIZE).ToDictionary(x => x, x => new Tile());
        state.Zoom = Constants.MAX_ZOOM;

        System.Console.WriteLine($"Starting new game with size of {Constants.SIZE}");

        return state;
    }
}