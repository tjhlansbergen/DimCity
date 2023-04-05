using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DimCity;

public class State
{
    public Dictionary<int, Tile> Tiles { get; set; }

    public int Size { get; set; }
    public Orientation Direction { get; set; } = Orientation.NORTH;
    public int Zoom { get; set; } = Constants.MAX_ZOOM;
    public Tuple<int, int> Cursor { get; set; }
    public Tuple<int, int> View { get; set; }
    public bool MenuVisible { get; set; }
}
