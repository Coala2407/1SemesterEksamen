//From: http://community.monogame.net/t/one-shot-key-press/11669
using Microsoft.Xna.Framework.Input;

public class Keyboard
{
    static KeyboardState currentKeyState;
    static KeyboardState previousKeyState;

    public static KeyboardState GetState()
    {
        previousKeyState = currentKeyState;
        currentKeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        return currentKeyState;
    }

    /// <summary>
    /// Check if key is held down
    /// </summary>
    /// <param name="key">Key on keyboard</param>
    /// <returns></returns>
    public static bool IsPressed(Keys key)
    {
        return currentKeyState.IsKeyDown(key);
    }

    /// <summary>
    /// Check if key is pressed. Triggers once.
    /// </summary>
    /// <param name="key">Key on keyboard</param>
    /// <returns></returns>
    public static bool HasBeenPressed(Keys key)
    {
        return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
    }
}