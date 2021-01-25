﻿using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Jetpack Joyride";
    public static readonly Vector2 Resolution = new Vector2(640, 480);

    static readonly float gravity = 650.0f;

    float jetpackAcceleration = 0.0f;

    Texture background = Engine.LoadTexture("background.png");
    Texture runner = Engine.LoadTexture("runner.png");

    Vector2 runnerPosition = new Vector2(Resolution.X / 6, Resolution.Y - 70);
    Vector2 runnerVelocity = new Vector2(0, 0);

    public Game()
    {
    }

    public void Update()
    {
        Engine.DrawTexture(background, Vector2.Zero);

        runnerPosition.X += runnerVelocity.X * Engine.TimeDelta;
        runnerPosition.Y += runnerVelocity.Y * Engine.TimeDelta;
        
        runnerVelocity.Y += gravity * Engine.TimeDelta;
        runnerVelocity.Y -= jetpackAcceleration * Engine.TimeDelta;

        bool leftHeld = Engine.GetKeyHeld(Key.Left);
        bool rightHeld = Engine.GetKeyHeld(Key.Right);
        if (leftHeld || rightHeld)
        {
            runnerVelocity.X = leftHeld ? -300.0f : 300.0f;
        }
        else
        {
            runnerVelocity.X = 0;
        }

        if (Engine.GetKeyHeld(Key.Space))
        {
            runnerVelocity.Y = -300.0f;
            jetpackAcceleration += 4.1f;
        }
        else
        {
            jetpackAcceleration = 0.0f;
            if (runnerVelocity.Y < -200.0f)
            {
                runnerVelocity.Y = -200.0f;
            }
        }

        if (runnerPosition.Y > Resolution.Y - 70)
        {
            // If the runner is on the ground
            runnerPosition.Y = Resolution.Y - 70;
            runnerVelocity.Y = 0;
        }

        if (runnerPosition.Y < 0)
        {
            runnerPosition.Y = 0;
            runnerVelocity /= 2;
            jetpackAcceleration = 0;
        }

        Engine.DrawTexture(runner, runnerPosition);
    }
}
