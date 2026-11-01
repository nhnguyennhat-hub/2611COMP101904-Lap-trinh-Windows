using System.Drawing;
namespace COMP1019_Lab08_SimplePaint;
public enum DrawMode { Pen, Line, Rectangle, Ellipse }
public sealed class DrawItem
{
    public DrawMode Mode { get; set; }
    public Point Start { get; set; }
    public Point End { get; set; }
    public Color Color { get; set; }
    public float Width { get; set; }
    public List<Point> Points { get; } = new();
    public DrawItem(DrawMode mode, Point start, Color color, float width)
    {
        Mode = mode; Start = start; End = start; Color = color; Width = width;
        Points.Add(start);
    }
}
