using Microsoft.Xna.Framework;

namespace DimCity;

public static class Utils
{
    public static int PointToIndex(Point point, int size) => (point.Y * size) + point.X; 
    public static Point IndexToPoint(int index, int size) => new Point(index % size , index / size);    
}
