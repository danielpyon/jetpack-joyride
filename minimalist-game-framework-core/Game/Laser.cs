using System;
using System.Collections.Generic;
using System.Text;

class Laser : Renderable
{
    private Texture texture;
    private Vector2 position;
    private LaserType type;

    public Laser(Texture texture, LaserType type, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        this.type = type;
    }

    public void HandleInput()
    {

    }

    public void Move(Camera Camera)
    {

    }

    private static Vector2 GetCameraAdjustedPosition(Vector2 position, Camera camera)
    {
        return new Vector2(
            position.X - camera.X,
            position.Y - camera.Y
        );
    }

    public void Render(Camera camera)
    {
        Engine.DrawTexture(texture, GetCameraAdjustedPosition(position, camera));
    }
}
