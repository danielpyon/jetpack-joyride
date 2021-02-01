using System;
using System.Collections.Generic;
using System.Text;

class Laser : Renderable
{
    private Texture texture;
    private Vector2 position;
    private LaserType type;
    private Character character;

    public Laser(Texture texture, LaserType type, Vector2 position, Character character)
    {
        this.texture = texture;
        this.position = position;
        this.type = type;
        this.character = character;
    }

    private bool CollidingWithLaser()
    {

        return false;
    }

    public void HandleInput()
    {
        if (CollidingWithLaser())
        {
            character.Die();
        }
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
