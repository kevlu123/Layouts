using System.Collections;

namespace Layouts;

public abstract class Container : View, IEnumerable<View> {
    protected List<View> Children { get; } = new();

    public override List<View> HitTest(int x, int y) {
        if (!Interactable || !VisibleRect.Contains(x, y)) {
            return new();
        }
        foreach (var child in Children) {
            var hits = child.HitTest(x, y);
            if (hits.Count > 0) {
                hits.Add(this);
                return hits;
            }
        }
        return new() { this };
    }

    public override void Draw() {
        if (Visible) {
            foreach (var child in Children) {
                if (child.Visible) {
                    child.Draw();
                }
            }
        }
    }

    public void Add(View child) {
        Children.Add(child);
    }

    public IEnumerator<View> GetEnumerator() {
        return Children.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}