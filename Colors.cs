using Raylib_cs;

internal static class Colors
{
    // shapes
    internal static readonly Color Background = Color.FromHSV(0, 0, 0.25f);
    internal static readonly Color Panel = Color.FromHSV(0, 0, 0.15f);
    internal static readonly Color PinStripe = Color.FromHSV(0, 0, 0.65f);
    internal static readonly Color PinStripeModerate = Color.FromHSV(0, 0, 0.55f);
    internal static readonly Color MenuItemSelected = Color.FromHSV(218, 0.80f, 0.89f);
    
    internal static readonly Color GridItem = Color.FromHSV(0, 0, 0.10f);


    // text
    internal static readonly Color ConsoleText = Color.FromHSV(218, 0.80f, 0.89f);
    internal static readonly Color MenuText = Color.FromHSV(0, 0, 0.85f);
    internal static readonly Color MenuTextSelected = Color.FromHSV(0, 0, 1.0f);


    // Transportation
    internal static readonly Color Rail = Color.FromHSV(210, 0.70f, 0.75f);
    internal static readonly Color Road = Color.FromHSV(55, 0.70f, 0.80f);


    // Terraform
    internal static readonly Color Water = Color.Black;
    internal static readonly Color Mountains = Color.FromHSV(30, 0.75f, 0.18f);
    internal static readonly Color Forest = Color.FromHSV(120, 0.80f, 0.25f);
    
}