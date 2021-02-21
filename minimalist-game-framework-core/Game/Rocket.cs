using System;
using System.Collections.Generic;
using System.Text;

class Rocket : Renderable
{
    private Character character;
    private Vector2 position;
    private Texture texture;
    private Texture warning;
    private Vector2 warningPosition;

    private bool moving = false;
    private float elapsed = 0.0f;
    private float velocity = 200.0f;
    private float acceleration = 500.0f;

    // Rocket only needs to know the character
    public Rocket(Character character)
    {
        this.character = character;
        this.texture = Engine.LoadTexture("Missile.png");
        position = new Vector2(Globals.WIDTH, character.Y - character.Height);
        this.warning = Engine.LoadTexture("ExclamationBreakSmall.png");
        warningPosition = new Vector2(Globals.WIDTH - warning.Width, character.Y - character.Height);
    }

    public void HandleInput()
    {
        if (Globals.DEBUG)
        {
            if (Engine.GetKeyHeld(Key.A))
                position.X -= 10.0f;
            if (Engine.GetKeyHeld(Key.D))
                position.X += 10.0f;
            if (Engine.GetKeyHeld(Key.W))
                position.Y -= 10.0f;
            if (Engine.GetKeyHeld(Key.S))
                position.Y += 10.0f;

            if (Colliding())
            {
                Console.WriteLine("Colliding");
            }
            else
            {
                Console.WriteLine("Not Colliding");
            }

            return;
        }

        elapsed += Engine.TimeDelta;

        if (elapsed >= 4.5f)
        {
            moving = true;
        }

        if (Colliding())
        {
            character.Die();
        }
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
                position.Y = character.Y - character.Height;
                warningPosition.Y = position.Y;
            }
            else
            {
                position.X -= velocity * Engine.TimeDelta;
                velocity += acceleration * Engine.TimeDelta;
            }
        }
    }

    public void Render(Camera camera)
    {
        Engine.DrawTexture(texture, position);

        if(!moving)
            Engine.DrawTexture(warning, warningPosition);
    }
}
