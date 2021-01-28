using System;
using System.Collections.Generic;
using System.Text;

class TopMenu : Renderable
{
    // Textures and fonts
    private Texture texturePause = Engine.LoadTexture("PauseButton.png");
    private Texture textureScore = Engine.LoadTexture("longbuton.png");
    private Character character;
    private Font Scorefont = Engine.LoadFont("Futured.TTF", pointSize: 30);


    // Position, size and distance fields
    private Vector2 position;
    private Vector2 positionPause;
    private Vector2 positionScore;
    private Vector2 sizeP = new Vector2(100, 100);
    private Vector2 sizeS = new Vector2(150, 100);
    private int dist;

    public TopMenu(Character character)
    {
        // setup character and position variables
        this.character = character;
   
        position = new Vector2(50, 7);
        positionScore = new Vector2(00, -28);
        positionPause = new Vector2(915, -30);
    }

    public void HandleInput()
    {
        // grabbing distance from character
        dist = ((int) character.X) / 20 - 7;
    }

    public void Move(Camera camera)
    {
        //does not move
    }

    public int getDistance()
    {
        // a get method for those methods that need distance
        return dist;
    }

    public void Render(Camera camera)
    {
        //Actually displaying the top menu

        String distString = dist + "";
        //Console.WriteLine(distString);
        Engine.DrawTexture(texturePause, positionPause, null, sizeP);
        Engine.DrawTexture(textureScore, positionScore, null, sizeS);

        if (dist > 349)
        {
            Engine.DrawString(distString, position, Color.Chartreuse, Scorefont);
        }
        else if (dist > 0)
        {
            Engine.DrawString(distString, position, Color.Crimson, Scorefont);
        }
    }
}
