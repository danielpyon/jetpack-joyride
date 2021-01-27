using System;
using System.Collections.Generic;
using System.Text;

class Background : Renderable
{
    private Texture texture;
    private float x; // where on the map it is (horizontally)

    public Background(String filename, float x)
    {
        texture = Engine.LoadTexture(filename);
        this.x = x;
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
        position.X = x - camera.X;
        position.Y = 0 - camera.Y;
        Engine.DrawTexture(texture, position);
    }
}
