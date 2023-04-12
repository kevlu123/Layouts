namespace Layouts;

public class VerticalStack : ScrollContainer {
    public Alignment StackAlignment { get; set; } = Alignment.Start;

    public VerticalStack() {
        VerticalScroll = true;
    }

    public override void ComputeRect(Rect rect, Rect visibleRect) {
        base.ComputeRect(rect, visibleRect);

        var nonfill = Children
            .Where(child => !child.Size.FillHeight)
            .ToList();
        var usedSpace = nonfill.Sum(child => child.Size.Height(rect.Width, rect.Height));
        var freeSpace = Math.Max(0, rect.Height - usedSpace);

        int fillsSeen = 0;
        int fillCount = Children.Count - nonfill.Count;

        int initialY;
        if (fillCount > 0) {
            initialY = rect.Y;
        } else {
            initialY = StackAlignment switch {
                Alignment.Start => rect.Y,
                Alignment.Centre => rect.Y + freeSpace / 2,
                Alignment.End => rect.Y + freeSpace,
                _ => throw new InvalidOperationException("Invalid vertical alignment"),
            };
        }
        var y = initialY - (int)Scroll;

        foreach (var child in Children) {
            int height;
            if (child.Size.FillHeight) {
                fillsSeen++;
                height = freeSpace / fillCount;
                if (fillsSeen == fillCount) {
                    height += freeSpace % fillCount;
                }
            } else {
                height = child.Size.Height(rect.Width, rect.Height);
            }

            var width = child.Size.FillWidth ? rect.Width : child.Size.Width(rect.Width, rect.Height);
            var x = child.HorizontalAlignment switch {
                Alignment.Start => rect.X,
                Alignment.Centre => rect.X + (rect.Width - width) / 2,
                Alignment.End => rect.Right - width,
                _ => throw new InvalidOperationException("Invalid horizontal alignment"),
            };

            var childRect = new Rect(x, y, width, height);
            var childVisibleRect = Overflow == Overflow.Visible ? childRect : childRect.Intersect(visibleRect);
            child.ComputeRect(childRect, childVisibleRect);
            y += height;
        }

        ScrollMax = Math.Max(0, usedSpace - rect.Height);
    }
}

public class HorizontalStack : ScrollContainer {
    public Alignment StackAlignment { get; set; } = Alignment.Start;

    public HorizontalStack() {
        HorizontalScroll = true;
    }

    public override void ComputeRect(Rect rect, Rect visibleRect) {
        base.ComputeRect(rect, visibleRect);

        var nonfill = Children
            .Where(child => !child.Size.FillWidth)
            .ToList();
        
        var usedSpace = nonfill.Sum(child => child.Size.Width(rect.Width, rect.Height));
        var freeSpace = Math.Max(0, rect.Width - usedSpace);

        int fillsSeen = 0;
        int fillCount = Children.Count - nonfill.Count;
        
        int initialX;
        if (fillCount > 0) {
            initialX = rect.X;
        } else {
            initialX = StackAlignment switch {
                Alignment.Start => rect.X,
                Alignment.Centre => rect.X + freeSpace / 2,
                Alignment.End => rect.X + freeSpace,
                _ => throw new InvalidOperationException("Invalid horizontal alignment"),
            };
        }
        var x = initialX - (int)Scroll;

        foreach (var child in Children) {
            int width;
            if (child.Size.FillWidth) {
                fillsSeen++;
                width = freeSpace / fillCount;
                if (fillsSeen == fillCount) {
                    width += freeSpace % fillCount;
                }
            } else {
                width = child.Size.Width(rect.Width, rect.Height);
            }

            var height = child.Size.FillHeight ? rect.Height : child.Size.Height(rect.Width, rect.Height);
            var y = child.VerticalAlignment switch {
                Alignment.Start => rect.Y,
                Alignment.Centre => rect.Y + (rect.Height - height) / 2,
                Alignment.End => rect.Bottom - height,
                _ => throw new InvalidOperationException("Invalid vertical alignment"),
            };
            var childRect = new Rect(x, y, width, height);
            var childVisibleRect = Overflow == Overflow.Visible ? childRect : childRect.Intersect(visibleRect);
            child.ComputeRect(childRect, childVisibleRect);
            x += width;
        }

        ScrollMax = Math.Max(0, usedSpace - rect.Width);
    }
}