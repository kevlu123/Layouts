namespace Layouts;

public class Padding : Container {
    public int Top { get; set; }
    public int Bottom { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }

    public Padding(View child) : this(0, child) { }
    public Padding(int lrtb, View child) : this(lrtb, lrtb, child) { }
    public Padding(int lr, int tb, View child) : this(lr, lr, tb, tb, child) { }
    public Padding(int left, int right, int top, int bottom, View child) {
        Top = top;
        Bottom = bottom;
        Left = left;
        Right = right;
        Children.Add(child);

        Size = child.Size;
        HorizontalAlignment = child.HorizontalAlignment;
        VerticalAlignment = child.VerticalAlignment;
    }
    
    public override void ComputeRect(Rect rect, Rect visibleRect) {
        base.ComputeRect(rect, visibleRect);
        var childRect = new Rect(
            rect.X + Left,
            rect.Y + Top,
            rect.Width - Left - Right,
            rect.Height - Top - Bottom
        );
        Children[0].ComputeRect(childRect, childRect.Intersect(visibleRect));
    }
}