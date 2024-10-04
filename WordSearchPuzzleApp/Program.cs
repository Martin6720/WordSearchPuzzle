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

            //string stringMatrix = "KALTJSHODA\r\n\r\nLLPUKLTOAT\r\n\r\nAKTAAKAARR\r\n\r\nSAANLAKPEA\r\n\r\nARPOVPTOKK\r\n\r\nRHOMOLICEA\r\n\r\nKOLSPEKESR\r\n\r\nORAOCAALTP\r\n\r\nSPOKVSTIAA\r\n\r\nMATKAFTKAT\r\n\r\nAIAKOSTKAY";
            //string searchedWords = "ALKA HORA JUTA KAPLE KARPATY KARTA KASA KAVKA KLAS KOSMONAUT KOST KROK LAPKA MATKA OKRASA OPAT OSMA PAKT PATKA PIETA POCEL POVLAK PROHRA SEKERA SHODA SOPKA TAKT TAKTIKA TLAK VOLHA";

            string stringMatrix = "";
            string searchedWords = "";

            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "InputMatrix.txt"))
                stringMatrix = sr.ReadToEnd();
            using(StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "SearchedWords.txt"))
                searchedWords = sr.ReadToEnd();
            WordSearchPuzzleInput input = new WordSearchPuzzleInput(stringMatrix, searchedWords);
            Solver solver = new Solver(input, rules);
            solver.Solve();

            string output = BuildPrintOutput(solver);
            Console.WriteLine(output);
            Console.ReadLine();
        }
        public static string BuildPrintOutput(Solver solver)
        {
            string output = string.Empty;
            string outputMatrix = "";
            const string BGCOLOR = "\u001b[48;5;1m";    //48 - background, 1m - red (256 color palette)
            const string RESET = "\x1B[0m";             //set format to default
            for (int i = 0; i < solver.Matrix.GetLength(0); i++)
            {
                for (int n = 0; n < solver.Matrix.GetLength(1); n++)
                {
                    outputMatrix += (!solver.Matrix[i, n].Item2 ? BGCOLOR : "") + solver.Matrix[i, n].Item1 + RESET;
                }
                outputMatrix += "\r\n";
            }
            output += "Tajenka: " + solver.Solution + "\r\n" + "\r\n";
            output += "Osmisměrka:\r\n\r\n";
            output += outputMatrix+"\r\n\r\n";
            List<string> foundWords = solver.FoundWords.DistinctBy(x => x.Word).Select(x => x.Word).ToList();
            output +="Nalezená slova:\r\n";
            foreach (string found in foundWords)
                output += found + "\r\n";
            output += "\r\n";
            output += "Nenalezená slova:\r\n";
            foreach (string notFound in solver.SearchedWords.Where(x => !foundWords.Contains(x)))
                output += notFound + "\r\n";
            return output;
        }
    }
}
