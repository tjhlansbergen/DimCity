﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DimCity;

public class DimGame : Game
{
    public Point Resolution { get; private set; }

    public DimGame(bool windowed)
    {
        var graphicsDeviceManager = new GraphicsDeviceManager(this);
        if (windowed) 
        {
            graphicsDeviceManager.PreferredBackBufferWidth = 1280;
            graphicsDeviceManager.PreferredBackBufferHeight = 720;
            graphicsDeviceManager.ApplyChanges();
            
            Resolution = new Point(graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight);            
            this.Window.AllowUserResizing = true;

        }
        else
        {
            graphicsDeviceManager.IsFullScreen = true;
            Resolution = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        }
        

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Services.AddService<IStateService>(new StateService(this));
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

    protected override void OnExiting(Object sender, EventArgs args)
    {
        System.Console.WriteLine("Exiting");
        Services.GetService<IStateService>().SaveGame();
        System.Console.WriteLine($"Game saved to: {Constants.SAVE_PATH}");

        base.OnExiting(sender, args);
    }
}
