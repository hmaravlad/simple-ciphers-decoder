using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GeneticAlgoDecipher
{
	class Program
	{
		static void Main(string[] args)
		{
			AlgoConfig regularConfig = new AlgoConfig() 
			{ 
				KeyLength = 1,
				NumberOfIterations = 200, 
				PopulationSize = 500, 
				PairMultiplication = 8, 
				TrioMultiplication = 30 
			};

			AlgoConfig polyalphabeticConfig = new AlgoConfig()
			{
				KeyLength = 4,
				NumberOfIterations = 200,
				PopulationSize = 500,
				PairMultiplication = 8,
				TrioMultiplication = 30
			};

			FrequencyAnalysis analysis = new FrequencyAnalysis();


			/* Regular cyphers */


			SubstitutionCypher cypher = new SubstitutionCypher();
			GeneticAlgo algo = new GeneticAlgo(regularConfig, analysis, cypher);



			/* Polyalphabetic cyphers */

			/*
			PolyalphabeticCypher cypher = new PolyalphabeticCypher();
			GeneticAlgoPolyalphabetic algo = new GeneticAlgoPolyalphabetic(polyalphabeticConfig, analysis, cypher);
			*/

			algo.RunAlgorythm();

			foreach (var item in algo.Substitutions.Take(20))
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
