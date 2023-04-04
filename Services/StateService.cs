using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DimCity;

public interface IStateService
{
    Point Size { get; }
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
    string SaveGame();
}

public class StateService : IStateService
{
    private DimGame _game;
    private State _state;

    public Menu GameMenu { get; set; }

    public Point Size => _state.Size;
    public Orientation Direction => _state.Direction;
    public int Zoom => _state.Zoom;
    public Point Cursor => _state.Cursor;
    public Point View => _state.View;
    public bool MenuVisible { get => _state.MenuVisible; set => _state.MenuVisible = value; }

    public StateService(DimGame game, Point size)
    {
        _game = game;
        _state = new State();
        _state.Size = size;
        _state.Cursor = new Point(size.X / 2, size.Y / 2);
        _state.Tiles = _initTiles(size);
        _state.Zoom = Constants.MAX_ZOOM;
        
        GameMenu = new Menu();
    }

    public StateService(DimGame game, State state)
    {
        _game = game;
        _state = state;
        
        GameMenu = new Menu();
    }

    private static Dictionary<Point, Tile> _initTiles(Point size)
    {
        var tiles = new Dictionary<Point, Tile>();

        for (int y = 0; y < size.Y; y++)
        {
            for (int x = 0; x < size.X; x++)
            {
                tiles.Add(new Point(x,y), new Tile());
            }
        }

        return tiles;
    }

    public void ZoomIn()
    {
        if (_state.Zoom > 1) _state.Zoom--;
    }
    public void ZoomOut()
    {
        if (_state.Zoom < Constants.MAX_ZOOM) _state.Zoom++;
    }

    public void RotateLeft()
    {
        switch (_state.Direction)
        {
            case Orientation.NORTH:
                _state.Direction = Orientation.WEST;
                break;
            case Orientation.EAST:
                _state.Direction = Orientation.NORTH;
                break;
            case Orientation.SOUTH:
                _state.Direction = Orientation.EAST;
                break;
            case Orientation.WEST:
                _state.Direction = Orientation.SOUTH;
                break;
        }
    }
    public void RotateRight()
    {
        switch (_state.Direction)
        {
            case Orientation.NORTH:
                _state.Direction = Orientation.EAST;
                break;
            case Orientation.EAST:
                _state.Direction = Orientation.SOUTH;
                break;
            case Orientation.SOUTH:
                _state.Direction = Orientation.WEST;
                break;
            case Orientation.WEST:
                _state.Direction = Orientation.NORTH;
                break;
        }
    }

    public void MoveCursor(int byX, int byY)
    {
        int xx = 0, yy = 0;

        switch (_state.Direction)
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

        var dest = _state.Cursor;
        dest.X += xx;
        dest.Y += yy;

        if (new Rectangle(new Point(0,0), _state.Size).Contains(dest)) _state.Cursor = dest;
    }

    public void MoveView(int byX, int byY)
    {
        var dest = _state.View;
        dest.X += byX;
        dest.Y += byY;

        var x = _game.Resolution.X;
        var y = _game.Resolution.Y;
        var bounds = new Rectangle(0-x,0-(y*2), x*2, y*3);
        bounds.Inflate(-100*_state.Zoom, -100*_state.Zoom);
        if (bounds.Contains(dest) || !bounds.Contains(_state.View)) _state.View = dest;
    }

    public string GetTileTextureName(Point coords)
    {
        return _state.Tiles[coords].TextureName;
    }

    public void Build()
    {
        var tileName = GameMenu.GetSelectedTileName();

        if (_state.MenuVisible) return;
        if (tileName == null) return;
        
        _state.Tiles[_state.Cursor] = new Tile { TextureName = tileName };
    }

    public string SaveGame()
    {
        var path = "path";
        var json = JsonSerializer.Serialize(_state);

        return path;
    }
}