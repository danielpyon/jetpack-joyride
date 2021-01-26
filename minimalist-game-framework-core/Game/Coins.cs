using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

class Coins : Renderable
{
    private Texture texture = Engine.LoadTexture("coin1.png");
    private Coin[] coins;

    public Coins(int amount)
    {
        coins = new Coin[amount];
        coins.Select(coin => new Coin(texture));
    }

    public void HandleInput()
    {
        
    }

    public void Move(Camera camera)
    {

    }

    public void Render(Camera camera)
    {

    }
}
