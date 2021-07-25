using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TetrisSFML.GamePlayClasses
{
    internal class GameGrid
    {
        private readonly Size _size;
        private bool[,] _area;

        public GameGrid(Size size)
        {
            _size = size;
            _area = new bool[size.Width, size.Height];
        }

        public void DrawShape(IEnumerable<Point> points)
        {
            foreach (Point point in points)
            {
                _area[point.X, point.Y] = true;
            }
        }

        public void EraseShape(IEnumerable<Point> points)
        {
            foreach (Point point in points)
            {
                _area[point.X, point.Y] = false;
            }
        }

        public void CompleteRow(IEnumerable<int> rows)
        {
            var newArea = new bool[_size.Width, _size.Height];
            int areaOffset = 0;

            for (int i = 0; i < _size.Width; i++)
            {
                for (int j = _size.Height; j >= 0; j--)
                {
                    if (rows.Contains(j))
                    {
                        areaOffset++;
                        continue;
                    }

                    newArea[i, j] = _area[i, j - areaOffset];
                }
            }

            _area = newArea;
        }
    }
}