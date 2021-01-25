using System;
using System.Collections.Generic;
using System.Text;

class Background : Renderable
{
    private Texture texture = Engine.LoadTexture("background.png");

    public void HandleInput()
    {
        
    }

    public void Move()
    {
        
    }

    public void Render(Camera camera)
    {
        Engine.DrawTexture(texture, Vector2.Zero);
    }
}
