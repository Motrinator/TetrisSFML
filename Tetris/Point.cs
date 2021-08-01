using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisSFML.Tetris
{
    public readonly struct Point : IEquatable<Point>
    {
        public static readonly Point Up = new(-1, 0);
        public static readonly Point Down = new(1, 0);
        public static readonly Point Left = new(0, -1);
        public static readonly Point Right = new(0, 1);

        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point operator+(Point pointLeft, Point pointRight)
        {
            return new Point (pointLeft.X + pointRight.X, pointLeft.Y + pointRight.Y);
        }

        public static Point operator-(Point pointLeft, Point pointRight)
        {
            return new Point (pointLeft.X - pointRight.X, pointLeft.Y - pointRight.Y);
        }

        public static Point operator/(Point pointLeft, double divider)
        {
            return new Point(Convert.ToInt32(Math.Round(pointLeft.X / divider)), Convert.ToInt32(Math.Round(pointLeft.Y / divider)));
        }

        public static Point operator*(Point pointLeft, int multiplier)
        {
            return new Point(pointLeft.X * multiplier, pointLeft.Y * multiplier);
        }

        public static bool operator==(Point pointLeft, Point pointRight)
        {
            return pointLeft.Equals(pointRight);
        }

        public static bool operator!=(Point pointLeft, Point pointRight)
        {
            return !pointLeft.Equals(pointRight);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Point point)
            {
                return InternalEquals(point);
            }

            return base.Equals(obj);
        }

        public bool Equals(Point other)
        {
            return InternalEquals(other);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = (hash * 23) + X.GetHashCode();
            hash = (hash * 23) + Y.GetHashCode();

            return hash;
        }

        private bool InternalEquals(in Point other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
    }
}
