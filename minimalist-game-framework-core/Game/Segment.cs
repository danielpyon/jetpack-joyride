using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.IO;

class Segment : Renderable
{
    private float X;

    private List<Vector2> laserPositions;
    private List<Vector2> coinPositions;

    private Texture laserTexture;
    private Texture coinTexture;

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

    private static Segment LoadSegmentFromFile(float X, int segmentNumber)
    {
        String filename = segmentNumber + ".xml";
        String filepath = Directory.GetCurrentDirectory() + "/Assets/segments/" + filename;

        XElement root = XElement.Load(filepath);
        XElement coins = GetFirstElementByTagName(root, "coins");
        XElement lasers = GetFirstElementByTagName(root, "lasers");

        List<Vector2> coinPositions = GetAllPositions(coins);
        List<Vector2> laserPositions = GetAllPositions(lasers);
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
        this.laserPositions = laserPositions;
        this.coinPositions = coinPositions;

        laserTexture = Engine.LoadTexture("vertical.png");
        coinTexture = Engine.LoadTexture("coin.gif");
    }

    public void HandleInput()
    {
    }

    public void Move(Camera camera)
    {
    }

    private Vector2 GetRelativePosition(Vector2 position, Camera camera)
    {
        return new Vector2(
            position.X - camera.X,
            position.Y - camera.Y
        );
    }

    public void Render(Camera camera)
    {
        foreach (Vector2 position in coinPositions)
        {
            Engine.DrawTexture(coinTexture, GetRelativePosition(position, camera));
        }
    }
}
