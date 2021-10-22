using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgoDecipher
{
	class GeneticAlgo
	{
		FrequencyAnalysis FrequencyAnalysis = new FrequencyAnalysis();
		SubstitutionCypher SubstitutionCypher = new SubstitutionCypher()
		public void RunAlgorythm()
		{
			int i = 0;
			do
			{
				CreatePopulation();
				Breeding();
				Mutation();
				Selection();
				i++;
			} while (i < 1000);
			

		}

		void CreatePopulation()
		{

		}

		void Breeding()
		{

		}

		void Mutation()
		{

		}

		void Selection()
		{

		}
	}
}
