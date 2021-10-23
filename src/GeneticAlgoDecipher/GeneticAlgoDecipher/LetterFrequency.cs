using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgoDecipher
{
	class LetterFrequency: IComparable<LetterFrequency>
	{
		public char Letter { get; set; }

		public double Frequency { get; set; }

		public int CompareTo(LetterFrequency other)
		{
			if (Frequency < other.Frequency) return -1;
			if (Frequency > other.Frequency) return 1;
			return 0;
		}
	}
}
