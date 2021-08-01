using System;

namespace TetrisSFML.Tetris.Tetraminos
{
    public class LineTetramino : Tetramino
    {
        private const byte _rotatePositionCount = 3;

        private int _rotation;

        public LineTetramino(byte typeId, byte rotation, Point[] points, Grid grid) : base(typeId, rotation, points, grid)
        {
            _rotation = rotation;
        }

        public override void Rotate()
        {
            float centerX = (0.5f * _points[1].X) - _points[2].X;
            float centerY = (0.5f * _points[1].Y) - _points[2].Y;

            _rotation = (_rotation + 1) % _rotatePositionCount;

            switch (++_rotation)
            {
                case 0:
                    centerY += 0.5f;
                    break;
                case 1:
                    centerX -= 0.5f;
                    break;
                case 2:
                    centerY -= 0.5f;
                    break;
                case 3:
                    centerX += 0.5f;
                    break;
            }

            for (int i = 0; i < _points.Length; i++)
            {
                float x = _points[i].X - centerX;
                float y = _points[i].Y - centerY;

                _points[i] = new Point(Convert.ToInt32(centerX + y), Convert.ToInt32(centerY + x));
            }
        }
    }
}
