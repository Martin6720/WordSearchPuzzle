using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearchPuzzleSolver
{
    public enum WordSearchType
    {
        FourWay,
        EightWay
    }
    public class WordSearchRules
    {
        public WordSearchType WordSearchType { get; set; }
        public List<(int, int)> AllowedDirections { get; set; }
        public WordSearchRules() { }
        public WordSearchRules ConfigureRules()
        {
            WordSearchType = WordSearchType;
            ConfigureWordSearchType(WordSearchType);
            return this;
        }
        public void ConfigureWordSearchType(WordSearchType wordSearchType)
        {
            AllowedDirections = new List<(int, int)>();
            if (wordSearchType == WordSearchType.FourWay)
            {
                AllowedDirections.Add(new(-1, 0));
                AllowedDirections.Add(new(0, -1));
                AllowedDirections.Add(new(0, 1));
                AllowedDirections.Add(new(1, 0));
            }
            if (wordSearchType == WordSearchType.EightWay)
            {
                AllowedDirections.Add(new(-1, -1));
                AllowedDirections.Add(new(-1, 0));
                AllowedDirections.Add(new(-1, 1));
                AllowedDirections.Add(new(0, -1));
                AllowedDirections.Add(new(0, 1));
                AllowedDirections.Add(new(1, -1));
                AllowedDirections.Add(new(1, 0));
                AllowedDirections.Add(new(1, 1));
            }
        }
    }
}
