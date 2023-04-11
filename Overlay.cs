namespace Layouts;

public class Overlay : Container {
    public override void ComputeRect(Rect rect, Rect visibleRect) {
        base.ComputeRect(rect, visibleRect);
        foreach (var child in Children) {
            child.ComputeRect(rect, visibleRect);
        }
    }
}