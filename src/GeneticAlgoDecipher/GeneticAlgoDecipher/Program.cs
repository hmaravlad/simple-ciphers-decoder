using System;

namespace GeneticAlgoDecipher
{
	class Program
	{
		static void Main(string[] args)
		{
			GeneticAlgo algo = new GeneticAlgo();
			SubstitutionCypher cypher = new SubstitutionCypher();


			algo.RunAlgorythm();
			foreach(var item in algo.Substitutions)
			{
				string result = cypher.Decypher(algo.TextToDecypher, item.ToDictionary());
				Console.WriteLine(result);
				Console.WriteLine("\n\n================\n\n");
			}
		}
	}
}
