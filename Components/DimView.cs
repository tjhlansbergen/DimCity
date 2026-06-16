internal abstract class DimView
{
    public Rect Bounds { get; private set;}

    internal DimView(Rect bounds)
    {
        Bounds = bounds;
    }

    internal abstract void Draw();
}