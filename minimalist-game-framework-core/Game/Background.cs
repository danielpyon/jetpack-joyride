using System;
using System.Collections.Generic;
using System.Text;

class Background : Renderable
{
    private Texture texture;

    // Where the right edge is on the map (horizontally)
    public float MaxX
    {
        get;
    }

    public float MinX
    {
        get;
    }

    public Background(String filename, float MinX, Texture texture = null)
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
        this.texture = texture;
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
    }

    public void Move(Camera camera)
    {

    }

    public void Render(Camera camera)
    {
        Vector2 position = new Vector2();
        position.X = MinX - camera.X;
        position.Y = 0 - camera.Y;
        Engine.DrawTexture(texture, position);
    }
}
