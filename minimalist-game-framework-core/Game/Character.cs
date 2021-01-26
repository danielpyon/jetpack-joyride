using System;
using System.Collections.Generic;
using System.Text;

class Character : Renderable
{
    // Texture for the character
    private Texture texture = Engine.LoadTexture("runner.png");
    
    // Movement constants
    private static readonly float gravity = 650.0f;
    private static readonly float horizontalSpeed = 300.0f;

    // Position, velocity, acceleration
    private Vector2 position = new Vector2(Globals.WIDTH / 6, Globals.HEIGHT - 70);
    private Vector2 velocity = new Vector2(0, 0);
    private float acceleration = 0.0f;

    public Character()
    {
    }

    public float X
    {
        get
        {
            return position.X;
        }
    }

    public float Y
    {
        get
        {
            return position.Y;
        }
    }

    public void HandleInput()
    {
        bool leftHeld = Engine.GetKeyHeld(Key.Left);
        bool rightHeld = Engine.GetKeyHeld(Key.Right);
        bool spaceHeld = Engine.GetKeyHeld(Key.Space);

        // Horizontal movement
        if (leftHeld || rightHeld)
        {
            velocity.X = leftHeld ? -horizontalSpeed : horizontalSpeed;
        }
        else
        {
            velocity.X = 0;
        }

        // Vertical movement
        if (spaceHeld)
        {
            velocity.Y = -300.0f;
            acceleration += 4.1f;
        }
        else
        {
            acceleration = 0.0f;
            if (velocity.Y < -200.0f)
            {
                velocity.Y = -200.0f;
            }
        }
    }

    public void Move(Camera camera)
    {
        // Update position
        position.X += velocity.X * Engine.TimeDelta;
        position.Y += velocity.Y * Engine.TimeDelta;
        
        // Update velocity
        velocity.Y += gravity * Engine.TimeDelta;
        velocity.Y -= acceleration * Engine.TimeDelta;

        // Handle edge cases
        if (position.Y > Globals.HEIGHT - 70)
        {
            // If the runner goes under the ground, move him to ground level
            position.Y = Globals.HEIGHT - 70;
            velocity.Y = 0;
        }

        if (position.Y < 0)
        {
            position.Y = 0;
            velocity /= 2;
            acceleration = 0;
        }

        camera.CenterOnCharacter(this);
    }

    public void Render(Camera camera)
    {
        Vector2 renderPosition = new Vector2();
        renderPosition.X = position.X - camera.X;
        renderPosition.Y = position.Y - camera.Y;
        Engine.DrawTexture(texture, renderPosition);
    }
}
