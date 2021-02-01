using System;
using System.Collections.Generic;
using System.Text;

class BackgroundPanel : Renderable
{
    // All the background objects that should be rendered currently
    private List<Background> backgrounds;

    // Backgrounds will render in this order (this is constant)
    private String[] filenames =
    {
        "background1.png",
        "background2.png",
    };

    // Cached textures
    private Texture[] textures;

    // The offset for how early or late the new backgrounds get generated
    // The bigger the offset, the earlier the backgrounds get rendered
    // Generally, the bigger the offset, the smoother the gameplay
    private const float OFFSET = 500.0f;

    // Variables for infinite scrolling 
    private Character character;
    private int backgroundIndex = 0;
    private float backgroundX = 0;
    private bool isEndCurrentlyVisible = false;

    public BackgroundPanel(Character character)
    {
        this.character = character;
        textures = new Texture[filenames.Length];
        for (int i = 0; i < filenames.Length; i++)
        {
            textures[i] = Engine.LoadTexture(filenames[i]);
        }
        
        backgrounds = new List<Background>();
        
        // The initial background
        AddBackground();
    }

    private void AddBackground()
    {
        Background background = new Background(filenames[backgroundIndex], backgroundX, textures[backgroundIndex]);
        backgroundIndex++;

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

        return cameraEdge + OFFSET >= backgroundEdge;
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
        int numberOfBackgrounds = backgrounds.Count;

        // If there are at least 4 backgrounds, we want to remove everything but the last two
        if (numberOfBackgrounds >= 4)
        {
            backgrounds.RemoveRange(0, 2);
        }

        // Below is a buggy weird way (don't delete it, just don't use it)
        // backgrounds.RemoveAll(background => !IsBackgroundVisible(background));
    }

    public void HandleInput()
    {
        
    }

    public void Move(Camera camera)
    {
        
    }

    public void Render(Camera camera)
    {
        bool endVisible =  IsEndVisible();

        if (endVisible && !isEndCurrentlyVisible)
        {
            isEndCurrentlyVisible = true;
            AddBackground();
        }

        if (!endVisible && isEndCurrentlyVisible)
        {
            isEndCurrentlyVisible = false;
        }

        RemoveInvisibleBackgrounds();

        RenderAllBackgrounds(camera);
    }

}
