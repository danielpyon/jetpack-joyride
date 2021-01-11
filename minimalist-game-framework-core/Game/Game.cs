using System;
using System.Collections.Generic;

class Game
{
    public static readonly string Title = "Jetpack Joyride";
    public static readonly Vector2 Resolution = new Vector2(640, 480);

    static readonly float gravity = 300.0f;

    bool onGround = false;

    Texture background = Engine.LoadTexture("background.png");
    Texture runner = Engine.LoadTexture("runner.png");

    Vector2 runnerPosition = new Vector2(0, Resolution.Y - 70);
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

        if (Engine.GetKeyDown(Key.Space))
        {
            if (onGround)
            {
                runnerVelocity.Y = -300.0f;
                onGround = false;
            }
        }
        else
        {
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
            onGround = true;
        }

        Engine.DrawTexture(runner, runnerPosition);
    }
}
