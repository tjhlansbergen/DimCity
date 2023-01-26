using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DimCity;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _emptyTexture;
    private Texture2D _roadTexture;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

         _emptyTexture = Content.Load<Texture2D>("Tiles/empty");
         _roadTexture = Content.Load<Texture2D>("Tiles/road");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        var width = 100;
        var height = 65;
        var xStart = (_graphics.PreferredBackBufferWidth / 2);
        var yStart = 10;
        var tileHeight = 15;

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                _spriteBatch.Draw(_emptyTexture, new Vector2(x*width,y*height), Color.White);        
            }
        }

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                _spriteBatch.Draw(_roadTexture, new Rectangle(xStart + ((x-y) * width / 2), yStart + ((x+y) * ((height - tileHeight) / 2)), width, height), Color.White);        
                _spriteBatch.Draw(_emptyTexture, new Rectangle(xStart + ((x-y) * width / 2), yStart + ((x+y) * ((height - tileHeight) / 2)), width, height), Color.White);        
            }
        }
        
        

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
