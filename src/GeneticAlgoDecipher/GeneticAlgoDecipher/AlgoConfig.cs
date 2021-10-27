using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgoDecipher
{
	class AlgoConfig
	{
		public int KeyLength { get; set; }
		public int PopulationSize { get; set; }
		public int NumberOfIterations { get; set; }
		public int PairMultiplication { get; set; }
		public int TrioMultiplication { get; set; }

		public string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	}
}
