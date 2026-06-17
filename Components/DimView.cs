internal abstract class DimView
{
    public Rect Bounds { get; private set;}
    public Console Console { get; private set; }

    internal DimView(Rect bounds, Console console)
    {
        Bounds = bounds;
        Console = console;
    }

    internal abstract void Draw();
}