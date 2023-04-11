namespace Layouts;

public class Root {
    private Vector2 mousePosition;
    private View? mouseOver;
    private List<View> mouseOverHierarchy = new();
    private readonly View?[] mouseDrag = new View?[5];
    private readonly bool[] mouseState = new bool[5];
    private View? focus;

    public View RootView { get; set; }
    public View? Focus {
        get => focus;
        set {
            if (value == focus) {
                return;
            }
            focus?.OnFocusLost();
            value?.OnFocusGained();
            focus = value;
        }
    }

    public Root(View child) {
        RootView = child;
    }
    
    public void ComputeRect(Rect rect) {
        RootView.ComputeRect(rect, rect);
    }

    public void Draw() {
        if (RootView.Visible) {
            RootView.Draw();
        }
    }

    public void Update() {
        var hits = RootView.Interactable
            ? RootView.HitTest(mousePosition.X, mousePosition.Y)
            : new List<View>();
        var hit = hits.FirstOrDefault();
        if (hit != mouseOver) {
            mouseOver?.OnMouseLeave();
            hit?.OnMouseEnter();
        }
        mouseOver = hit;
        mouseOverHierarchy = hits;
    }

    public void RegisterMouseMove(int x, int y) {
        mousePosition = new Vector2(x, y);
        for (int i = 0; i < mouseDrag.Length; i++) {
            mouseDrag[i]?.OnMouseDrag(new MouseEventArgs { Position = mousePosition, Button = (MouseButton)i });
        }
    }

    public void RegisterMouseButton(MouseButton button, bool down) {
        var b = (int)button;
        var e = new MouseEventArgs { Position = mousePosition, Button = button };
        if (down) {
            if (!mouseState[b]) {
                mouseOver?.OnMouseDown(e);
                mouseDrag[b] = mouseOver;
                mouseState[b] = true;
                Focus = mouseOver;
            }
        } else {
            if (mouseState[b]) {
                mouseDrag[b]?.OnMouseUp(e);
                mouseDrag[b] = null;
                mouseState[b] = false;
            }
        }
    }

    public void RegisterMouseScroll(float deltaX, float deltaY) {
        foreach (var view in mouseOverHierarchy) {
            var e = new MouseScrollEventArgs { Position = mousePosition, Delta = new Vector2f(deltaX, deltaY) };
            view.OnMouseScroll(e);
            if (e.Handled) {
                break;
            }
        }
    }
}