using System;
using System.Collections.Generic;
using System.Text;

class GameScene : Scene
{
    private Sound music;
    private SoundInstance musicInstance;

    public GameScene() : base()
    {
        Renderable character = new Character();
        Renderable backgroundPanel = new BackgroundPanel((Character)character);
        Renderable topMenu = new TopMenu((Character)character);

        renderables.AddRange(new List<Renderable>() { backgroundPanel, character, topMenu });

        Camera camera = new Camera(0, 0);
        this.camera = camera;

        music = Engine.LoadSound("game.wav");
        musicInstance = Engine.PlaySound(music, true, 4.0f);
    }

    public override void Update()
    {
        base.Update();
    }

    ~GameScene()
    {
        Engine.StopSound(musicInstance);
    }
}
