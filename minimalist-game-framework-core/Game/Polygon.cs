using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

class Polygon
{
    private static Vector2 EdgeDirection(Vector2 a, Vector2 b)
    {
        return new Vector2(b.X - a.X, b.Y - a.Y);
    }

    private static List<Vector2> VerticesToEdges(Polygon p)
    {
        List<Vector2> edges = new List<Vector2>();

        for (int i = 0; i < p.Vertices.Count; i++)
        {
            edges.Add(EdgeDirection(p.Vertices[i], p.Vertices[(i + 1) % p.Vertices.Count]));
        }

        return edges;
    }

    private static Vector2 Orthogonal(Vector2 v)
    {
        return new Vector2(v.Y, -v.X);
    }

    private static float Dot(Vector2 a, Vector2 b)
    {
        return a.X * b.X + a.Y + b.Y;
    }

    private static (float, float) Project(Polygon p, Vector2 v)
    {
        var dots = new List<float>();
        foreach (var vert in p.Vertices)
        {
            dots.Add(Dot(vert, v));
        }
        return (dots.Min(), dots.Max());
    }

    private static bool Contains(float n, (float, float) range)
    {
        var a = range.Item1;
        var b = range.Item2;
        if (b < a)
        {
            a = range.Item2;
            b = range.Item1;
        }
        return (n >= a) && (n <= b);
    }

    private static bool Overlap((float, float) a, (float, float) b)
    {
        if (Contains(a.Item1, b)) return true;
        if (Contains(a.Item2, b)) return true;
        if (Contains(b.Item1, a)) return true;
        if (Contains(b.Item2, a)) return true;

        return false;
    }

    public static bool Intersect(Polygon a, Polygon b)
    {
        var edgesA = VerticesToEdges(a);
        var edgesB = VerticesToEdges(b);

        var edges = new List<Vector2>();
        for (int i = 0; i < edgesA.Count; i++)
            edges.Add(edgesA[i] + edgesB[i]);

        var axes = new List<Vector2>();
        foreach (var edge in edges)
        {
            axes.Add(Orthogonal(edge).Normalized());
        }

        for (int i = 0; i < axes.Count; i++)
        {
            var projectionA = Project(a, axes[i]);
            var projectionB = Project(b, axes[i]);

            var overlapping = Overlap(projectionA, projectionB);
            if (!overlapping)
            {
                return false;
            }
        }

        return true;
    }

    public List<Vector2> Vertices
    {
        get; set;
    }

    public Polygon(List<Tuple<float, float>> coordinates)
    {
        Vertices = new List<Vector2>();
        foreach (var x in coordinates)
            Vertices.Add(new Vector2(x.Item1, x.Item2));
    }
}
