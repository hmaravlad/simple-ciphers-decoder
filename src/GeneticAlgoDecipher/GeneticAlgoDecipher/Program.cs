using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
				Console.WriteLine("===============");
				Console.WriteLine("Change: " + item.PairChangeResult);
				Console.WriteLine(result);
				Console.WriteLine("\n\n================\n\n");
			}
		}
	}
}
