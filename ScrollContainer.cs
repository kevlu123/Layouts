namespace Layouts;

public abstract class ScrollContainer : Container {
    public static float DefaultScrollSpeed { get; set; } = 0.04f;

    public new List<View> Children => base.Children;
    public Overflow Overflow { get; set; } = Overflow.Scroll;
    public float Scroll { get => Math.Clamp(scroll, 0, ScrollMax); set => scroll = value; }
    public float ScrollSpeed { get; set; } = DefaultScrollSpeed;
    public float ScrollMax { get; protected set; }

    protected bool HorizontalScroll { get; set; }
    protected bool VerticalScroll { get; set; }

    private float scroll;
    
    public override void OnMouseScroll(MouseScrollEventArgs e) {
        if (Overflow == Overflow.Scroll) {
            if (HorizontalScroll) {
                Scroll += e.Delta.X * ScrollSpeed;
            }
            if (VerticalScroll) {
                Scroll -= e.Delta.Y * ScrollSpeed;
            }
            e.Handled = true;
        }
    }
}
