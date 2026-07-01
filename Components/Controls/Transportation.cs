using Raylib_cs;

internal class Transportation : DimGrid
{ 
    internal Transportation(Rect bounds, Console console, Font font) : base(bounds, console, font)
    {
        foreach (var type in Enum.GetValues<Enumerations.TransportationType>())
        {
            switch (type)
            {
                case Enumerations.TransportationType.Road:
                    AddItem(type.ToString(), () => console.Write($"Selected {type}"), Icons.Road);
                    break;
                case Enumerations.TransportationType.Rail:
                    AddItem(type.ToString(), () => console.Write($"Selected {type}"), Icons.Rail);
                    break;
            }
        }        
    }
}