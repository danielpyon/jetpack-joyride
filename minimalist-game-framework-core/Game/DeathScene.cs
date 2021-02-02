using System;
using System.Collections.Generic;
using System.Text;

class DeathScene : Scene
{
    private Sound music;
    private SoundInstance musicInstance;

    public DeathScene(int coins, int distance) : base()
    {
        renderables.AddRange(new List<Renderable>() { new DeathMenu(coins, distance) });

        Camera camera = new Camera(0, 0);
        this.camera = camera;

        music = Engine.LoadSound("menu.wav");
        musicInstance = Engine.PlaySound(music, true);
    }

    public override void CleanUp()
    {
        Engine.StopSound(musicInstance);
    }
}
