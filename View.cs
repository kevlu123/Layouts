namespace Layouts;

public abstract class View {
    public Sizing Size { get; set; } = new() { ProportionalWidth = 1, ProportionalHeight = 1 };

    public Rect ComputedRect { get; set; }
    public Rect VisibleRect { get; set; }

    public Alignment HorizontalAlignment { get; set; } = Alignment.Centre;
    public Alignment VerticalAlignment { get; set; } = Alignment.Centre;
    
    private bool visible = true;
    private bool interactable = true;
    public bool Visible { get => visible && VisibleRect.HasPositiveSize; set => visible = value; }
    public bool Interactable { get => interactable && VisibleRect.HasPositiveSize; set => interactable = value; }

    public virtual void ComputeRect(Rect rect, Rect visibleRect) {
        ComputedRect = rect;
        VisibleRect = visibleRect;
    }

    public virtual List<View> HitTest(int x, int y) {
        if (!Interactable || !VisibleRect.Contains(x, y)) {
            return new();
        }
        return new() { this };
    }

    public abstract void Draw();
    public virtual void OnMouseEnter() { }
    public virtual void OnMouseLeave() { }
    public virtual void OnMouseDown(MouseEventArgs e) { }
    public virtual void OnMouseUp(MouseEventArgs e) { }
    public virtual void OnMouseDrag(MouseEventArgs e) { }
    public virtual void OnMouseScroll(MouseScrollEventArgs e) { }
    public virtual void OnFocusGained() { }
    public virtual void OnFocusLost() { }
}