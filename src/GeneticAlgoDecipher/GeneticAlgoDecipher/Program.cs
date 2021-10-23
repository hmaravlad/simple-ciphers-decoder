using System;

namespace GeneticAlgoDecipher
{
	class Program
	{
		static void Main(string[] args)
		{
			GeneticAlgo algo = new GeneticAlgo();
			SubstitutionCypher cypher = new SubstitutionCypher();
			const string filename = "letterFrequency.json";



			algo.RunAlgorythm();
			foreach(var item in algo.Substitutions)
			{
				string result = cypher.Decypher(algo.TextToDecypher, item.ToDictionary());
				Console.WriteLine("===============");
				Console.WriteLine("Change: " + item.AnalysisResult.Change);
				Console.WriteLine(result);
				Console.WriteLine("\n\n================\n\n");
			}
		}
	}
}
