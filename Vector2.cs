namespace Layouts;

public struct Vector2 {
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x, int y) {
        X = x;
        Y = y;
    }

    public static Vector2 operator+(Vector2 a, Vector2 b) {
        return new(a.X + b.X, a.Y + b.Y);
    }

    public static Vector2 operator-(Vector2 a, Vector2 b) {
        return new(a.X - b.X, a.Y - b.Y);
    }

    public static Vector2 operator*(Vector2 a, int b) {
        return new(a.X * b, a.Y * b);
    }

    public static Vector2 operator/(Vector2 a, int b) {
        return new(a.X / b, a.Y / b);
    }

    public static Vector2 operator-(Vector2 a) {
        return new(-a.X, -a.Y);
    }

    public static bool operator==(Vector2 a, Vector2 b) {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator!=(Vector2 a, Vector2 b) {
        return !(a == b);
    }

    public override readonly bool Equals(object? obj) {
        return obj is Vector2 vector && this == vector;
    }

    public override readonly int GetHashCode() {
        return HashCode.Combine(X, Y);
    }
}

public struct Vector2f {
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2f(float x, float y) {
        X = x;
        Y = y;
    }

    public static Vector2f operator+(Vector2f a, Vector2f b) {
        return new(a.X + b.X, a.Y + b.Y);
    }

    public static Vector2f operator-(Vector2f a, Vector2f b) {
        return new(a.X - b.X, a.Y - b.Y);
    }

    public static Vector2f operator*(Vector2f a, float b) {
        return new(a.X * b, a.Y * b);
    }

    public static Vector2f operator/(Vector2f a, float b) {
        return new(a.X / b, a.Y / b);
    }

    public static Vector2f operator-(Vector2f a) {
        return new(-a.X, -a.Y);
    }

    public static bool operator==(Vector2f a, Vector2f b) {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator!=(Vector2f a, Vector2f b) {
        return !(a == b);
    }

    public override readonly bool Equals(object? obj) {
        return obj is Vector2f vector && this == vector;
    }

    public override readonly int GetHashCode() {
        return HashCode.Combine(X, Y);
    }
}