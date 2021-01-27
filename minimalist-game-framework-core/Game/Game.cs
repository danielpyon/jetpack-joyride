using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = Globals.TITLE;
    public static readonly Vector2 Resolution = new Vector2(Globals.WIDTH, Globals.HEIGHT);

    Renderable character;
    Renderable backgroundPanel;

    List<Renderable> Renderables;
    
    Camera camera;

    Sound menuMusic;
    Sound gameMusic;

    public Game()
    {
        character = new Character();
        backgroundPanel = new BackgroundPanel((Character) character);
        
        Renderables = new List<Renderable>();
        Renderables.AddRange(new List<Renderable>() { backgroundPanel, character });

        camera = new Camera(0, 0);
        
        menuMusic = Engine.LoadSound("menu.wav");
        gameMusic = Engine.LoadSound("game.wav");

        Engine.PlaySound(gameMusic, true, 4.0f);
    }

    public void Update()
    {
        // Handle Input
        Renderables.ForEach(delegate (Renderable r)
        {
            r.HandleInput();
        });

        // Movement
        Renderables.ForEach(delegate (Renderable r)
        {
            r.Move(camera);
        });

        // Render
        Renderables.ForEach(delegate (Renderable r)
        {
            r.Render(camera);
        });
    }
}
