using System;
using System.Collections.Generic;
using System.Text;

class Background : Renderable
{
    private Texture texture;

    public Background(String filename)
    {
        texture = Engine.LoadTexture(filename);
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
        position.X = -camera.X;
        position.Y = -camera.Y;
        Engine.DrawTexture(texture, position);
    }
}
