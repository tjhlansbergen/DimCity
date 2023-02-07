using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DimCity;

public interface IDrawingService
{
    public void Draw();
}

public class DrawingService : IDrawingService
{
    private DimGame _game;
    private IStateService _state;
    private IGraphicsDeviceService _graphicsDeviceService;
    private ITextureService _textureService;

    public DrawingService(DimGame game)
    {
        _game = game;
        _state = game.Services.GetService<IStateService>();
        _graphicsDeviceService = game.Services.GetService<IGraphicsDeviceService>();
        _textureService = game.Services.GetService<ITextureService>();
    }

    public void Draw()
    {
        _graphicsDeviceService.GraphicsDevice.Clear(Color.CornflowerBlue);

        var spritebatch = new SpriteBatch(_graphicsDeviceService.GraphicsDevice);
        spritebatch.Begin();

        var width = 200 / _state.Zoom;
        var height = 130 / _state.Zoom;
        var thickness = 30 / _state.Zoom;

        var start = new Point(_game.Resolution.X / 2, 20);
        var left = start.X + _state.View.X;
        var top = start.Y + _state.View.Y - ((Constants.MAX_ZOOM - _state.Zoom) * height);

        for (int y = 0; y < _state.Size.Y; y++)
        {
            for (int x = 0; x < _state.Size.X; x++)
            {
                DrawOneTile(x, y);
            }
        }

        if (_state.MenuVisible) DrawMenu();

        void DrawOneTile(int x, int y)
        {
            var rotatedXY = ApplyOrientation(x,y);
    
            var t = _textureService.GetTile(_state.GetTile(rotatedXY));
            var r = new Rectangle(left + ((x - y) * width / 2), top + ((x + y) * ((height - thickness) / 2)), width, height);
            var c =  (rotatedXY == _state.Cursor) ? Color.White * 0.7f : (_state.MenuVisible) ? Color.White * 0.5f : Color.White;

            spritebatch.Draw(t, r, c);
        }

        void DrawMenu()
        {
            var border = 30;
            var bounds = new Rectangle(border, border, _game.Resolution.X / 5, _game.Resolution.Y - (border*2));
            spritebatch.Draw(ColoredTexture(Color.MidnightBlue), bounds, Color.White);
            bounds.Inflate(-2,-2);
            spritebatch.Draw(ColoredTexture(Color.Beige), bounds, Color.White);

            const int padding = 12;
            const int cols = 6;
            var height = bounds.Y + border;
            var size = (bounds.Width - ((cols * (padding * 2)) + border )) / cols;
            
            foreach (var section in Menu.GetSections())
            {
                // todo show section name instead
                spritebatch.Draw(ColoredTexture(Color.SlateBlue), new Rectangle(bounds.X + border, height, bounds.Width - border, 2), Color.White);
                height += 2 + border;

                if (_state.GameMenu.ActiveSection == section.Key)
                {
                    var tiles = Menu.GetTileNames(section.Key);
                    for (int i = 0; i < tiles.Count; i++)
                    {
                        var x = i - ((i / cols) * cols);
                        var xx = bounds.X + border + ((x + 1) * padding) + (x * size);
                        spritebatch.Draw(_textureService.GetTile(tiles[i]), new Rectangle(xx, height, size, size), Color.White);    
                        if (x == cols - 1) height += size + (padding*2);
                    }
                    height += border + size;
                }
            }


            


        }
        
        spritebatch.End();
    }

    private Point ApplyOrientation(int x, int y)
    {
        int xx = 0, yy = 0;

        switch (_state.Direction)
        {
            case Orientation.NORTH:
                xx = x;
                yy = y;
                break;
            case Orientation.EAST:
                xx = y;
                yy = _state.Size.Y - 1 - x;
                break;
            case Orientation.SOUTH:
                xx = _state.Size.X - 1 - x;
                yy = _state.Size.Y - 1 - y;
                break;
            case Orientation.WEST:
                xx = _state.Size.X - 1 - y;
                yy = x;
                break;
        }

        return new Point(xx, yy);
    }

    private Texture2D ColoredTexture(Color color)
    {
        var texture = new Texture2D(_graphicsDeviceService.GraphicsDevice, 1, 1);
        texture.SetData(new Color[] { color } );
        return texture;
    }
}