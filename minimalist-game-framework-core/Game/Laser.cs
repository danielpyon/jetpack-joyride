using System;
using System.Collections.Generic;
using System.Text;

class Laser : Renderable
{
    private Texture texture;
    private Vector2 position;
    private LaserType type;
    private Character character;
    private Polygon bounds;

    public Laser(Texture texture, LaserType type, Vector2 position, Character character)
    {
        this.texture = texture;
        this.position = position;
        this.type = type;
        this.character = character;

        float x, y, w, h;

        var points = new List<(float, float)>();
        switch (this.type)
        {
            case LaserType.HorizontalLong:
                x = position.X + 57;
                y = position.Y + 126;
                w = 160;
                h = 52;
                points.AddRange(new List<(float, float)>() { (x, y - h), (x + w, y - h), (x + w, y), (x, y) });
                break;
            case LaserType.HorizontalShort:
                x = position.X + 80;
                y = position.Y + 173;
                w = 136;
                h = 43;
                points.AddRange(new List<(float, float)>() { (x, y - h), (x + w, y - h), (x + w, y), (x, y) });
                break;
            case LaserType.VerticalLong:
                x = position.X + 122;
                y = position.Y + 253;
                w = 53;
                h = 235;
                points.AddRange(new List<(float, float)>() { (x, y - h), (x + w, y - h), (x + w, y), (x, y) });
                break;
            case LaserType.VerticalShort:
                x = position.X + 118;
                y = position.Y + 232;
                w = 44;
                h = 179;
                points.AddRange(new List<(float, float)>() { (x, y - h), (x + w, y - h), (x + w, y), (x, y) });
                break;
            case LaserType.DiagonalDownLong:
                x = position.X;
                y = position.Y;
                points.AddRange(new List<(float, float)>() { (x + 82, y + 87), (x + 118, y + 65), (x + 222, y + 178), (x + 193, y + 204) });
                break;
            case LaserType.DiagonalDownShort:
                x = position.X;
                y = position.Y;
                points.AddRange(new List<(float, float)>() { (x + 91, y + 94), (x + 125, y + 73), (x + 204, y + 161), (x + 178, y + 187) });
                break;
            case LaserType.DiagonalUpLong:
                x = position.X;
                y = position.Y;
                points.AddRange(new List<(float, float)>() { (x + 86, y + 170), (x + 184, y + 66), (x + 210, y + 103), (x + 114, y + 200) });
                break;
            case LaserType.DiagonalUpShort:
                x = position.X;
                y = position.Y;
                points.AddRange(new List<(float, float)>() { (x + 98, y + 157), (x + 180, y + 68), (x + 209, y + 100), (x + 123, y + 187) });
                break;
            default:
                x = 0;
                y = 0;
                w = 0;
                h = 0;
                points.AddRange(new List<(float, float)>() { (x, y - h), (x + w, y - h), (x + w, y), (x, y) });
                break;
        }
        
        bounds = new Polygon(points);
    }

    private bool CollidingWithLaser()
    {
        return Polygon.Intersect(bounds, character.Bounds);
    }

    public void HandleInput()
    {
        if (CollidingWithLaser())
        {
            Console.WriteLine("collide");
            character.Die();
        }
        else
        {
            Console.WriteLine("not collide");
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
