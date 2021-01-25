using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = Globals.TITLE;
    public static readonly Vector2 Resolution = new Vector2(Globals.WIDTH, Globals.HEIGHT);

    Character character = new Character();
    Camera camera = new Camera();

    Texture background = Engine.LoadTexture("background.png");

    public Game()
    {
    }

    public void Update()
    {
        Engine.DrawTexture(background, Vector2.Zero);
        
        character.HandleInput();
        character.Move();
        character.Render(camera);
    }
}
