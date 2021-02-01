using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = Globals.TITLE;
    public static readonly Vector2 Resolution = new Vector2(Globals.WIDTH, Globals.HEIGHT);

    Sound menuMusic;
    Sound gameMusic;

    Scene gameScene;

    public Game()
    {
        gameScene = new GameScene();

        menuMusic = Engine.LoadSound("menu.wav");
        gameMusic = Engine.LoadSound("game.wav");

        Engine.PlaySound(gameMusic, true, 4.0f);
    }

    public void Update()
    {
        gameScene.Update();
    }
}
