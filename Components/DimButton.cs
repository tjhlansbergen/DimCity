using System.Numerics;
using Raylib_cs;

internal class DimButton
{
    private const int padding = 8;

    private readonly Font _font;
    private readonly Vector2 _textSize;

    private string Label { get; }

    internal Rect Bounds { get; }

    internal DimButton(string label, Font font, float right, float bottom)
    {
        _font = font;
        _textSize = Raylib.MeasureTextEx(font, label, DimWindow.fontSize, 1);
        var size = new Vector2(_textSize.X + 2 * padding, font.BaseSize + 2 * padding);
        Bounds = new Rect
        {
            position = new Vector2(right - size.X, bottom - size.Y),
            size = size
        };
        Label = label;
    }

    internal void Draw()
    {
        DimLib.DrawRect(Bounds, Colors.Panel, outline: true);
        Raylib.DrawTextEx(
            _font,
            Label,
            new Vector2(Bounds.position.X + padding, Bounds.position.Y + (Bounds.size.Y - _textSize.Y) / 2),
            DimWindow.fontSize,
            1,
            Colors.MenuText);
    }
}
