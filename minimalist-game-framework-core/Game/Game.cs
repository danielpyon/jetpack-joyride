using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = Globals.TITLE;
    public static readonly Vector2 Resolution = new Vector2(Globals.WIDTH, Globals.HEIGHT);

    Sound menuMusic;

    Scene gameScene;

    public Game()
    {
        gameScene = new GameScene();

        menuMusic = Engine.LoadSound("menu.wav");
    }

    public void Update()
    {
        gameScene.Update();
    }
}
