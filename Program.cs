using TetrisSFML.Tetris;

namespace TetrisSFML
{
    internal static class Program
    {
        private static void Main()
        {
            using (var tetrisViewModel = new TetrisViewModel())
            {
                tetrisViewModel.Run();
            }
        }
    }
}
