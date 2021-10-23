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
				if (i % 1 == 0)
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
			Substitutions = Substitutions.OrderBy(x => x.PairChangeResult).ToList();

			foreach(var item in Substitutions)
			{
				//Console.WriteLine(item.LetterSequence);
			}
		}

		void Breeding()
		{
			for(int i = 0; i < PopulationSize; i+=2)
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
					while (swapsSecond.ContainsKey(changedSecuence1[i]))
					{
						var lettersWithDuplicates = changedSecuence1.ToString().GroupBy(c => c).Where(x => x.Count() > 1).Select(x => x.Key).ToList();

						changedSecuence1[i] = swapsSecond[changedSecuence1[i]];
						/*if(swapsFirst.ContainsKey(changedSecuence1[i]))
						{
							changedSecuence1[i] = swapsFirst[changedSecuence1[i]];
						}*/
						//Console.WriteLine("Here " + i);
					}

					while (swapsFirst.ContainsKey(changedSecuence2[i]))
					{
						var lettersWithDuplicates = changedSecuence2.ToString().GroupBy(c => c).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
						changedSecuence2[i] = swapsFirst[changedSecuence2[i]];

						/*if (swapsSecond.ContainsKey(changedSecuence2[i]))
						{
							changedSecuence2[i] = swapsSecond[changedSecuence2[i]];
						}*/
						//Console.WriteLine("Here " + i);
					}
				}

				for (int i = crossEnd+1; i < Alphabet.Length; i++)
				{
					while (swapsSecond.ContainsKey(changedSecuence1[i]))
					{
						var lettersWithDuplicates = changedSecuence1.ToString().GroupBy(c => c).Where(x => x.Count() > 1).Select(x => x.Key).ToList();

						changedSecuence1[i] = swapsSecond[changedSecuence1[i]];
						/*if (swapsFirst.ContainsKey(changedSecuence1[i]))
						{
							changedSecuence1[i] = swapsFirst[changedSecuence1[i]];
						}*/
						//Console.WriteLine("Here " + i);
					}

					while (swapsFirst.ContainsKey(changedSecuence2[i]))
					{
						var lettersWithDuplicates = changedSecuence2.ToString().GroupBy(c => c).Where(x => x.Count() > 1).Select(x => x.Key).ToList();

						changedSecuence2[i] = swapsFirst[changedSecuence2[i]];
						/*if (swapsSecond.ContainsKey(changedSecuence2[i]))
						{
							changedSecuence2[i] = swapsSecond[changedSecuence2[i]];
						}*/
						//Console.WriteLine("Here " + i);
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
				if(random.Next(100) <= 3)
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
			var x = Substitutions
				.Distinct(new SubstitutionComparer()).ToList();
			Substitutions = Substitutions
				.Distinct(new SubstitutionComparer())
				.OrderBy(x => x.PairChangeResult)
				.Take(PopulationSize).ToList();
		}

		void CalculateFit()
		{
			List<Task> tasks = new List<Task>();

			foreach (var item in Substitutions)
			{
				var newTask = new Task(() => {
					string text = SubstitutionCypher.Decypher(TextToDecypher, item.ToDictionary());
					var result = FrequencyAnalysis.AnalyzePairs(text);
					item.PairChangeResult = result;
				});
				newTask.Start();
				tasks.Add(newTask);				
			}
			Task.WaitAll(tasks.ToArray());
		}
	}
}
