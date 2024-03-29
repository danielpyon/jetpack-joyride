﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Linq;
using System.IO;

class Character : Renderable
{
    // Texture for the character
    private Texture texture = Engine.LoadTexture("runner.gif");
    
    // Movement constants
    private static readonly float gravity = 650.0f;
    private static readonly float horizontalSpeed = 1000.0f;

    // Position, velocity, acceleration
    private Vector2 position;
    private Vector2 velocity = new Vector2(300.0f, 0);
    private float acceleration = 0.0f;

    // Other state
    private int coins = 0;
    private bool dying = false;
    private bool deathSoundPlayed = false;
    private Sound laserSound;
    private Sound deathSound;
    private bool isBoosting = false;
    private float elapsed = 0.0f;
    private float oldSpeed = 300.0f;
    private float framerate = 10.0f;
    private float frameIndexGround = 0;
    private float frameIndexAir = 0;

    private Texture animated;

    //boost visual
    private Vector2 boostCoord;
    private Texture boostTexture = Engine.LoadTexture("boostshield.png");
    private Texture texture2 = Engine.LoadTexture("runnerboost2.png");

    public Polygon Bounds
    {
        get;
        set;
    }

    public int Coins
    {
        get
        {
            return coins;
        }
    }

    public Vector2 RelativePosition
    {
        get
        {
            return new Vector2(Globals.WIDTH / 6 - Width / 2, Y);
        }
    }

    public Character()
    {
        laserSound = Engine.LoadSound("laser.wav");
        deathSound = Engine.LoadSound("death.wav");
        position = new Vector2(Globals.WIDTH / 6 - Width / 2, Globals.HEIGHT);

        animated = Engine.LoadTexture("charactersprites.png");

        UpdateBounds();
    }

    private void UpdateBounds()
    {
        float X = position.X;
        float Y = position.Y;
        var bounds = new List < (float, float) >();
        bounds.Add((X, Y));
        bounds.Add((X, Y - Height));
        bounds.Add((X + Width, Y - Height));
        bounds.Add((X + Width, Y));
        if (Bounds == null)
            Bounds = new Polygon(bounds);
        else
            Bounds.UpdateVertices(bounds);
    }

    public float X
    {
        get
        {
            return position.X;
        }
    }

    public float Y
    {
        get
        {
            return position.Y;
        }
    }

    public int Width
    {
        get
        {
            return texture.Width;
        }
    }

    public int Height
    {
        get
        {
            return texture.Height;
        }
    }

    private XElement GetFirstElementByTagName(XElement document, String tagName)
    {
        return (from el in document.Elements()
                where el.Name == tagName
                select el).ToList()[0];
    }

    private int GetDistance()
    {
        int pixelsPerMeter = 147;
        return ((int)X) / pixelsPerMeter;
    }

    public Boolean Dying()
    {
        return dying;
    }

    private void SaveState()
    {
        String filename = "state.xml";
        String filepath = Directory.GetCurrentDirectory() + "/Assets/" + filename;

        XElement root = XElement.Load(filepath);
        int currentCoins = int.Parse(GetFirstElementByTagName(root, "coins").Attribute("value").Value);
        int currentDistance = int.Parse(GetFirstElementByTagName(root, "distance").Attribute("value").Value);

        XmlTextWriter writer = new XmlTextWriter(filepath, null);
        writer.WriteStartElement("state");

        writer.WriteStartElement("coins");
        writer.WriteAttributeString("value", (currentCoins + coins).ToString());
        writer.WriteFullEndElement();

        int distance = GetDistance();
        writer.WriteStartElement("distance");
        writer.WriteAttributeString("value", distance > currentDistance? distance.ToString() : currentDistance.ToString());
        writer.WriteFullEndElement();
        
        writer.WriteEndElement();
        writer.Close();
    }

