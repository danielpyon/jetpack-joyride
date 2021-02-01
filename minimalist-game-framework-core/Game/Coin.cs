using System;
using System.Collections.Generic;
using System.Text;

class Coin : Renderable
{
    private Texture texture;
    private Vector2 position;
    private Character character;
    private bool invisible = false;

    public Coin(Texture texture, Vector2 position, Character character)
    {
        this.texture = texture;
        this.position = position;
        this.character = character;
    }

    private bool CloseEnough(float a, float b, float threshold = 0.1f)
    {
        return Math.Abs(a - b) <= threshold;
    }

    private bool CollidingWithCharacter()
    {
        int width = texture.Width + 75;
        int height = texture.Height + 75;

        (float low, float high) xRange = (position.X, position.X + width);
        (float low, float high) yRange = (position.Y, position.Y + height);

        float x = character.X + character.Width / 2;
        float y = character.Y + character.Height / 2;

        return
            (x >= xRange.low) &&
            (x <= xRange.high) &&
            (y >= yRange.low) &&
            (y <= yRange.high);
    }

    public void HandleInput()
    {
        // If colliding with character, call IncrementCoins and set invisible to true (doesn't render)
        if (!invisible && CollidingWithCharacter())
        {
            character.IncrementCoins();
            // Play coin sound?
            invisible = true;
        }
    }

    public void Move(Camera Camera)
    {

    }
    
    private static Vector2 GetCameraAdjustedPosition(Vector2 position, Camera camera)
    {
        return new Vector2(
            position.X - camera.X,
            position.Y - camera.Y
        );
    }

    public void Render(Camera camera)
    {
        if (!invisible)
        {
            Engine.DrawTexture(texture, GetCameraAdjustedPosition(position, camera));
        }
    }
}
