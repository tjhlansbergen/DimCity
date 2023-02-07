using System;
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

    string GetTile(Point coords);
    void MoveCursor(int byX, int by);
    void MoveView(int byX, int by);
    void RotateLeft();
    void RotateRight();
    void ZoomIn();
    void ZoomOut();
}

public class StateService : IStateService
{
    private DimGame _game;
    private int _zoom = Constants.MAX_ZOOM;
    

    public Point Size { get; private set; }
    public Orientation Direction { get; private set; } = Orientation.NORTH;
    public int Zoom => _zoom;
    public Point Cursor { get; private set; }
    public Point View { get; private set; }
    public bool MenuVisible { get; set; }
    public Menu GameMenu { get; set; }

    public StateService(DimGame game, Point size)
    {
        _game = game;
        Size = size;
        Cursor = new Point(Size.X / 2, Size.Y / 2);
        GameMenu = new Menu();
    }

    public void ZoomIn()
    {
        if (_zoom > 1) _zoom--;
    }
    public void ZoomOut()
    {
        if (_zoom < Constants.MAX_ZOOM) _zoom++;
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

        if (new Rectangle(new Point(0,0), Size).Contains(dest)) Cursor = dest;
    }

    public void MoveView(int byX, int byY)
    {
        var dest = View;
        dest.X += byX;
        dest.Y += byY;

        var x = _game.Resolution.X;
        var y = _game.Resolution.Y;
        var bounds = new Rectangle(0-x,0-(y*2), x*2, y*3);
        bounds.Inflate(-100*_zoom, -100*_zoom);
        if (bounds.Contains(dest) || !bounds.Contains(View)) View = dest;
    }

    public string GetTile(Point coords)
    {
        int x = coords.X, y = coords.Y;
        if (x == 5 && y == 10 || x == 5 && y == 11 || x == 5 && y == 12 || x == 5 && y == 13 || x == 6 && y == 13 || x == 6 && y == 14)
        {
            return "grass";
        }

        return "road";
    }
}