    public void HandleInput()
    {
        if (dying)
        {
            if (!deathSoundPlayed)
            {
                Engine.PlaySound(laserSound, false, 1.0f);
                Engine.PlaySound(deathSound, false, 1.0f);
                deathSoundPlayed = true;
            }

            if (velocity.X > 0)
                velocity.X -= 1.0f;
            else
            {
                SaveState();
                Game.UpdateScene(Coins, GetDistance());
            }
            return;
        }

        bool leftHeld = Engine.GetKeyHeld(Key.Left);
        bool rightHeld = Engine.GetKeyHeld(Key.Right);
        bool spaceHeld = Engine.GetKeyHeld(Key.Space);

        // Horizontal movement
        if (Globals.DEBUG)
        {
            if (leftHeld || rightHeld)
            {
                velocity.X = leftHeld ? -horizontalSpeed : horizontalSpeed;
            }
            else
            {
                velocity.X = 0;
            }
        }

        // Vertical movement
        if (spaceHeld)
        {
            acceleration = gravity * 3.0f; //this felt the best for the thrust/weight ratio
        }

        bool boostHeld = Engine.GetKeyDown(Key.B);
        if (boostHeld)
        {
            if (coins - 10 >= 0)
            {
                coins -= 10;
                oldSpeed = velocity.X;
                velocity.X = oldSpeed * 1.5f;
                isBoosting = true;
            }
        }

        if (isBoosting)
        {
            if (elapsed >= 3.0f)
            {
                isBoosting = false;
                velocity.X = oldSpeed;
            }

            elapsed += Engine.TimeDelta;
        }
        else
        {
            elapsed = 0.0f;
        }
    }

    public void SpeedUp()
    {
        if (velocity.X < 1000.0f)
            velocity.X += 80.0f;
    }

    public void Move(Camera camera)
    {
        // Update position
        position.X += velocity.X * Engine.TimeDelta;
        position.Y += velocity.Y * Engine.TimeDelta;
        
        // Update velocity
        velocity.Y += gravity * Engine.TimeDelta;
        velocity.Y -= acceleration * Engine.TimeDelta;

        // Reset acceleration
        acceleration = 0;
        
        // Handle edge cases
        if (position.Y > Globals.HEIGHT - 30)
        {
            // If the runner goes under the ground, move him to ground level
            position.Y = Globals.HEIGHT - 30;
            velocity.Y = 0;
        }

        if (position.Y < Height)
        {
            position.Y = Height;
            velocity.Y /= 2;
            acceleration = 0;
        }

        UpdateBounds();

        camera.CenterOnCharacter(this);
    }

    private Vector2 AdjustedCoordinates()
    {
        // Must subtract height because otherwise, the "zero" y-level is the top of the sprite
        return new Vector2(position.X, position.Y - Height);
    }

    public void Render(Camera camera)
    {
        Vector2 adjustedCoordinates = AdjustedCoordinates();
        Vector2 renderPosition = new Vector2(
            adjustedCoordinates.X - camera.X,
            adjustedCoordinates.Y - camera.Y);

        if (dying)
        {
            Engine.DrawTexture(texture2, renderPosition);
        }
        else if (isBoosting)
        {
            boostCoord.X = renderPosition.X + 10;
            boostCoord.Y = renderPosition.Y - 10;
            Engine.DrawTexture(texture2, renderPosition);
            Engine.DrawTexture(boostTexture, boostCoord);
        }
        else if (Engine.GetKeyHeld(Key.Space))
        {
            frameIndexAir = (frameIndexAir + Engine.TimeDelta * framerate) % 2.0f;
            Bounds2 frameBounds = new Bounds2(40 * 3 + (int)frameIndexAir * 40, 0, 40, 70);
            Engine.DrawTexture(animated, renderPosition, source: frameBounds);
        }
        else if (position.Y <= Globals.HEIGHT - 40)
        {
            Engine.DrawTexture(texture, renderPosition);
        }
        else
        {
            frameIndexGround = (frameIndexGround + Engine.TimeDelta * framerate) % 3.0f;
            Bounds2 frameBounds = new Bounds2((int)frameIndexGround * 40, 0, 40, 70);
            Engine.DrawTexture(animated, renderPosition, source: frameBounds);
        }
    }

    public void IncrementCoins()
    {
        coins++;
    }

    public void Die()
    {
        if(!isBoosting)
            dying = true;
    }
}
