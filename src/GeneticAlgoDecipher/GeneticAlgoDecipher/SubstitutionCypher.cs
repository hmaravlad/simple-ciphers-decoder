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

	class PolyalphabeticCypher
	{
		public string Decypher(string text, Dictionary<char, char>[] keys)
		{
			StringBuilder result = new StringBuilder(text);
			for (int i = 0; i < text.Length; i++)
			{
				result[i] = keys[i % keys.Length][text[i]];
			}
			return result.ToString();
		}
	}
}
