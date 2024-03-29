﻿using System;
using System.Collections.Generic;
using System.Text;

class Camera
{
    public Camera(float X, float Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public float X
    {
        get;
        set;
    }

    public float Y
    {
        get;
        set;
    }

    public void CenterOnCharacter(Character c)
    {
        X = c.X - Globals.WIDTH / 6 + c.Width / 2;
        Y = 0;
    }

    public void Move(float XOffset, float YOffset)
    {
        X += XOffset;
        Y += YOffset;
    }

}