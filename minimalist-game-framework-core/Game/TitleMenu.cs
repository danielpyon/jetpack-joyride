using System;
using System.Collections.Generic;
using System.Text;

class TitleMenu : Renderable
{
    private Font font;
    private Vector2 size = new Vector2(1000, 500);
    private Vector2 posTWO = new Vector2(0, 0);
    private Texture background;
    private Texture backgroundTWO;
    private Texture character;

    public TitleMenu()
    {
        font = Engine.LoadFont("titlefont.ttf", pointSize: 40);
        background = Engine.LoadTexture("background1.png");
        backgroundTWO = Engine.LoadTexture("titlescreen4.png");


        String[] options = { "astronautRUNNER.png", "bodybuilderRUNNER.png", "bubblegumRUNNER.png", "clownRUNNER.png", "fireboyRUNNER.png", "kingRUNNER.png", "lizardRUNNER.png", "snakeRUNNER.png", "submarinerRUNNER.png" };
        Random r = new Random();
        String name = options[r.Next(0, options.Length)];
        character = Engine.LoadTexture("clothings/" + name);
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

        Engine.DrawTexture(character, new Vector2(Globals.WIDTH / 2, Globals.HEIGHT / 2));
        //Engine.DrawString("LAGS Advanced Game Studio presents", new Vector2(45, 20), Color.White, font);
        //Engine.DrawString(Globals.TITLE, new Vector2(300, 80), Color.Yellow, font);
        
        //Engine.DrawString("Press space to play...", new Vector2(250, 400), Color.White, font);
    }
}
