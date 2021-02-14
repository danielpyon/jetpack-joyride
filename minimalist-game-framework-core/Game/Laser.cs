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
                x = position.X + 78;
                y = position.Y + 158;
                w = 130;
                h = 30;
                points.AddRange(new List<(float, float)>() { (x, y - h), (x + w, y - h), (x + w, y), (x, y) });
                break;
            case LaserType.HorizontalShort:
                x = position.X + 97;
                y = position.Y + 157;
                w = 103;
                h = 24;
                points.AddRange(new List<(float, float)>() { (x, y - h), (x + w, y - h), (x + w, y), (x, y) });
                break;
            case LaserType.VerticalLong:
                x = position.X + 132;
                y = position.Y + 220;
                w = 20;
                h = 160;
                points.AddRange(new List<(float, float)>() { (x, y - h), (x + w, y - h), (x + w, y), (x, y) });
                break;
            case LaserType.VerticalShort:
                x = position.X + 132;
                y = position.Y + 212;
                w = 23;
                h = 105;
                points.AddRange(new List<(float, float)>() { (x, y - h), (x + w, y - h), (x + w, y), (x, y) });
                break;
            case LaserType.DiagonalDownLong:
                x = position.X;
                y = position.Y;
                points.AddRange(new List<(float, float)>() { (x + 105, y + 97), (x + 121, y + 88), (x + 201, y + 173), (x + 185, y + 189) });
                break;
            case LaserType.DiagonalDownShort:
                x = position.X;
                y = position.Y;
                points.AddRange(new List<(float, float)>() { (x + 110, y + 104), (x + 126, y + 99), (x + 188, y + 159), (x + 172, y + 170) });
                break;
            case LaserType.DiagonalUpLong:
                x = position.X;
                y = position.Y;
                points.AddRange(new List<(float, float)>() { (x + 93, y + 175), (x + 183, y + 88), (x + 198, y + 98), (x + 107, y + 192) });
                break;
            case LaserType.DiagonalUpShort:
                x = position.X;
                y = position.Y;
                points.AddRange(new List<(float, float)>() { (x + 112, y + 159), (x + 178, y + 95), (x + 191, y + 105), (x + 123, y + 173) });
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
