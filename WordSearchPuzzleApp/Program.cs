using System.Linq;
using WordSearchPuzzleSolver;

namespace WordSearchPuzzleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WordSearchRules rules = new WordSearchRules();
            rules.WordSearchType = WordSearchType.EightWay;

            string stringMatrix = "KALTJSHODA\r\n\r\nLLPUKLTOAT\r\n\r\nAKTAAKAARR\r\n\r\nSAANLAKPEA\r\n\r\nARPOVPTOKK\r\n\r\nRHOMOLICEA\r\n\r\nKOLSPEKESR\r\n\r\nORAOCAALTP\r\n\r\nSPOKVSTIAA\r\n\r\nMATKAFTKAT\r\n\r\nAIAKOSTKAY";
            string searchedWords = "ALKA HORA JUTA KAPLE KARPATY KARTA KASA KAVKA KLAS KOSMONAUT KOST KROK LAPKA MATKA OKRASA OPAT OSMA PAKT PATKA PIETA POCEL POVLAK PROHRA SEKERA SHODA SOPKA TAKT TAKTIKA TLAK VOLHA";
            WordSearchPuzzleInput input = new WordSearchPuzzleInput(stringMatrix, searchedWords);
            Solver solver = new Solver(input, rules);
            solver.Solve();
            Console.WriteLine(solver.Solution);
            string ccw = String.Join("\r\n", solver.FoundWords.Select(x => x.Word));
            Console.WriteLine(ccw);
            int a = 0;
        }
    }
}
