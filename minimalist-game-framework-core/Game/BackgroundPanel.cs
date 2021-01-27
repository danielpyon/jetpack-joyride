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
    };

    private Character character;
    private int backgroundIndex = 0;
    private float backgroundX = 0;

    public BackgroundPanel(Character character)
    {
        this.character = character;

        backgrounds = new List<Background>();

        AddBackground();
        AddBackground();
        AddBackground();
        AddBackground();
    }

    private void AddBackground()
    {
        Background background = new Background(filenames[backgroundIndex++], backgroundX);
        float backgroundWidth = background.Width;

        backgrounds.Add(background);
        backgroundIndex %= filenames.Length;
        backgroundX += backgroundWidth; // Shift the background image every time you add one
    }

    private void RenderAllBackgrounds(Camera camera)
    {
        foreach (Background background in backgrounds) {
            background.Render(camera);
        }
    }

    /// <summary>
    /// Returns the background that the character is currently on
    /// </summary>
    private Background GetCurrentBackground()
    {
        foreach (Background background in backgrounds)
        {
            float minX = background.MinX;
            float maxX = background.MaxX;
            float characterX = character.X;

            if (minX <= characterX && characterX <= maxX)
            {
                return background;
            }
        }

        return null;
    }

    /// <summary>
    /// Returns whether the end of the background is currently visible from the camera
    /// </summary>
    /// <returns>True if the camera can see the edge of the current background, false otherwise</returns>
    private bool IsEndVisible()
    {
        float characterX = character.X;
        float characterWidth = character.Width;
        float screenWidth = Globals.WIDTH;
        float backgroundEdge = GetCurrentBackground().MaxX; // The max X-coordinate for the current background

        // The max X-coordinate that the camera sees
        float cameraEdge = characterX + 5 * screenWidth / 6;

        return cameraEdge > backgroundEdge;
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
        /*
         * if end is visible:
         *      addbackground()
         * remove invisible backgrounds()
         * render all backgrounds()
         */

        RenderAllBackgrounds(camera);
        Console.WriteLine("Is the end visible? " + IsEndVisible().ToString());
    }

}
