using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisSFML.Tetris.Tetraminos
{
    public class Tetramino
    {
        private const float _rotationStep = MathF.PI / -2;

        public byte TypeId { get; }
        public Point Position { get; private set; }
        public IEnumerable<Point> PointPositions
        {
            get
            {
                return _points.Select(x => x + Position);
            }
        }

        protected readonly Point[] _points;
        protected readonly Grid _grid;

        public Tetramino(byte typeId, byte? rotation, Point[] points, Grid grid)
        {
            Console.WriteLine("Created tetromino {0}, rotation {1}", typeId, rotation);
            _points = points ?? throw new ArgumentNullException(nameof(points));
            _grid = grid ?? throw new ArgumentNullException(nameof(grid));
            TypeId = typeId;
            Position = new Point(-_points.Min(x => x.X) - 2, Grid.Columns / 2);

            if (rotation.HasValue)
            {
                Rotate(rotation.Value);
            }
        }

        public bool TryDown()
        {
            Span<Point> newPoints = stackalloc Point[_points.Length];

            for (int i = 0; i < _points.Length; i++)
            {
                newPoints[i] = _points[i] + Position + Point.Down;
            }

            if (_grid.HasCollision(newPoints))
            {
                for (int i = 0; i < _points.Length; i++)
                {
                    newPoints[i] -= Point.Down;
                }

                _grid.CommitPoints(newPoints, TypeId);

                return false;
            }

            Position += Point.Down;

            return true;
        }

        public virtual void Rotate()
        {
            Rotate(1);
        }

        public void Move(Shift shift)
        {
            Point shiftPoint = shift switch
            {
                Shift.Left => Point.Left,
                Shift.Right => Point.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(shift))
            };
            Span<Point> newPoints = stackalloc Point[_points.Length];

            for (int i = 0; i < _points.Length; i++)
            {
                newPoints[i] = _points[i] + Position + shiftPoint;
            }

            if (!_grid.HasCollision(newPoints))
            {
                Position += shiftPoint;
            }
        }

        private void Rotate(int rotateCount)
        {
            if (rotateCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rotateCount));
            }

            if (rotateCount == 0)
            {
                return;
            }

            Point centerPoint = _points.Aggregate((x, y) => x + y) / _points.Length;

            for (int i = 0; i < _points.Length; i++)
            {
                float angleSin = MathF.Sin(_rotationStep * rotateCount);
                float angleCos = MathF.Cos(_rotationStep * rotateCount);

                Point point = _points[i] - centerPoint;
                int newX = Convert.ToInt32((point.X * angleCos) - (point.Y * angleSin));
                int newY = Convert.ToInt32((point.X * angleSin) - (point.Y * angleCos));

                _points[i] = new Point(newX, newY) + centerPoint;
            }

            Position = _grid.CheckAndFixPoints(Position, _points);
        }
    }
}
