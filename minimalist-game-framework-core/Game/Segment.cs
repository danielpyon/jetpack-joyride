using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.IO;

class Segment : Renderable
{
    private float X;

    private List<Coin> coins;
    private List<Laser> lasers;

    private static Texture laserTexture = Engine.LoadTexture("vertical.png");
    private static Texture coinTexture = Engine.LoadTexture("coin.gif");

    private static Random random = new Random();

    private static XElement GetFirstElementByTagName(XElement document, String tagName)
    {
        return (from el in document.Elements()
                where el.Name == tagName
                select el).ToList()[0];
    }
    
    private static String GetAttribute(XElement e, String attribute)
    {
        return e.Attribute(attribute).Value;
    }

    private static List<Vector2> GetAllPositions(XElement document)
    {
        List<Vector2> positions = new List<Vector2>();
        foreach (XElement e in document.Elements())
        {
            float X = float.Parse(GetAttribute(e, "x"));
            float Y = float.Parse(GetAttribute(e, "y"));
            positions.Add(new Vector2(X, Y));
        }
        return positions;
    }

    private static List<Vector2> RelativeToAbsolute(List<Vector2> relativePositions, float offset)
    {
        List<Vector2> absolutePositions = new List<Vector2>();
        
        foreach (Vector2 rel in relativePositions)
        {
            absolutePositions.Add(
                new Vector2(
                    rel.X + offset,
                    rel.Y
                )
            );
        }

        return absolutePositions;
    }
    
    private static Segment LoadSegmentFromFile(float X, int segmentNumber)
    {
        String filename = segmentNumber + ".xml";
        String filepath = Directory.GetCurrentDirectory() + "/Assets/segments/" + filename;

        XElement root = XElement.Load(filepath);
        XElement coins = GetFirstElementByTagName(root, "coins");
        XElement lasers = GetFirstElementByTagName(root, "lasers");

        List<Vector2> coinPositions = RelativeToAbsolute(GetAllPositions(coins), X);
        List<Vector2> laserPositions = RelativeToAbsolute(GetAllPositions(lasers), X);
        
        return new Segment(X, laserPositions, coinPositions);
    }
    
    private static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    
    public static Segment[] GenerateSegments(float startX)
    {
        Segment[] segments = new Segment[13];

        List<int> segmentNumbers = Enumerable.Range(1, 13).ToList();
        Shuffle(segmentNumbers);

        for (int i = 0; i < segmentNumbers.Count; i++)
        {
            float currentX = startX + 500 * i;
            segments[i] = LoadSegmentFromFile(currentX, segmentNumbers[i]);
        }

        return segments;
    }

    public Segment(float X, List<Vector2> laserPositions, List<Vector2> coinPositions)
    {
        this.X = X;
        this.lasers = new List<Laser>();
        this.coins = new List<Coin>();
        
        foreach(Vector2 position in laserPositions)
        {
            this.lasers.Add(new Laser(laserTexture, position));
        }

        foreach(Vector2 position in coinPositions)
        {
            this.coins.Add(new Coin(coinTexture, position));
        }
    }

    public void HandleInput()
    {
    }

    public void Move(Camera camera)
    {
    }

    public void Render(Camera camera)
    {
        foreach (Coin c in coins)
        {
            c.Render(camera);
        }

        foreach (Laser l in lasers)
        {
            l.Render(camera);
        }
    }
}
