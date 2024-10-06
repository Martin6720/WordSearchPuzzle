using System.Text.RegularExpressions;
using WordSearchPuzzleSolver;

namespace WordSearchPuzzleApp
{
    internal class Program
    {
        const string BGCOLOR = "\u001b[48;5;1m";    //48 - background, 1m - red (256 color palette)
        const string RESET = "\x1B[0m";             //set format to default
        const string InputMatrixPath = "InputMatrix.txt";
        const string SearchedWordsPath = "SearchedWords.txt";
        static void Main(string[] args)
        {
            WordSearchRules rules = new WordSearchRules();
            rules.WordSearchType = WordSearchType.EightWay;

            //string stringMatrix = "KALTJSHODA\r\n\r\nLLPUKLTOAT\r\n\r\nAKTAAKAARR\r\n\r\nSAANLAKPEA\r\n\r\nARPOVPTOKK\r\n\r\nRHOMOLICEA\r\n\r\nKOLSPEKESR\r\n\r\nORAOCAALTP\r\n\r\nSPOKVSTIAA\r\n\r\nMATKAFTKAT\r\n\r\nAIAKOSTKAY";
            //string searchedWords = "ALKA HORA JUTA KAPLE KARPATY KARTA KASA KAVKA KLAS KOSMONAUT KOST KROK LAPKA MATKA OKRASA OPAT OSMA PAKT PATKA PIETA POCEL POVLAK PROHRA SEKERA SHODA SOPKA TAKT TAKTIKA TLAK VOLHA";

            string stringMatrix = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + InputMatrixPath);
            string searchedWords = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + SearchedWordsPath);

            WordSearchPuzzleInput input = new WordSearchPuzzleInput(stringMatrix, searchedWords);
            Solver solver = new Solver(input, rules);
            solver.Solve();
            solver.Solve();
            solver.Solve();

            Console.WriteLine(BuildPrintOutput(solver));
            Console.ReadLine();
        }
        private static string BuildPrintOutput(Solver solver)
        {
            string output = string.Empty;
            string outputMatrix = string.Empty;
            for (int i = 0; i < solver.Matrix.GetLength(0); i++)
            {
                for (int n = 0; n < solver.Matrix.GetLength(1); n++)
                {
                    outputMatrix += (!solver.Matrix[i, n].Item2 ? BGCOLOR : "") + solver.Matrix[i, n].Item1 + RESET;
                }
                outputMatrix += Environment.NewLine;
            }
            output += $"Tajenka: {solver.Solution + Environment.NewLine + Environment.NewLine}";
            output += $"Osmisměrka:{Environment.NewLine + Environment.NewLine}";
            output += outputMatrix + Environment.NewLine + Environment.NewLine;
            List<string> foundWords = solver.FoundWords.DistinctBy(x => x.Word).Select(x => x.Word).ToList();
            output += $"Nalezená slova:{Environment.NewLine}";
            foreach (string found in foundWords)
                output += found + Environment.NewLine;
            output += Environment.NewLine;
            output += $"Nenalezená slova:{Environment.NewLine}";
            foreach (string notFound in solver.SearchedWords.Where(x => !foundWords.Contains(x)))
                output += notFound + Environment.NewLine;
            return output;
        }
    }
}
