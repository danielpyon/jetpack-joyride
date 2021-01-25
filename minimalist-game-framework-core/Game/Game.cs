using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = Globals.TITLE;
    public static readonly Vector2 Resolution = new Vector2(Globals.WIDTH, Globals.HEIGHT);

    Renderable character = new Character();
    Renderable background = new Background()
    Camera camera = new Camera();

    public Game()
    {
    }

    public void Update()
    {
        // Handle Input
        background.HandleInput();
        character.HandleInput();

        // Movement
        background.Move();
        character.Move();

        // Render
        background.Render(camera);
        character.Render(camera);
    }
}
