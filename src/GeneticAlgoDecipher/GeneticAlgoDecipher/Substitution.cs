using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneticAlgoDecipher.FrequencyAnalysis;

namespace GeneticAlgoDecipher
{
	class Substitution
	{
		const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public string LetterSequence { get; set; }

		public AnalysisResult AnalysisResult { get; set; }

		public Substitution()
		{
			LetterSequence = String.Concat(Alphabet.OrderBy(x => Guid.NewGuid()));
		}

		public Substitution(string letterSequence)
		{
			LetterSequence = letterSequence;
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

	class SubstitutionComparer : IEqualityComparer<Substitution>
	{
		public bool Equals(Substitution x, Substitution y)
		{
			return x.LetterSequence == y.LetterSequence;
		}

		public int GetHashCode([DisallowNull] Substitution obj)
		{
			return obj.LetterSequence.GetHashCode();
		}
	}
}
