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
    private IGraphicsDeviceManager _graphicsDeviceManager;
    private ITextureService _textureService;

    public DrawingService(Game game)
    {
        _game = game;
        _state = game.Services.GetService<IStateService>();
        _graphicsDeviceService = game.Services.GetService<IGraphicsDeviceService>();
        _graphicsDeviceManager = game.Services.GetService<IGraphicsDeviceManager>();
        _textureService = game.Services.GetService<ITextureService>();
    }

    public void Draw()
    {
        _graphicsDeviceService.GraphicsDevice.Clear(Color.CornflowerBlue);

        var spritebatch = new SpriteBatch(_graphicsDeviceService.GraphicsDevice);
        spritebatch.Begin();

        var width = 100 / _state.Zoom;
        var height = 65 / _state.Zoom;
        var thickness = 15 / _state.Zoom;

        var xStart = (_graphicsDeviceManager as GraphicsDeviceManager).PreferredBackBufferWidth / 2;
        var yStart = 10;

        for (int y = 0; y < _state.Size.Y; y++)
        {
            for (int x = 0; x < _state.Size.X; x++)
            {
                DrawOneTile(x, y);
            }
        }

        void DrawOneTile(int x, int y)
        {
            var texture = _textureService.Get(_state.GetTile(x,y));
            spritebatch.Draw(texture, new Rectangle(xStart + ((x - y) * width / 2), yStart + ((x + y) * ((height - thickness) / 2)), width, height), Color.White);
        }
        
        spritebatch.End();
    }
}