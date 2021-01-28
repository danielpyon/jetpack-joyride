using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = Globals.TITLE;
    public static readonly Vector2 Resolution = new Vector2(Globals.WIDTH, Globals.HEIGHT);

    Renderable character;
    Renderable tm;
    Renderable backgroundPanel;
    Camera camera;

    Sound menuMusic;
    Sound gameMusic;

    public Game()
    {
        character = new Character();
        backgroundPanel = new BackgroundPanel((Character) character);
        camera = new Camera(0, 0);
        tm = new TopMenu((Character) character);
        
        menuMusic = Engine.LoadSound("menu.wav");
        gameMusic = Engine.LoadSound("game.wav");

        Engine.PlaySound(gameMusic, true, 4.0f);
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

        //distance
        tm.HandleInput();
        tm.Render(camera);
    }
}
