using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DimCity;

public class State
{
    public Dictionary<Point, Tile> Tiles;
    public Point Size { get; set; }
    public Orientation Direction { get; set; } = Orientation.NORTH;
    public int Zoom { get; set; } = Constants.MAX_ZOOM;
    public Point Cursor { get; set; }
    public Point View { get; set; }
    public bool MenuVisible { get; set; }
}
