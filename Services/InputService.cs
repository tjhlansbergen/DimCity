using Microsoft.Xna.Framework;
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
        if (keyboard.GetPressedKeyCount() == 0 || keyboard.GetPressedKeyCount() == 1 && IsModifierDown(keyboard))
        {
            _lock = false;
            return;
        }

        if (_lock) return;

        if (keyboard.IsKeyDown(Keys.Escape)) _game.Exit();
        if (keyboard.IsKeyDown(Keys.Tab)) _state.MenuVisible = !_state.MenuVisible;

        if (_state.MenuVisible)
        {
            if (keyboard.IsKeyDown(Keys.Up)) _state.GameMenu.Up();
            if (keyboard.IsKeyDown(Keys.Down)) _state.GameMenu.Down();
            if (keyboard.IsKeyDown(Keys.Left)) _state.GameMenu.Left();
            if (keyboard.IsKeyDown(Keys.Right)) _state.GameMenu.Right();
            _lock = true;
            return;
        }

        if (keyboard.IsKeyDown(Keys.Space))
        {
            if (keyboard.IsKeyDown(Keys.Up)) _state.ZoomIn();
            if (keyboard.IsKeyDown(Keys.Down)) _state.ZoomOut();
            if (keyboard.IsKeyDown(Keys.Left)) _state.RotateLeft();
            if (keyboard.IsKeyDown(Keys.Right)) _state.RotateRight();
            _lock = true;
        }
        else if (keyboard.IsKeyDown(Keys.LeftControl))
        {
            if (keyboard.IsKeyDown(Keys.Up)) _state.MoveView(0, -10);
            if (keyboard.IsKeyDown(Keys.Down)) _state.MoveView(0, 10);
            if (keyboard.IsKeyDown(Keys.Left)) _state.MoveView(-10, 0);
            if (keyboard.IsKeyDown(Keys.Right)) _state.MoveView(10, 0);
        }
        else 
        {
            if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.PageUp)) _state.MoveCursor(0, -1);
            if (keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.PageDown)) _state.MoveCursor(0, 1);
            if (keyboard.IsKeyDown(Keys.Left)) _state.MoveCursor(-1, 0);
            if (keyboard.IsKeyDown(Keys.Right)) _state.MoveCursor(1, 0);
            
            if (!keyboard.IsKeyDown(Keys.LeftAlt)) _lock = true;
        }
    }

    private bool IsModifierDown(KeyboardState keyboard)
    {
        return (keyboard.IsKeyDown(Keys.Space) ||
                keyboard.IsKeyDown(Keys.LeftAlt) ||
                keyboard.IsKeyDown(Keys.LeftControl)); 
    }
}