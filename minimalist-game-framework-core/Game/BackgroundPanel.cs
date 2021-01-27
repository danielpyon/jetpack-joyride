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
        Background currentBackground = GetCurrentBackground();
        if (currentBackground == null)
        {
            return true;
        }

        float characterX = character.X;
        float screenWidth = Globals.WIDTH;
        float backgroundEdge = currentBackground.MaxX; // The max X-coordinate for the current background

        // The max X-coordinate that the camera sees
        float cameraEdge = characterX + 5 * screenWidth / 6;

        return cameraEdge >= backgroundEdge;
    }

    /// <summary>
    /// Returns whether the given background is visible
    /// </summary>
    /// <param name="background">The background object</param>
    /// <returns>True if the background is visible, false otherwise</returns>
    private bool IsBackgroundVisible(Background background)
    {
        if (background == null)
        {
            return false;
        }

        float characterX = character.X;
        float characterWidth = character.Width;
        float screenWidth = Globals.WIDTH;
        float backgroundEdge = background.MinX;

        float cameraEdge = characterX - screenWidth / 6 + characterWidth / 2;
        return cameraEdge >= backgroundEdge;
    }

    /// <summary>
    /// Removes all the backgrounds that are no longer visible
    /// </summary>
    private void RemoveInvisibleBackgrounds()
    {
        backgrounds.RemoveAll(background => !IsBackgroundVisible(background));
    }

    public void HandleInput()
    {
        
    }

    public void Move(Camera camera)
    {
        
    }

    public void Render(Camera camera)
    {
        if (IsEndVisible())
        {
            AddBackground();
        }

        RemoveInvisibleBackgrounds();

        RenderAllBackgrounds(camera);
    }

}
