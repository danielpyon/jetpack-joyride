using System;
using System.Collections.Generic;
using System.Text;

class BackgroundPanel : Renderable
{
    private List<Background> backgrounds; // Background objects to be rendered (List<> retains insertion order)

    // Backgrounds will render in this order (this is constant)
    private String[] filenames =
    {
        "background1.png",
        "background2.png",
        "background3.png"
    };

    private Character character;
    private int backgroundIndex = 0;
    private float backgroundX = 0;
    private int backgroundWidth; // Background image width (pixels)

    public BackgroundPanel(Character character)
    {
        this.character = character;
        backgroundWidth = new Background(filenames[0], 0).Width;

        backgrounds = new List<Background>();

        AddBackground();
        AddBackground();
        AddBackground();
        AddBackground();
    }

    private void AddBackground()
    {
        backgrounds.Add(new Background(filenames[backgroundIndex++], backgroundX));
        backgroundIndex %= filenames.Length;
        backgroundX += backgroundWidth; // Shift the background image every time you add one
    }

    private void RenderAllBackgrounds(Camera camera)
    {
        foreach (Background background in backgrounds) {
            background.Render(camera);
        }
    }

    private bool IsEndVisible()
    {
        return true;
    }

    private void RemoveInvisibleBackgrounds()
    {

    }

    public void HandleInput()
    {
        
    }

    public void Move(Camera camera)
    {

    }

    public void Render(Camera camera)
    {
        RenderAllBackgrounds(camera);
    }

}
