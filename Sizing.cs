namespace Layouts;

public delegate int SizeCalculation(int parentWidth, int parentHeight);

public class Sizing {
    public SizeCalculation Width { get; set; } = (w, h) => w;
    public SizeCalculation Height { get; set; } = (w, h) => h;
    public int FixedWidth { set => Width = (w, h) => value; }
    public int FixedHeight { set => Height = (w, h) => value; }
    public float ProportionalWidth { set => Width = (w, h) => (int)(value * w); }
    public float ProportionalHeight { set => Height = (w, h) => (int)(value * h); }
    public bool FillWidth { get; set; }
    public bool FillHeight { get; set; }
    public float AspectByWidth { set => Height = (w, h) => (int)(Width(w, h) / value); }
    public float AspectByHeight { set => Width = (w, h) => (int)(Height(w, h) * value); }
}