namespace Layouts;

public struct Rect {
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public int Left { readonly get => X; set => X = value; }
    public int Right => X + Width;
    public int Top { readonly get => Y; set => Y = value; }
    public int Bottom => Y + Height;

    public Vector2 TopLeft => new(X, Y);
    public Vector2 TopRight => new(X + Width, Y);
    public Vector2 BottomLeft => new(X, Y + Height);
    public Vector2 BottomRight => new(X + Width, Y + Height);

    public bool HasPositiveSize => Width > 0 && Height > 0;
    
    public Rect() : this(0, 0, 0, 0) { }
    public Rect(int width, int height) : this(0, 0, width, height) { }
    public Rect(int x, int y, int width, int height) {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public bool Contains(int x, int y) {
        return x >= X && x < Right && y >= Y && y < Bottom;
    }
    
    public Rect Intersect(Rect other) {
        var x = Math.Max(X, other.X);
        var y = Math.Max(Y, other.Y);
        var right = Math.Min(Right, other.Right);
        var bottom = Math.Min(Bottom, other.Bottom);
        return new Rect(x, y, right - x, bottom - y);
    }
}