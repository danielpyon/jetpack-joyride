﻿using System;
using System.Collections.Generic;
using System.Text;

class Coin : Renderable
{
    private Texture texture;
    private Vector2 position;
    private Character character;
    private bool invisible = false;
    private Sound collectSound;

    public Coin(Texture texture, Vector2 position, Character character)
    {
        collectSound = Engine.LoadSound("coin.wav");
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
        float width = texture.Width;
        float height = texture.Height;

        (float low, float high) xRangeCoin = (position.X, position.X + width);
        (float low, float high) yRangeCoin = (position.Y - height, position.Y);

        (float low, float high) xRangeCharacter = (character.X, character.X + character.Width);
        (float low, float high) yRangeCharacter = (character.Y - character.Height, character.Y);

        return
            !(xRangeCoin.low > xRangeCharacter.high ||
              xRangeCoin.high < xRangeCharacter.low ||
              yRangeCoin.high < yRangeCharacter.low ||
              yRangeCoin.low > yRangeCharacter.high);
    }

    public void HandleInput()
    {
        // If colliding with character, call IncrementCoins and set invisible to true (doesn't render)
        if (!invisible && CollidingWithCharacter())
        {
            Engine.PlaySound(collectSound, false, 0.0f);
            character.IncrementCoins();
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
