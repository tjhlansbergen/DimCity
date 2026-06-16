using System.Numerics;
using Raylib_cs;

internal class DimControls
{
    public Rect Bounds { get; private set;}

    internal DimControls(Rect bounds)
    {
        Bounds = bounds;
    }

    internal void Draw()
    {
        var controlsPanel = new Rect
        {
            position = Bounds.position,
            size = Bounds.size
        };

        DimLib.DrawRect(controlsPanel, Color.FromHSV(0, 0, 0.15f));
    }
}