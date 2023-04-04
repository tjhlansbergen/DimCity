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
    private IStateService _stateService;
    private IGraphicsDeviceService _graphicsDeviceService;
    private ITextureService _textureService;
    

    public DrawingService(DimGame game)
    {
        _game = game;
        _stateService = game.Services.GetService<IStateService>();
        _graphicsDeviceService = game.Services.GetService<IGraphicsDeviceService>();
        _textureService = game.Services.GetService<ITextureService>();
        
    }

    public void Draw()
    {
        _graphicsDeviceService.GraphicsDevice.Clear(Color.CornflowerBlue);

        var spritebatch = new SpriteBatch(_graphicsDeviceService.GraphicsDevice);
        spritebatch.Begin();

        var width = 200 / _stateService.Zoom;
        var height = 130 / _stateService.Zoom;
        var thickness = 30 / _stateService.Zoom;

        var start = new Point(_game.Resolution.X / 2, 20);
        var left = start.X + _stateService.View.X;
        var top = start.Y + _stateService.View.Y - ((Constants.MAX_ZOOM - _stateService.Zoom) * height);

        for (int y = 0; y < _stateService.Size.Y; y++)
        {
            for (int x = 0; x < _stateService.Size.X; x++)
            {
                DrawOneTile(x, y);
            }
        }

        if (_stateService.MenuVisible) DrawMenu();

        void DrawOneTile(int x, int y)
        {
            var rotatedXY = ApplyOrientation(x,y);
    
            var t = (rotatedXY == _stateService.Cursor && _stateService.GameMenu.GetSelectedTileName() != null) ? _textureService.GetTexture(_stateService.GameMenu.GetSelectedTileName()) : _textureService.GetTexture(_stateService.GetTileTextureName(rotatedXY));
            var r = new Rectangle(left + ((x - y) * width / 2), top + ((x + y) * ((height - thickness) / 2)), width, height);
            var c =  (rotatedXY == _stateService.Cursor) ? Color.White * 0.7f : (_stateService.MenuVisible) ? Color.White * 0.5f : Color.White;

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
            var size = (bounds.Width - border - (cols * padding)) / cols;
            
            foreach (var section in Menu.GetSections())
            {
                spritebatch.DrawString(_textureService.RobotoFont, section.Value, new Vector2(bounds.X + border, height + 2), Color.Black);
                spritebatch.Draw(ColoredTexture(Color.SlateBlue), new Rectangle(bounds.X + border, height, bounds.Width - border, 2), Color.White);
                height += border + padding;

                if (_stateService.GameMenu.ActiveSection == section.Key)
                {
                    var tiles = Menu.GetTileNames(section.Key);
                    for (int i = 0; i < tiles.Count; i++)
                    {
                        var x = i - ((i / cols) * cols);
                        var xx = bounds.X + border + (x * (padding + size));
                        var r = new Rectangle(xx, height, size, size);
                        
                        if (i == _stateService.GameMenu.ActiveTile) spritebatch.Draw(ColoredTexture(Color.BurlyWood), r.InflateAndReturn(6,6), Color.White);
                        spritebatch.Draw(_textureService.GetTexture(tiles[i]), r, Color.White);    
                        
                        if (x == cols - 1) height += size + (padding*2);
                    }
                    height += border + size;
                }
                else
                {
                    height += border;
                }
            }
        }
        
        spritebatch.End();
    }

    private Point ApplyOrientation(int x, int y)
    {
        int xx = 0, yy = 0;

        switch (_stateService.Direction)
        {
            case Orientation.NORTH:
                xx = x;
                yy = y;
                break;
            case Orientation.EAST:
                xx = y;
                yy = _stateService.Size.Y - 1 - x;
                break;
            case Orientation.SOUTH:
                xx = _stateService.Size.X - 1 - x;
                yy = _stateService.Size.Y - 1 - y;
                break;
            case Orientation.WEST:
                xx = _stateService.Size.X - 1 - y;
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