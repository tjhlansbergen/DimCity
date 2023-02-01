using Microsoft.Xna.Framework;

namespace DimCity;

public interface IStateService
{
    Point Size { get; }
    Orientation Direction { get; }
    int Zoom { get; }

    string GetTile(int x, int y);
    void Move(int byX, int byY);
    void RotateLeft();
    void RotateRight();
    void ZoomIn();
    void ZoomOut();
}

public class StateService : IStateService
{
    private Microsoft.Xna.Framework.Game _game;
    private int _zoom = 1;

    public Point Size { get; private set; }
    public Orientation Direction { get; private set; } = Orientation.NORTH;
    public int Zoom => _zoom;

    public StateService(Microsoft.Xna.Framework.Game game, Point size)
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

    public void Move(int byX, int byY)
    {
        // not yet implemented
    }

    public string GetTile(int x, int y)
    {
        int xx = 0, yy = 0;

        switch (Direction)
        {
            case Orientation.NORTH:
                xx = x;
                yy = y;
                break;
            case Orientation.EAST:
                xx = y;
                yy = Size.Y - 1 - x;
                break;
            case Orientation.SOUTH:
                xx = Size.X - 1 - x;
                yy = Size.Y - 1 - y;
                break;
            case Orientation.WEST:
                xx = Size.X - 1 - y;
                yy = x;
                break;
        }

        if (xx == 5 && yy == 10 || xx == 5 && yy == 11 || xx == 5 && yy == 12 || xx == 5 && yy == 13 || xx == 6 && yy == 13 || xx == 6 && yy == 14)
        {
            return "empty";
        }

        return "grass";
    }
}