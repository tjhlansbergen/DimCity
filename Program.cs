
internal static class Program
{
    public static void Main(string[] args)
    {
        bool fullscreen = false;
        if (args.Length > 0 && args[0].Trim().ToLowerInvariant() == "-fs")
        {
            fullscreen = true;
        }

        var window = new DimWindow(fullscreen);
        window.Draw();
    }
}