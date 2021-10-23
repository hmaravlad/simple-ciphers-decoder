using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgoDecipher
{
	class SubstitutionCypher
	{
		public string Decypher(string text, Dictionary<char, char> key)
		{
			return String.Concat(text.Select(x => key.ContainsKey(x) ? key[x] : x));
		}		
	}
}
