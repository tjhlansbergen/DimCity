using Microsoft.Xna.Framework;

namespace DimCity;

public static class ExtensionMethods
{
    public static Rectangle InflateAndReturn(this Rectangle rect, int horizontalAmount, int verticalAmount)
    {
        var result = new Rectangle(rect.Location, rect.Size);
        result.Inflate(horizontalAmount, verticalAmount);
        return result;
    }

    public static Rectangle InflateAndReturn(this Rectangle rect, float horizontalAmount, float verticalAmount)
    {
        var result = new Rectangle(rect.Location, rect.Size);
        result.Inflate(horizontalAmount, verticalAmount);
        return result;
    }
}