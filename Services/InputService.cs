using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DimCity;

public interface IInputService
{
    void Update();
}

public class InputService : IInputService
{
    private Game _game;
    private IStateService _state;
    private bool _lock;

    public InputService(Game game)
    {
        _game = game;   
        _state = game.Services.GetService<IStateService>();
    }

    public void Update()
    {
        var keyboard = Keyboard.GetState();

        // no keys pressed, release lock
        if (keyboard.GetPressedKeyCount() == 0 || keyboard.GetPressedKeyCount() == 1 && keyboard.IsKeyDown(Keys.Space))
        {
            _lock = false;
            return;
        }

        if (_lock) return;

        if (keyboard.IsKeyDown(Keys.Escape)) _game.Exit();

        if (keyboard.IsKeyDown(Keys.Space))
        {
            if (keyboard.IsKeyDown(Keys.Up)) _state.ZoomIn();
            if (keyboard.IsKeyDown(Keys.Down)) _state.ZoomOut();
            if (keyboard.IsKeyDown(Keys.Left)) _state.RotateLeft();
            if (keyboard.IsKeyDown(Keys.Right)) _state.RotateRight();
        }
        else
        {
            var moveBy = 1;
            if (keyboard.IsKeyDown(Keys.LeftControl)) moveBy = 5;

            if (keyboard.IsKeyDown(Keys.Up)) _state.Move(0, -1 * moveBy);
            if (keyboard.IsKeyDown(Keys.Down)) _state.Move(0, 1 * moveBy);
            if (keyboard.IsKeyDown(Keys.Left)) _state.Move(-1 * moveBy, 0);
            if (keyboard.IsKeyDown(Keys.Right)) _state.Move(1 * moveBy, 0);
        }

        _lock = true;
    }
}