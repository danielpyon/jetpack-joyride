using System;
using System.Collections.Generic;
using System.Text;

class DeathMenu : Renderable
{
    public void HandleInput()
    {
        if (Engine.GetKeyHeld(Key.Space))
            Game.UpdateScene();
    }

    public void Move(Camera camera)
    {
    }

    public void Render(Camera camera)
    {
    }
}
