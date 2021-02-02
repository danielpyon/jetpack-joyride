using System;
using System.Collections.Generic;
using System.Text;

class DeathMenu : Renderable
{
    // Coins and distance from the previous run before death
    private int coins;
    private int distance;

    private Texture background;
    private Font font;

    public DeathMenu(int coins, int distance)
    {
        this.coins = coins;
        this.distance = distance;

        this.font = Engine.LoadFont("deathfont.ttf", pointSize: 40);
        this.background = Engine.LoadTexture("background1.png");

        LoadState();
    }

    private void LoadState()
    {

    }

    public void HandleInput()
    {
        if (Engine.GetKeyHeld(Key.Space))
            Game.UpdateScene();
    }

    public void Move(Camera camera)
    {
    }

    public void Render(Camera camera)
    {
    }
}
