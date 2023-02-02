using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DimCity;

public interface IDrawingService
{
    public void Draw();
}

public class DrawingService : IDrawingService
{
    private Game _game;
    private IStateService _state;
    private IGraphicsDeviceService _graphicsDeviceService;
    private ITextureService _textureService;
    private Point _start;

    public DrawingService(DimGame game)
    {
        _game = game;
        _state = game.Services.GetService<IStateService>();
        _graphicsDeviceService = game.Services.GetService<IGraphicsDeviceService>();
        _textureService = game.Services.GetService<ITextureService>();
        _start = new Point(game.Resolution.X / 2, 20);
    }

    public void Draw()
    {
        _graphicsDeviceService.GraphicsDevice.Clear(Color.CornflowerBlue);

        var spritebatch = new SpriteBatch(_graphicsDeviceService.GraphicsDevice);
        spritebatch.Begin();

        var width = 200 / _state.Zoom;
        var height = 130 / _state.Zoom;
        var thickness = 30 / _state.Zoom;

        var left = _start.X + _state.View.X;
        var top = _start.Y + _state.View.Y - ((Constants.MAX_ZOOM - _state.Zoom) * height);

        for (int y = 0; y < _state.Size.Y; y++)
        {
            for (int x = 0; x < _state.Size.X; x++)
            {
                DrawOneTile(x, y);
            }
        }

        void DrawOneTile(int x, int y)
        {
            var texture = _textureService.Get(_state.GetTile(ApplyOrientation(x, y)));

            var tileKey = ApplyOrientation(x,y);

            var t = _textureService.Get(_state.GetTile(tileKey));
            var r = new Rectangle(left + ((x - y) * width / 2), top + ((x + y) * ((height - thickness) / 2)), width, height);
            var c = (tileKey == _state.Cursor) ? Color.White * 0.7f : Color.White;

            spritebatch.Draw(t, r, c);
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
}