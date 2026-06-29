using Raylib_cs;

internal class Transportation : DimGrid
{ 
    internal Transportation(Rect bounds, Console console, Font font) : base(bounds, console, font, [])
    {
        foreach (var name in Enum.GetNames<Enumerations.TransportationType>())
        {
            AddItem(name, () => console.Write($"Selected {name}"));    
        }

        
    }

}