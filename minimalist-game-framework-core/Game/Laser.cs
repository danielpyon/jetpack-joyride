using System;
using System.Collections.Generic;
using System.Text;

class Laser : Renderable
{
    private Texture texture;
    private Vector2 position;

    public Laser(Texture texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
    }

    public void HandleInput()
    {

    }

    public void Move(Camera Camera)
    {

    }

    public void Render(Camera camera)
    {

    }
}
