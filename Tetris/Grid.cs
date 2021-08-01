using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisSFML.Tetris
{
    using ReadOnlyByteMatrix = IReadOnlyList<IReadOnlyList<byte>>;

    public delegate void RemoveEvent(ReadOnlyByteMatrix area, IEnumerable<int> removedRows);

    public class Grid
    {
        public const int Columns = 10;
        public const int Rows = 20;

        public event RemoveEvent OnRemoveRows;
        public event RemoveEvent OnRemovedRows;
        public event Action<ReadOnlyByteMatrix> OnCommitedPoints;
        public event Action OnGameOver;
        public ReadOnlyByteMatrix Area { get => _area; }

        private readonly List<byte[]> _area = new(Rows);

        public Grid(RemoveEvent onRemoveRows, RemoveEvent onRemovedRows, Action<ReadOnlyByteMatrix> onCommitedPoints, Action onGameOver)
        {
            OnRemoveRows = onRemoveRows ?? throw new ArgumentNullException(nameof(onRemoveRows));
            OnRemovedRows = onRemovedRows ?? throw new ArgumentNullException(nameof(onRemovedRows));
            OnCommitedPoints = onCommitedPoints ?? throw new ArgumentNullException(nameof(onCommitedPoints));
            OnGameOver = onGameOver ?? throw new ArgumentNullException(nameof(onGameOver));

            for (int i = 0; i < Rows; i++)
            {
                _area.Add(new byte[Columns]);
            }
        }

        public bool HasCollision(in ReadOnlySpan<Point> points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].Y < 0 || points[i].Y >= Columns || points[i].X >= Rows || (points[i].X >= 0 && _area[points[i].X][points[i].Y] > 0))
                {
                    return true;
                }
            }

            return false;
        }

        public Point CheckAndFixPoints(Point point, in Span<Point> points)
        {
            Point newPoint = point;

            for (int i = 0; i < points.Length; i++)
            {
                Point offsetPoint = point + points[i];

                if (offsetPoint.Y < 0)
                {
                    newPoint = new Point(point.X, 0);
                }
                else if (offsetPoint.Y >= Columns)
                {
                    newPoint = new Point(point.X, Columns - points[i].Y - 1);
                }

                if (offsetPoint.X < 0)
                {
                    continue;
                }
                else if (offsetPoint.X >= Rows)
                {
                    newPoint = new Point(Rows - points[i].X - 1, newPoint.Y);
                }

                offsetPoint = newPoint + points[i];

                while (_area[offsetPoint.X][offsetPoint.Y] > 0)
                {
                    newPoint += Point.Up;
                    offsetPoint += Point.Up;
                }
            }

            return newPoint;
        }

        public void CommitPoints(in ReadOnlySpan<Point> points, byte tetraminoType)
        {
            if (tetraminoType == 0)
            {
                throw new IndexOutOfRangeException(nameof(tetraminoType));
            }

            var destructedRows = new HashSet<int>();
            bool gameOver = false;

            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].Y < 0 && !gameOver)
                {
                    gameOver = true;

                    continue;
                }

                _area[points[i].X][points[i].Y] = tetraminoType;

                if(!destructedRows.Contains(points[i].X) && _area[points[i].X].All(y => y > 0))
                {
                    destructedRows.Add(points[i].X);
                }
            }

            if (gameOver)
            {
                OnGameOver.Invoke();

                return;
            }

            OnCommitedPoints(_area);

            if (destructedRows.Count > 0)
            {
                OnRemoveRows(_area, destructedRows);
            }

            foreach (int distructedRow in destructedRows)
            {
                _area.RemoveAt(distructedRow);
                _area.Insert(0, new byte[Columns]);
            }

            OnRemovedRows(_area, destructedRows);
        }
    }
}
