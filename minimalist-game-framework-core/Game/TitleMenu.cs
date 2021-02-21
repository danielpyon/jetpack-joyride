using System;
using System.Collections.Generic;
using System.Text;

class TitleMenu : Renderable
{
    private Font font;
    private Vector2 size = new Vector2(1000, 500);
    private Vector2 posTWO = new Vector2(30, 0);
    private Texture background;
    private Texture backgroundTWO;

    public TitleMenu()
    {
        font = Engine.LoadFont("titlefont.ttf", pointSize: 40);
        background = Engine.LoadTexture("background1.png");
        backgroundTWO = Engine.LoadTexture("titlescreen4.png");
    }

    public void HandleInput()
    {
        if (Engine.GetKeyHeld(Key.Space))
        {
            Game.UpdateScene();
        }
    }

    public void Move(Camera camera)
    {
    }

    public void Render(Camera camera)
    {
        Engine.DrawTexture(background, posTWO);
        Engine.DrawTexture(backgroundTWO, Vector2.Zero, null, size);

        //Engine.DrawString("LAGS Advanced Game Studio presents", new Vector2(45, 20), Color.White, font);
        //Engine.DrawString(Globals.TITLE, new Vector2(300, 80), Color.Yellow, font);

        //Engine.DrawString("Press start to play...", new Vector2(250, 400), Color.White, font);
    }
}
