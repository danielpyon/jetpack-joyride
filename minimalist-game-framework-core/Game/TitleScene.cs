using System;
using System.Collections.Generic;
using System.Text;

class TitleScene : Scene
{
    private Sound music;
    private SoundInstance musicInstance;

    public TitleScene() : base()
    {
        renderables.AddRange(new List<Renderable>() { new TitleMenu() });

        Camera camera = new Camera(0, 0);
        this.camera = camera;

        music = Engine.LoadSound("menu.wav");
        musicInstance = Engine.PlaySound(music, true);
    }

    ~TitleScene()
    {
        Engine.StopSound(musicInstance);
    }
}
