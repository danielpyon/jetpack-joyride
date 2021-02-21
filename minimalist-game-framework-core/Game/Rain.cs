using System;
using System.Collections.Generic;
using System.Text;

class Rain : Renderable
{
    private Character character;
    private Vector2 position;
    private Texture texture;

    private bool moving = false;
    private float elapsed = 0.0f;
    private float velocity = 100.0f;
    private float acceleration = 100.0f;

    public Rain(Character character)
    {
        this.character = character;
        this.texture = Engine.LoadTexture("acidrain.png");
        position = new Vector2(character.X + 700, 0);
    }

    public void HandleInput()
    {
        elapsed += Engine.TimeDelta;

        if (elapsed >= 1.0f)
        {
            moving = true;
        }

        if (Colliding())
        {
            character.Die();
        }
    }

    public bool Done()
    {
        if (position.Y > Globals.HEIGHT)
            return true;
        return false;
    }

    private bool Colliding()
    {
        float width = texture.Width;
        float height = texture.Height;

        (float low, float high) xRangeRocket = (position.X, position.X + width);
        (float low, float high) yRangeRocket = (position.Y + 30, position.Y + height - 30);


        float characterX = character.RelativePosition.X;
        float characterY = character.RelativePosition.Y;
        (float low, float high) xRangeCharacter = (characterX, characterX + character.Width);
        (float low, float high) yRangeCharacter = (characterY - character.Height, characterY);

        return
            !(xRangeRocket.low > xRangeCharacter.high ||
              xRangeRocket.high < xRangeCharacter.low ||
              yRangeRocket.high < yRangeCharacter.low ||
              yRangeRocket.low > yRangeCharacter.high);
    }

    public void Move(Camera camera)
    {
        if (!Globals.DEBUG)
        {
            if (!moving)
            {
                //position.X = character.X + character.Width / 2;
            }
            else
            {
                position.Y += velocity * Engine.TimeDelta;
                velocity += acceleration * Engine.TimeDelta;
            }
        }
    }

    public void Render(Camera camera)
    {
        Engine.DrawTexture(texture, new Vector2(position.X - camera.X, position.Y - camera.Y));
    }
}
