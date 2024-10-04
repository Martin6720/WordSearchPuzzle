using System.Linq;

namespace WordSearchPuzzleSolver
{
    public class Solver
    {
        public (char, bool)[,] Matrix { get; set; }
        public Dictionary<char, List<(int, int)>> CharMap { get; set; } = new Dictionary<char, List<(int, int)>>();
        WordSearchRules Rules { get; set; }
        public List<string> SearchedWords { get; set; }
        public List<PuzzleWord> FoundWords { get;} = new List<PuzzleWord>();
        public string Solution { get; private set; }
        public Solver(WordSearchPuzzleInput input, WordSearchRules rules) 
        {
            SearchedWords = input.SearchedWords;
            Rules = rules;
            ConfigureMatrix(input.Matrix);
            ConfigureRules(rules);
            CreateCharMap();
            Solve();
        }
        private void CreateCharMap()
        {
            CharMap.Clear();
            //rows
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                //columns
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (CharMap.ContainsKey(Matrix[i, j].Item1))
                        CharMap[Matrix[i, j].Item1].Add((i, j));
                    else
                        CharMap.Add(Matrix[i, j].Item1, new List<(int, int)>() { { (i, j) } });
                }
            }
        }
        public void Solve()
        {
            FoundWords.Clear();
            foreach (string word in SearchedWords)
            {
                var startingChar = CharMap[word[0]];
                foreach(var v in startingChar)
                {
                    FindWord(word, v);
                }
                SolveMatrix();
                GetSolutionFromSolvedMatrix();
            }
        }
        public void SolveMatrix()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int n = 0; n < Matrix.GetLength(1); n++)
                {
                    if (FoundWords.Any(x => x.LocationsCovered.Contains((i, n))))
                        Matrix[i, n] = (Matrix[i, n].Item1, false);
                }
            }
        }
        public void GetSolutionFromSolvedMatrix()
        {
            string solution = "";
            for(int i = 0;i < Matrix.GetLength(0);i++)
            {
                for(int n = 0; n < Matrix.GetLength(1);n++)
                {
                    if (Matrix[i, n].Item2)
                        solution += Matrix[i, n].Item1;
                }
            }
            Solution = solution;
        }
        private void FindWord(string word, (int, int) firstCharLocation)
        {
            foreach(var direction in Rules.AllowedDirections)
            {
                if (CheckWordInDirection(word, firstCharLocation, direction))
                    FoundWords.Add(new PuzzleWord(word, firstCharLocation, direction));
            }
        }
        private bool CheckWordInDirection(string word, (int, int) firstCharLocation, (int, int) rowColDirection)
        {
            (int, int) currentLocation = firstCharLocation;
            for (int i = 1; i < word.Length; i++)
            {
                currentLocation.Item1 += rowColDirection.Item1;
                currentLocation.Item2 += rowColDirection.Item2;
                if (!CharMap[word[i]].Contains(currentLocation))
                    return false;
            }
            return true;
        }        
        public void ConfigureRules(WordSearchRules rules)
        {
            Rules = rules.ConfigureRules();
        }
        public void ConfigureMatrix(char[,] matrix)
        {
            Matrix = new (char, bool)[matrix.GetLength(0), matrix.GetLength(1)];
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    Matrix[i,j] = (matrix[i,j], true);
                }
            }
        }
    }
}
