using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeneticAlgoDecipher
{
	interface IFrequencyAnalysis 
	{
		double AnalyzePairs(string text, int pairMultiplication, int trioMultiplication);
	}
	class FrequencyAnalysis : IFrequencyAnalysis
	{
		public class AnalysisResult
		{
			public double Change { get; set; }

			public Dictionary<char, double> LetterFrequencies { get; set; }

			public AnalysisResult()
			{
				Change = 0;
				LetterFrequencies = new Dictionary<char, double>();

				
			}
		}
		public Dictionary<char, double> EnglishFrequencies { get; set; }
		public Dictionary<string, double> PairFreqs = new Dictionary<string, double>();
		public Dictionary<string, double> TrippleFreqs = new Dictionary<string, double>();

		private string _alphabet;

		public FrequencyAnalysis()
		{
			EnglishFrequencies = new Dictionary<char, double>()
			{
				{ 'E', 0.111607 },
				{ 'A', 0.084966 },
				{ 'R', 0.075809 },
				{ 'I', 0.075448 },
				{ 'O', 0.071635 },
				{ 'T', 0.069509 },
				{ 'N', 0.066544 },
				{ 'S', 0.057351 },
				{ 'L', 0.054893 },
				{ 'C', 0.045388 },
				{ 'U', 0.036308 },
				{ 'D', 0.033844 },
				{ 'P', 0.031671 },
				{ 'M', 0.030129 },
				{ 'H', 0.030034 },
				{ 'G', 0.024705 },
				{ 'B', 0.020720 },
				{ 'F', 0.018121 },
				{ 'Y', 0.017779 },
				{ 'W', 0.012899 },
				{ 'K', 0.011016 },
				{ 'V', 0.010074 },
				{ 'X', 0.002902 },
				{ 'Z', 0.002722 },
				{ 'J', 0.001965 },
				{ 'Q', 0.001962 }
			};

			_alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

			const string filename = "letterFrequency.json";
			string file = File.ReadAllText(filename);
			List<PairFrequency> freqs = JsonSerializer.Deserialize<List<PairFrequency>>(file);

			foreach (var item in freqs.OrderByDescending(x => x.Chance).Take(200).ToList())
			{
				PairFreqs.Add(item.Letter, item.Chance);
			}

			const string threeFilename = "letterFrequencyThree.json";
			file = File.ReadAllText(threeFilename);
			List<PairFrequency> threeFreqs = JsonSerializer.Deserialize<List<PairFrequency>>(file);

			foreach (var item in threeFreqs)
			{
				TrippleFreqs.Add(item.Letter, item.Chance);
			}

		}

		public AnalysisResult Analyze(string text)
		{
			int textLength = text.Length;
			var result = new AnalysisResult();

			foreach (char letter in _alphabet)
			{
				double frequency = text.Where(x => x == letter).Count() / (double)textLength;
				result.LetterFrequencies.Add(letter, frequency);

				double change = Math.Abs(EnglishFrequencies[letter] - frequency);
				result.Change += change;
			}

			return result;
		}

		public double AnalyzePairs(string text, int pairMultiplication, int trioMultiplication)
		{
			double result = 0;
			foreach (var letter in _alphabet)
			{
				double score = text.Where(x => x == letter).Count() * EnglishFrequencies[letter];
				result += score;
				//double change = Math.Abs((text.Where(x => x == letter).Count()/text.Length) - EnglishFrequencies[letter]);
				//result += change;
			}
			//Dictionary<string, double> changes = new Dictionary<string, double>();
			/*
			foreach (var key in PairFreqs.Keys)
			{
				int index = 0;
				int quantity = 0;
				while ((index = text.IndexOf(key, index)) != -1)
				{
					quantity++;
					index++;
				}

				result += PairFreqs[key] * quantity * 20;
				//if(quantity == 0)
				//{
				//	continue;
				//}
				//double change = Math.Abs(((double)quantity / (text.Length - 1)) - PairFreqs[key]);
				//result += change;
				//changes.Add(key, change);


			}

			foreach (var key in TrippleFreqs.Keys)
			{
				int index = 0;
				int quantity = 0;
				while ((index = text.IndexOf(key, index)) != -1)
				{
					quantity++;
					index++;
				}

				result += TrippleFreqs[key] * quantity * 50;
				//if(quantity == 0)
				//{
				//	continue;
				//}
				//double change = Math.Abs(((double)quantity / (text.Length - 1)) - PairFreqs[key]);
				//result += change;
				//changes.Add(key, change);


			}
			*/
			
			Dictionary<string, int> appearancesPairs = new Dictionary<string, int>();
			for(int i = 0; i < text.Length-1; i++)
			{
				string current = String.Concat(text[i],text[i + 1]);
				if(!appearancesPairs.ContainsKey(current))
				{
					appearancesPairs.Add(current, 0);
				}
				appearancesPairs[current] += 1;
			}

			Dictionary<string, int> appearancesTrios = new Dictionary<string, int>();
			for (int i = 0; i < text.Length - 2; i++)
			{
				string current = String.Concat(text[i], text[i + 1], text[i + 2]);
				if (!appearancesTrios.ContainsKey(current))
				{
					appearancesTrios.Add(current, 0);
				}
				appearancesTrios[current] += 1;
			}

			foreach (var key in PairFreqs.Keys)
			{
				if(appearancesPairs.ContainsKey(key))
					result += PairFreqs[key] * appearancesPairs[key] * pairMultiplication; // 8-9
			}

			foreach (var key in TrippleFreqs.Keys)
			{
				if (appearancesTrios.ContainsKey(key))
					result += TrippleFreqs[key] * appearancesTrios[key] * trioMultiplication; // 30
			}
			
			return result;

		}
	}
}
