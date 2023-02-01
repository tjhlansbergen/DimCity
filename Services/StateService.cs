using Microsoft.Xna.Framework;

namespace DimCity;

public interface IStateService
{
    Point Size { get; }
    Orientation Direction { get; }
    int Zoom { get; }

    string GetTile(Point coords);
    void Move(int byX, int by);
    void RotateLeft();
    void RotateRight();
    void ZoomIn();
    void ZoomOut();
}

public class StateService : IStateService
{
    private Game _game;
    private int _zoom = 1;

    public Point Size { get; private set; }
    public Orientation Direction { get; private set; } = Orientation.NORTH;
    public int Zoom => _zoom;

    public StateService(Game game, Point size)
    {
        _game = game;
        Size = size;
    }

    public void ZoomIn()
    {
        if (_zoom > 1) _zoom--;
    }
    public void ZoomOut()
    {
        if (_zoom < 5) _zoom++;
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

    public void Move(int byX, int by)
    {
        // not yet implemented
    }

    public string GetTile(Point coords)
    {
        int x = coords.X, y = coords.Y;
        if (x == 5 && y == 10 || x == 5 && y == 11 || x == 5 && y == 12 || x == 5 && y == 13 || x == 6 && y == 13 || x == 6 && y == 14)
        {
            return "empty";
        }

        return "grass";
    }
}