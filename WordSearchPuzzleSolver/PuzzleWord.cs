using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearchPuzzleSolver
{
    public class PuzzleWord
    {
        public string Word { get; }
        public (int, int) Location { get; }
        public (int, int) Direction { get; }
        private List<(int, int)>? _LocationsCovered;
        public List<(int, int)> LocationsCovered
        {
            get
            {
                if (_LocationsCovered == null || !_LocationsCovered.Any())
                {
                    _LocationsCovered = new List<(int, int)>();
                    for (int i = 0; i < Word.Length; i++)
                        _LocationsCovered.Add((Location.Item1 + i * Direction.Item1, Location.Item2 + i * Direction.Item2));
                }
                return _LocationsCovered;
            }
        }
        public PuzzleWord(string word, (int, int) location, (int, int) direction)
        {
            Word = word;
            Location = location;
            Direction = direction;
        }
    }
}
