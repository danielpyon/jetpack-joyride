﻿using System;
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

    private bool CollidingWithCharacter()
    {
        int width = texture.Width;
        int height = texture.Height;

        return false;
    }

    public void HandleInput()
    {
        // If colliding with character, call IncrementCoins and set invisible to true (doesn't render)
        if (CollidingWithCharacter())
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
