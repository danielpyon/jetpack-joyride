using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = Globals.TITLE;
    public static readonly Vector2 Resolution = new Vector2(Globals.WIDTH, Globals.HEIGHT);

    Renderable character;
    Renderable backgroundPanel;
    Camera camera;

    public Game()
    {
        character = new Character();
        backgroundPanel = new BackgroundPanel((Character) character);
        camera = new Camera(0, 0);
    }

    public void Update()
    {
        // Handle Input
        backgroundPanel.HandleInput();
        character.HandleInput();

        // Movement
        backgroundPanel.Move(camera);
        character.Move(camera);

        // Render
        backgroundPanel.Render(camera);
        character.Render(camera);
    }
}
