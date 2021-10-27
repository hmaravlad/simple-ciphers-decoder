using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgoDecipher
{
	abstract class GeneticAlgoBase<SubT, SubComp> 
		where SubT: ISubstitution
		where SubComp: IEqualityComparer<SubT>, new()
	{
		protected readonly AlgoConfig config;
		protected int PopulationSize { get { return config.PopulationSize; } }
		protected int NumberOfIterations { get { return config.NumberOfIterations; } }
		protected int PairMultiplication { get { return config.PairMultiplication; } }
		protected int TrioMultiplication { get { return config.TrioMultiplication; } }
		protected string Alphabet { get { return config.Alphabet; } }
		
		protected List<SubT> Substitutions = new List<SubT>();

		protected FrequencyAnalysis frequencyAnaysis;

		public GeneticAlgoBase(AlgoConfig config, FrequencyAnalysis analysis)
		{
			config = config;
			frequencyAnaysis = analysis;
		}

		public void RunAlgorythm()
		{
			int i = 0;
			CreatePopulation();
			do
			{
				Breeding();
				Mutation();
				CalculateFit();
				Selection();
				i++;

				if (i % 50 == 0)
				{
					Console.WriteLine(i);
					Console.WriteLine($"Change: {Substitutions.First().PairChangeResult}\n ");
				}				
			} while (i < NumberOfIterations);


		}

		protected abstract void CreatePopulation();
		protected abstract void Breeding();
		protected abstract void Mutation();
		protected virtual void Selection()
		{
			Substitutions = Substitutions
				.Distinct(new SubComp())
				.OrderByDescending(x => x.PairChangeResult)
				.Take(PopulationSize).ToList();
		}
		protected void CalculateFit()
		{
			List<Task> tasks = new List<Task>();

			foreach (var item in Substitutions)
			{
				var newTask = new Task(() =>
				{
					string text = Decypher(item);// SubstitutionCypher.Decypher(TextToDecypher, item.ToDictionaries());
					var result = frequencyAnaysis.AnalyzePairs(text, PairMultiplication, TrioMultiplication);
					item.PairChangeResult = result;
				});
				newTask.Start();
				tasks.Add(newTask);
			}
			Task.WaitAll(tasks.ToArray());
		}

		protected abstract string Decypher(SubT item);
		

	}
}
