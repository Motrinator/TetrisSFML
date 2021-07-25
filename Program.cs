using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Threading;

namespace TetrisSFML
{
    internal static class Program
    {
        private const int CellSise = 8;
        private const int Collumns = 10;
        private const int ScreenResize = 4;
        private const int Rows = 20;

        private static int x = 0, y = 0;
        private static readonly bool[,] matrix = new bool[Collumns, Rows];

        private static void Main(string[] args)
        {
            using RenderWindow renderWindow = new(new VideoMode(2 * CellSise * Collumns * ScreenResize, CellSise * Rows * ScreenResize), "Tetris", Styles.Close);

            renderWindow.SetView(new View(new FloatRect(0, 0, 2 * CellSise * Collumns, CellSise * Rows)));
            renderWindow.SetFramerateLimit(60);

            using RectangleShape rectangleShape = new(new Vector2f(100, 100));

            rectangleShape.FillColor = Color.Green;
            renderWindow.Closed += (x, y) => renderWindow.Close();

            matrix[0, 0] = true;

            renderWindow.KeyReleased += KeyEventHandler;

            while (renderWindow.IsOpen)
            {
                renderWindow.WaitAndDispatchEvents();
                renderWindow.Clear();

                Draw(renderWindow, matrix);
            }
        }

        public static void KeyEventHandler(object sender, KeyEventArgs keyEventArgs)
        {
            matrix[x, y] = false;

            switch (keyEventArgs.Code)
            {
                case Keyboard.Key.S:
                    y++;
                    break;
                case Keyboard.Key.D:
                    x++;
                    break;
                case Keyboard.Key.A:
                    x--;
                    break;
            }

            matrix[x, y] = true;
        }

        public static void Draw(RenderWindow renderWindow, bool[,] matrix)
        {
            var cell = new RectangleShape(new Vector2f(CellSise - 1, CellSise - 1))
            {
                FillColor = Color.White
            };

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    cell.Position = new Vector2f(CellSise * i, CellSise * j);

                    if (matrix[i, j])
                    {
                        cell.FillColor = Color.Blue;
                    }
                    else
                    {
                        cell.FillColor = Color.White;
                    }

                    renderWindow.Draw(cell);
                }
            }

            renderWindow.Display();
        }
    }
}
