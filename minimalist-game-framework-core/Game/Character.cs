﻿using System;
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
    private Vector2 position;
    private Vector2 velocity = new Vector2(0, 0);
    private float acceleration = 0.0f;

    public Character()
    {
        position = new Vector2(Globals.WIDTH / 6 - Width / 2, Globals.HEIGHT);
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

    public int Width
    {
        get
        {
            return texture.Width;
        }
    }

    public int Height
    {
        get
        {
            return texture.Height;
        }
    }

    public void HandleInput()
    {
        bool leftHeld = Engine.GetKeyHeld(Key.Left);
        bool rightHeld = Engine.GetKeyHeld(Key.Right);
        bool spaceHeld = Engine.GetKeyHeld(Key.Space);
        bool spacePressed = Engine.GetKeyDown(Key.Space);

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
        if (spacePressed)
        {
            velocity.Y = -600.0f;
        }
        else if (spaceHeld)
        {
            acceleration = 700.0f;
        }
        else
        {
            acceleration = -400.0f;
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
        if (position.Y > Globals.HEIGHT)
        {
            // If the runner goes under the ground, move him to ground level
            position.Y = Globals.HEIGHT;
            velocity.Y = 0;
        }

        if (position.Y < Height)
        {
            position.Y = Height;
            velocity /= 2;
            acceleration = 0;
        }

        camera.CenterOnCharacter(this);
    }

    private Vector2 AdjustedCoordinates()
    {
        // Must subtract height because otherwise, the "zero" y-level is the top of the sprite
        return new Vector2(position.X, position.Y - Height);
    }

    public void Render(Camera camera)
    {
        Vector2 adjustedCoordinates = AdjustedCoordinates();
        Vector2 renderPosition = new Vector2(
            adjustedCoordinates.X - camera.X,
            adjustedCoordinates.Y - camera.Y); 
        
        Engine.DrawTexture(texture, renderPosition);
        Console.WriteLine(position);
    }
}