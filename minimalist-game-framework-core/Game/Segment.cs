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

    private static Dictionary<LaserType, Texture> laserTextureMap = new Dictionary<LaserType, Texture> {
        { LaserType.HorizontalShort, Engine.LoadTexture("horizontalshort.png") },
        { LaserType.HorizontalLong, Engine.LoadTexture("horizontallong.png") },
        { LaserType.VerticalShort, Engine.LoadTexture("verticalshort.png") },
        { LaserType.VerticalLong, Engine.LoadTexture("verticallong.png") },
        { LaserType.DiagonalDownShort, Engine.LoadTexture("diagonaldownshort.png") },
        { LaserType.DiagonalDownLong, Engine.LoadTexture("diagonaldownlong.png") },
        { LaserType.DiagonalUpShort, Engine.LoadTexture("diagonalupshort.png") },
        { LaserType.DiagonalUpLong, Engine.LoadTexture("diagonaluplong.png") },
    };

    private static Texture coinTexture = Engine.LoadTexture("coin.png");

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
    
    private static List<LaserType> GetAllLaserTypes(XElement document)
    {
        List<LaserType> laserTypes = new List<LaserType>();

        foreach (XElement e in document.Elements())
        {
            LaserType type = (LaserType)Enum.Parse(typeof(LaserType), GetAttribute(e, "type"));
            laserTypes.Add(type);
        }

        return laserTypes;
    }
    
    private static Segment LoadSegmentFromFile(float X, int segmentNumber, Character character)
    {
        String filename = segmentNumber + ".xml";
        String filepath = Directory.GetCurrentDirectory() + "/Assets/segments/" + filename;

        XElement root = XElement.Load(filepath);
        XElement coins = GetFirstElementByTagName(root, "coins");
        XElement lasers = GetFirstElementByTagName(root, "lasers");

        List<Vector2> coinPositions = RelativeToAbsolute(GetAllPositions(coins), X);
        List<Vector2> laserPositions = RelativeToAbsolute(GetAllPositions(lasers), X);
        List<LaserType> laserTypes = GetAllLaserTypes(lasers);

        return new Segment(X, laserPositions, laserTypes, coinPositions, character);
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
    
    private static bool SegmentIsTunnel(int segment)
    {
        return segment == 0  ||
               segment == 1  ||
               segment == 11 ||
               segment == 12;
    }
    
    public static Segment[] GenerateSegments(float startX, Character character)
    {
        Segment[] segments = new Segment[13];

        List<int> segmentNumbers = Enumerable.Range(1, 13).ToList();
        Shuffle(segmentNumbers);

        for (int i = 0; i < segmentNumbers.Count; i++)
        {
            float currentX = startX + 500 * i;

            // Special cases:
            // If it's the first or last two segments, don't have any lasers or coins, because it's part of the tunnel
            if (SegmentIsTunnel(i))
            {
                segments[i] = new Segment(currentX);
                continue;
            }

            segments[i] = LoadSegmentFromFile(currentX, segmentNumbers[i], character);
        }

        return segments;
    }

    public Segment(float X, List<Vector2> laserPositions, List<LaserType> laserTypes, List<Vector2> coinPositions, Character character)
    {
        this.X = X;
        this.lasers = new List<Laser>();
        this.coins = new List<Coin>();

        foreach(var laser in laserTypes.Zip(laserPositions, (type, position) => (type, position)))
        {
            this.lasers.Add(new Laser(laserTextureMap[laser.type], laser.type, laser.position));
        }

        foreach(Vector2 position in coinPositions)
        {
            this.coins.Add(new Coin(coinTexture, position, character));
        }
    }

    public Segment(float X)
    {
        this.X = X;
        this.lasers = new List<Laser>();
        this.coins = new List<Coin>();
    }

    public void HandleInput()
    {
        foreach(Coin c in coins)
        {
            c.HandleInput();
        }

        foreach(Laser l in lasers)
        {
            l.HandleInput();
        }
    }

    public void Move(Camera camera)
    {
        foreach(Coin c in coins)
        {
            c.Move(camera);
        }

        foreach(Laser l in lasers)
        {
            l.Move(camera);
        }
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
