using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgoDecipher
{
	class GeneticAlgo
	{
		const int PopulationSize = 100;
		const int NumberOfIterations= 1000;
		const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		FrequencyAnalysis FrequencyAnalysis = new FrequencyAnalysis();
		SubstitutionCypher SubstitutionCypher = new SubstitutionCypher();

		public string TextToDecypher = "EFFPQLEKVTVPCPYFLMVHQLUEWCNVWFYGHYTCETHQEKLPVMSAKSPVPAPVYWMVHQLUSPQLYWLASLFVWPQLMVHQLUPLRPSQLULQESPBLWPCSVRVWFLHLWFLWPUEWFYOTCMQYSLWOYWYETHQEKLPVMSAKSPVPAPVYWHEPPLUWSGYULEMQTLPPLUGUYOLWDTVSQETHQEKLPVPVSMTLEUPQEPCYAMEWWYTYWDLUULTCYWPQLSEOLSVOHTLUYAPVWLYGDALSSVWDPQLNLCKCLRQEASPVILSLEUMQBQVMQCYAHUYKEKTCASLFPYFLMVHQLUPQLHULIVYASHEUEDUEHQBVTTPQLVWFLRYGMYVWMVFLWMLSPVTTBYUNESESADDLSPVYWCYAMEWPUCPYFVIVFLPQLOLSSEDLVWHEUPSKCPQLWAOKLUYGMQEUEMPLUSVWENLCEWFEHHTCGULXALWMCEWETCSVSPYLEMQYGPQLOMEWCYAGVWFEBECPYASLQVDQLUYUFLUGULXALWMCSPEPVSPVMSBVPQPQVSPCHLYGMVHQLUPQLWLRPOEDVMETBYUFBVTTPENLPYPQLWLRPTEKLWZYCKVPTCSTESQPBYMEHVPETCMEHVPETZMEHVPETKTMEHVPETCMEHVPETT";
		public List<Substitution> Substitutions = new List<Substitution>();

		public void RunAlgorythm()
		{
			int i = 0;
			do
			{
				CreatePopulation();
				Breeding();
				Mutation();
				CalculateFit();
				Selection();
				i++;
				Console.WriteLine(i);
			} while (i < NumberOfIterations);
			

		}

		void CreatePopulation()
		{
			for (int i = 0; i < PopulationSize; i++)
			{
				var newSubstitution = new Substitution();
				Substitutions.Add(newSubstitution);
			}
			CalculateFit();
			Substitutions = Substitutions.OrderByDescending(x => x.AnalysisResult.Change).ToList();

			foreach(var item in Substitutions)
			{
				Console.WriteLine(item.LetterSequence);
			}
		}

		void Breeding()
		{
			for(int i = 0; i < Substitutions.Count; i+=2)
			{
				Crossover(Substitutions[i], Substitutions[i + 1]);
			}

			void Crossover(Substitution first, Substitution second)
			{
				Random random = new Random();
				int crossStart = random.Next(Alphabet.Length);
				int crossEnd = random.Next(Alphabet.Length);

				if (crossStart > crossEnd) // if order is wrong - swap
				{
					int buff = crossEnd;
					crossEnd = crossStart;
					crossStart = buff;
				}

				Dictionary<char, char> swapsFirst = new Dictionary<char, char>();
				Dictionary<char, char> swapsSecond = new Dictionary<char, char>();

				StringBuilder changedSecuence1 = new StringBuilder(first.LetterSequence);
				StringBuilder changedSecuence2 = new StringBuilder(second.LetterSequence);

				for (int i = crossStart; i <= crossEnd; i++)
				{
					char firstLetter = first.LetterSequence[i];
					char secondLetter = second.LetterSequence[i];
					swapsFirst.Add(firstLetter, secondLetter);
					swapsSecond.Add(secondLetter, firstLetter);

					changedSecuence1[i] = secondLetter;
					changedSecuence2[i] = firstLetter;
				}				

				for (int i = 0; i < crossStart; i++)
				{
					while (swapsFirst.ContainsKey(changedSecuence1[i]))
					{
						changedSecuence1[i] = swapsFirst[changedSecuence1[i]];
						Console.WriteLine("here");
					}

					while (swapsSecond.ContainsKey(changedSecuence2[i]))
					{
						changedSecuence2[i] = swapsSecond[changedSecuence2[i]];
						Console.WriteLine("here");
					}
				}

				for (int i = crossEnd+1; i < Alphabet.Length; i++)
				{
					while (swapsFirst.ContainsKey(changedSecuence1[i]))
					{
						changedSecuence1[i] = swapsFirst[changedSecuence1[i]];
					}

					while (swapsSecond.ContainsKey(changedSecuence2[i]))
					{
						changedSecuence2[i] = swapsSecond[changedSecuence2[i]];
					}
				}

				Substitutions.Add(new Substitution(changedSecuence1.ToString()));
				Substitutions.Add(new Substitution(changedSecuence2.ToString()));
			}
		}

		void Mutation()
		{
			var random = new Random();

			foreach (var item in Substitutions)
			{
				if(random.Next(100) <= 1)
				{
					Mutate(item);
				}

			}

			void Mutate(Substitution substitution)
			{
				StringBuilder stringBuilder = new StringBuilder(substitution.LetterSequence);
				int first = random.Next(stringBuilder.Length);
				int second = random.Next(stringBuilder.Length);

				char buff = stringBuilder[first];
				stringBuilder[first] = stringBuilder[second];
				stringBuilder[second] = buff;
			}
		}

		void Selection()
		{
			Substitutions = Substitutions
				.Distinct(new SubstitutionComparer())
				.OrderByDescending(x => x.AnalysisResult.Change)
				.Take(PopulationSize).ToList();
		}

		void CalculateFit()
		{
			foreach (var item in Substitutions)
			{
				string text = SubstitutionCypher.Decypher(TextToDecypher, item.ToDictionary());
				var result = FrequencyAnalysis.Analyze(text);
				item.AnalysisResult = result;
			}
		}
	}
}
