using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeneticAlgoDecipher
{
	class FrequencyAnalysis
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

			foreach (var item in freqs)
			{
				PairFreqs.Add(item.Letter, item.Chance);
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

		public double AnalyzePairs(string text)
		{
			double result = 0;
			//Dictionary<string, double> changes = new Dictionary<string, double>();
			foreach (var key in PairFreqs.Keys)
			{
				int index = 0;
				int quantity = 0;
				while ((index = text.IndexOf(key, index)) != -1)
				{
					quantity++;
					index++;
				}
				/*if(quantity == 0)
				{
					continue;
				}*/
				double change = Math.Abs(((double)quantity / (text.Length - 1)) - PairFreqs[key]);
				result += change;
				//changes.Add(key, change);
			}
			return result;
		}
	}
}
