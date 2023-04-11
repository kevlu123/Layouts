namespace Layouts;

public class MouseEventArgs : EventArgs {
    public Vector2 Position { get; set; }
    public MouseButton Button { get; set; }
}

public class MouseScrollEventArgs : EventArgs {
    public Vector2 Position { get; set; }
    public Vector2f Delta { get; set; }
    public bool Handled { get; set; }
}