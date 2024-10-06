using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordSearchPuzzleSolver
{
    public class WordSearchPuzzleInput
    {
        public char[,] Matrix { get; set; }
        public List<string> SearchedWords { get; set; }
        public WordSearchPuzzleInput(string stringMatrix, string stringSearchedWords)
        {
            //Sanitaze input
            stringMatrix = stringMatrix.ToUpper();
            stringSearchedWords = stringSearchedWords.ToUpper();
            var lines = stringMatrix.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < lines.Length; i++)
                lines[i] = Regex.Replace(lines[i], @"[^a-zA-Z]", "");
            int size = lines[0].Length;

            //All lines must have same count of characters
            foreach(var line in lines)
                if (line.Length != size)
                    throw new Exception("Bad input for stringMatrix - every line must have same count of characters to create the matrix.");

            Matrix = new char[lines.Length, size];
            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < size; j++)
                    Matrix[i, j] = lines[i][j];

            SearchedWords = stringSearchedWords.Split(null).ToList();
        }
    }
}
