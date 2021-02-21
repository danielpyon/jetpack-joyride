using System;
using System.Collections.Generic;
using System.Text;

class Rocket : Renderable
{
    private Character character;
    private Vector2 position;
    private Texture texture;

    private bool moving = false;
    private float elapsed = 0.0f;
    private float velocity = 200.0f;
    private float acceleration = 500.0f;

    private Sound deathSound;

    // Rocket only needs to know the character
    public Rocket(Character character)
    {
        this.character = character;
        this.texture = Engine.LoadTexture("runner.png");
        this.deathSound = Engine.LoadSound("death.wav");
        position = new Vector2(Globals.WIDTH - texture.Width, character.Y - character.Height);
    }
    
    public void HandleInput()
    {
        elapsed += Engine.TimeDelta;

        if (elapsed >= 4.5f)
        {
            moving = true;
        }

        if (Colliding())
        {
            Console.WriteLine("Collided");
            Engine.PlaySound(deathSound);
            character.Die();
        }
    }

    private bool Colliding()
    {
        float width = texture.Width;
        float height = texture.Height;

        (float low, float high) xRangeRocket = (position.X, position.X + width);
        (float low, float high) yRangeRocket = (position.Y - height, position.Y);

        (float low, float high) xRangeCharacter = (character.X, character.X + character.Width);
        (float low, float high) yRangeCharacter = (character.Y - character.Height, character.Y);

        return
            !(xRangeRocket.low > xRangeCharacter.high ||
              xRangeRocket.high < xRangeCharacter.low ||
              yRangeRocket.high < yRangeCharacter.low ||
              yRangeRocket.low > yRangeCharacter.high);
    }

    public void Move(Camera camera)
    {
        if(!moving)
            position.Y = character.Y - character.Height;

        if (moving)
        {
            position.X -= velocity * Engine.TimeDelta;
            velocity += acceleration * Engine.TimeDelta;
        }
    }

    public void Render(Camera camera)
    {
        Engine.DrawTexture(texture, position);
    }
}
