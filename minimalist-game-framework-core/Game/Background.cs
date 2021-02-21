using System;
using System.Collections.Generic;
using System.Text;

class Background : Renderable
{
    private Texture texture;
    private Segment[] segments;

    private Character character;
    
    private int rocketStart;
    private bool rocketsLaunched = false;
    private Rocket rocket;

    private bool rainLaunched = false;
    private Rain rain;

    // Where the right edge is on the map (horizontally)
    public float MaxX
    {
        get;
    }

    public float MinX
    {
        get;
    }

    public Background(String filename, float MinX, Character character, Texture texture = null)
    {
        if (texture == null)
        {
            this.texture = Engine.LoadTexture(filename);
        }
        else
        {
            this.texture = texture;
        }
        
        this.MinX = MinX;
        this.MaxX = MinX + Width;

        this.character = character;

        this.segments = Segment.GenerateSegments(MinX, character);

        Random r = new Random();
        rocketStart = r.Next(1, segments.Length - 2);
    }

    public int Height
    {
        get
        {
            return texture.Height;
        }
    }

    public int Width
    {
        get
        {
            return texture.Width;
        } 
    }

    public void HandleInput()
    {
        int currentSegment = Segment.GetCurrentSegment(character, segments);

        if (!rocketsLaunched && currentSegment == rocketStart)
        {
            // Rocket r = new Rocket(character);
            Console.WriteLine("Rockets launching!!");
            rocket = new Rocket(character);
            rocketsLaunched = true;
        }
        if (rocketsLaunched)
        {
            rocket.HandleInput();
        }

        if (!rainLaunched && currentSegment >= 1 && currentSegment <= 3)
        {
            Console.WriteLine("Acid launching!!");
            rain = new Rain(character);
            rainLaunched = true;
        }
        
        if (rainLaunched)
        {
            if (rain.Done())
                rainLaunched = false;
            rain.HandleInput();
        }
        

        foreach (Segment s in segments)
        {
            s.HandleInput();
        }
    }

    public void Move(Camera camera)
    {
        foreach (Segment s in segments)
        {
            s.Move(camera);
        }

        if (rocketsLaunched)
        {
            rocket.Move(camera);
        }

        if (rainLaunched)
        {
            rain.Move(camera);
        }
    }

    public void Render(Camera camera)
    {
        Vector2 position = new Vector2(MinX - camera.X, 0 - camera.Y);
        Engine.DrawTexture(texture, position);

        foreach (Segment s in segments)
        {
            s.Render(camera);
        }

        if (rocketsLaunched)
        {
            rocket.Render(camera);
        }

        if(rainLaunched)
        {
            rain.Render(camera);
        }
    }
}
