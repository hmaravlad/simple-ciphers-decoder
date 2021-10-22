using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgoDecipher
{
	class Substitution
	{
		const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string LetterSequence { get; set; }

		public Substitution()
		{
			LetterSequence = String.Concat(Alphabet.OrderBy(x => Guid.NewGuid()));
		}

		public Dictionary<char, char> ToDictionary()
		{
			var dictionary = new Dictionary<char, char>();

			for(int i = 0; i < Alphabet.Length; i++)
			{
				dictionary.Add(LetterSequence[i], Alphabet[i]);
			}

			return dictionary;
		}
	}
}
