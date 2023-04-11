namespace Layouts;

public class Grid : ScrollContainer {
    public SizeCalculation ColumnWidth { get; set; } = (w, h) => w / 8;
    public SizeCalculation RowHeight { get; set; }
    public int ColumnCount { set => ColumnWidth = (w, h) => w / value; }
    public float CellAspect { set => RowHeight = (w, h) => (int)(ColumnWidth(w, h) / value); }
    public Alignment GridHorizontalAlignment { get; set; } = Alignment.Start;
    public Alignment GridVerticalAlignment { get; set; } = Alignment.Start;

    public Grid() {
        VerticalScroll = true;
        RowHeight = (w, h) => ColumnWidth(w, h);
    }

    public override void ComputeRect(Rect rect, Rect visibleRect) {
        base.ComputeRect(rect, visibleRect);

        int columnWidth = ColumnWidth(rect.Width, rect.Height);
        int rowHeight = RowHeight(rect.Width, rect.Height);
        int columnCount = Math.Max(1, rect.Width / columnWidth);
        int rowCount = (Children.Count - 1) / columnCount + 1;

        int usedWidth = Math.Min(Children.Count, columnCount) * columnWidth;;
        int usedHeight = rowCount * rowHeight;
        int freeWidth = Math.Max(0, rect.Width - usedWidth);
        int freeHeight = Math.Max(0, rect.Height - usedHeight);

        int initialY = GridVerticalAlignment switch {
            Alignment.Start => rect.Y,
            Alignment.Centre => rect.Y + freeHeight / 2,
            Alignment.End => rect.Y + freeHeight,
            _ => throw new InvalidOperationException("Invalid vertical alignment"),
        };
        int initialX = GridHorizontalAlignment switch {
            Alignment.Start => rect.X,
            Alignment.Centre => rect.X + freeWidth / 2,
            Alignment.End => rect.X + freeWidth,
            _ => throw new InvalidOperationException("Invalid horizontal alignment"),
        };

        var y = initialY - (int)Scroll;

        for (int i = 0; i < Children.Count; i++) {
            var child = Children[i];
            int col = i % columnCount;
            
            var childRect = new Rect(
                initialX + col * columnWidth,
                y,
                child.Size.Width(columnWidth, rowHeight),
                child.Size.Height(columnWidth, rowHeight)
            );
            var childVisibleRect = childRect.Intersect(visibleRect);
            child.ComputeRect(childRect, childVisibleRect);

            if (col == columnCount - 1) {
                y += childRect.Height;
            }
        }

        ScrollMax = Math.Max(0, usedHeight - rect.Height);
    }
}