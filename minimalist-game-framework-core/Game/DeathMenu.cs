using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Linq;
using System.IO;


class DeathMenu : Renderable
{
    // Coins and distance from the previous run before death
    private int coins;
    private int distance;

    private Texture background;
    private Font font;

    private int totalCoins;
    private int highestDistance;

    public DeathMenu(int coins, int distance)
    {
        this.coins = coins;
        this.distance = distance;

        this.font = Engine.LoadFont("deathfont.ttf", pointSize: 40);
        this.background = Engine.LoadTexture("background1.png");

        LoadState();
    }

    private XElement GetFirstElementByTagName(XElement document, String tagName)
    {
        return (from el in document.Elements()
                where el.Name == tagName
                select el).ToList()[0];
    }


    private void LoadState()
    {
        String filename = "state.xml";
        String filepath = Directory.GetCurrentDirectory() + "/Assets/" + filename;

        XElement root = XElement.Load(filepath);
        totalCoins = int.Parse(GetFirstElementByTagName(root, "coins").Attribute("value").Value);
        highestDistance = int.Parse(GetFirstElementByTagName(root, "distance").Attribute("value").Value);
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
        Engine.DrawTexture(background, Vector2.Zero);
        Engine.DrawString("Coins this round: " + coins, new Vector2(45, 20), Color.White, font);
        Engine.DrawString("Distance: " + distance + " m", new Vector2(45, 80), Color.White, font);

        Engine.DrawString("Total Coins: " + totalCoins, new Vector2(45, 250), Color.White, font);
        Engine.DrawString("High Score: " + highestDistance + " m", new Vector2(45, 310), Color.White, font);

        Engine.DrawString("Press space to play again...", new Vector2(150, 400), Color.Yellow, font);
    }
}
