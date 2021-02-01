﻿using System;
using System.Collections.Generic;
using System.Text;

class TopMenu : Renderable
{
    private Texture pauseTexture;
    private Character character;
    private Font distanceFont;

    private Vector2 distancePosition;
    private Vector2 pausePosition;
    private Vector2 pauseSize;

    private float characterX;

    public TopMenu(Character character)
    {
        this.character = character;
        characterX = character.X;

        distancePosition = new Vector2(50, 7);
        distanceFont = Engine.LoadFont("distancefont.ttf", pointSize: 30);

        pauseTexture = Engine.LoadTexture("pausebutton.png");
        pausePosition = new Vector2(915 - 10, -30 + 5);
        pauseSize = new Vector2(100, 100);
    }

    public void HandleInput()
    {
        characterX = character.X;
    }

    public void Move(Camera camera)
    {
    }

    public int GetDistance()
    {
        int pixelsPerMeter = 147;
        return ((int) characterX) / pixelsPerMeter;
    }

    private void RenderDistance()
    {
        String distanceString = GetDistance().ToString() + " m";
        Engine.DrawString(distanceString, distancePosition, Color.LightGray, distanceFont);
    }

    private void RenderPosition()
    {
        Engine.DrawTexture(pauseTexture, pausePosition, null, pauseSize);
    }

    public void Render(Camera camera)
    {
        RenderDistance();
        RenderPosition();
    }
}
