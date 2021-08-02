using System;

namespace TetrisSFML.Tetris.Tetraminos
{
    public class LineTetramino : Tetramino
    {
        private const byte _rotatePositionCount = 4;

        private int _rotation;

        public LineTetramino(byte typeId, byte rotation, Point[] points, Grid grid) : base(typeId, rotation, points, grid)
        {
            _rotation = rotation;
        }

        public override void Rotate()
        {
            _rotation = (_rotation + 1) % _rotatePositionCount;
            var centerPoint = _rotation switch
            {
                0 or 2 => _points[2],
                1 or 3 => _points[1],
                _ => throw new IndexOutOfRangeException(),
            };

            Rotate(centerPoint, -1);
        }
    }
}
