using System;
using System.Collections.Generic;
using System.Text;

class GameScene : Scene
{
    public GameScene() : base()
    {
        Renderable character = new Character();
        Renderable backgroundPanel = new BackgroundPanel((Character)character);
        Renderable topMenu = new TopMenu((Character)character);

        renderables.AddRange(new List<Renderable>() { backgroundPanel, character, topMenu });

        Camera camera = new Camera(0, 0);
        this.camera = camera;
    }

}
