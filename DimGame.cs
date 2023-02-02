﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DimCity;

public class DimGame : Game
{
    public Point Resolution { get; private set; }

    public DimGame()
    {
        var graphicsDeviceManager = new GraphicsDeviceManager(this);
        graphicsDeviceManager.IsFullScreen = true;
        //graphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        //graphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        Resolution = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);


        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Services.AddService<IStateService>(new StateService(this, new Point(100,100)));
        Services.AddService<ITextureService>(new TextureService(this));
        Services.AddService<IDrawingService>(new DrawingService(this));
        Services.AddService<IInputService>(new InputService(this));
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Services.GetService<ITextureService>().Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        Services.GetService<IInputService>().Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Services.GetService<IDrawingService>().Draw();
        base.Draw(gameTime);
    }
}
