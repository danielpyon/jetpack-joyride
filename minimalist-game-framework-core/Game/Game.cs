using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = Globals.TITLE;
    public static readonly Vector2 Resolution = new Vector2(Globals.WIDTH, Globals.HEIGHT);

    public static Scene CurrentScene
    {
        get;
        set;
    }

    public Game()
    {
        UpdateScene();
    }

    public static void UpdateScene(int coins = -1, int distance = -1)
    {
        if (CurrentScene != null)
            CurrentScene.CleanUp();

        if (CurrentScene == null)
        {
            CurrentScene = new TitleScene();
        }
        else if (CurrentScene.GetType() == typeof(TitleScene))
        {
            CurrentScene = new GameScene();
        }
        else if (CurrentScene.GetType() == typeof(GameScene))
        {
            CurrentScene = new DeathScene(coins, distance);
        }
        else
        {
            // Death scene -> Game scene
            CurrentScene = new GameScene();
        }
    }

    public void Update()
    {
        CurrentScene.Update();
    }
}
