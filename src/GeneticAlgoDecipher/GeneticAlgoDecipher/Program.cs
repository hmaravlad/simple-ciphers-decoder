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



			//GeneticAlgo algo = new GeneticAlgo();
			GeneticAlgoPolyalphabetic algo = new GeneticAlgoPolyalphabetic();
			//SubstitutionCypher cypher = new SubstitutionCypher();
			PolyalphabeticCypher cypher = new PolyalphabeticCypher();

			algo.RunAlgorythm();

			foreach (var item in algo.Substitutions.Take(20))
			{
				string result = cypher.Decypher(algo.TextToDecypher, item.ToDictionaries());
				Console.WriteLine("===============");
				Console.WriteLine("Change: " + item.PairChangeResult);
				Console.WriteLine(result);
				Console.WriteLine("\n\n================\n\n");
			}
			/*
			const string correct = "EKMFLGDQVINTBWYHJUSPAXORCZ";
			var first = algo.Substitutions.First();

			foreach (var item in algo.Substitutions)
			{
				int wrong = 0;

				for (int i = 0; i < correct.Length; i++)
				{
					if (item.LetterSequence[i] != correct[i])
						wrong += 1;
				}
				item.Wrong = wrong;
				
			}

			Console.WriteLine("SmallestWrong: " + algo.Substitutions.OrderBy(a => a.Wrong).First().Wrong);

			foreach (var item in algo.Substitutions.Take(20))
			{
				string result = cypher.Decypher(algo.TextToDecypher, item.ToDictionary());
				Console.WriteLine("===============");
				Console.WriteLine("Change: " + item.PairChangeResult);
				Console.WriteLine(result);
				Console.WriteLine("\n\n================\n\n");
			}
			
			Substitution sub = new Substitution(correct);
			SubstitutionCypher substitutionCypher = new SubstitutionCypher();
			string decyphered = substitutionCypher.Decypher(algo.TextToDecypher, sub.ToDictionary());
			FrequencyAnalysis frequencyAnalysis = new FrequencyAnalysis();
			var resOfCorrect = frequencyAnalysis.AnalyzePairs(decyphered);

			Console.WriteLine("Correct Score: " + resOfCorrect);
			*/
		}
	}
}
