using System;
using System.Collections.Generic;
using System.Text;

class TitleMenu : Renderable
{
    private Font font;
    private Texture background;

    public TitleMenu()
    {
        font = Engine.LoadFont("titlefont.ttf", pointSize: 40);
        background = Engine.LoadTexture("background1.png");
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
        Engine.DrawTexture(background, Vector2.Zero);

        Engine.DrawString("LAGS Advanced Game Studio presents", new Vector2(45, 20), Color.White, font);
        Engine.DrawString(Globals.TITLE, new Vector2(300, 80), Color.Yellow, font);

        Engine.DrawString("Press space to play...", new Vector2(250, 400), Color.White, font);
    }
}
