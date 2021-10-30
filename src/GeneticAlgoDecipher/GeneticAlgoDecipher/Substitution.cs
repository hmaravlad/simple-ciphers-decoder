using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeneticAlgoDecipher.FrequencyAnalysis;

namespace GeneticAlgoDecipher
{
	interface ISubstitution
	{
		double PairChangeResult { get; set; }
	}
	class Substitution : ISubstitution
	{
		const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public string LetterSequence { get; set; }

		public AnalysisResult AnalysisResult { get; set; }
		public double PairChangeResult { get; set; }

		public int Wrong { get; set; }

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

		public static Substitution FromDictionary(Dictionary<char,	char> cypher)
		{
			StringBuilder stringBuilder = new StringBuilder("");
			for (int i = 0; i < Alphabet.Length; i++)
			{
				stringBuilder.Append(cypher[Alphabet[i]]);
			}
			return new Substitution(stringBuilder.ToString());
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


	class PolyalphabeticSubstitution: ISubstitution
	{
		const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public string[] LetterSequences { get; set; }

		public AnalysisResult AnalysisResult { get; set; }
		public double PairChangeResult { get; set; }

		public int Wrong { get; set; }
		public int NumberOfAlphabets { get; set; }

		public PolyalphabeticSubstitution(int numberOfAlphabets) 
		{
			LetterSequences = new string[numberOfAlphabets];
			NumberOfAlphabets = numberOfAlphabets;

			for (int i = 0; i < numberOfAlphabets; i++)
			{
				LetterSequences[i] = String.Concat(Alphabet.OrderBy(x => Guid.NewGuid()));

			}
		}

		public PolyalphabeticSubstitution(string[] letterSequences)
		{
			NumberOfAlphabets = letterSequences.Length;
			LetterSequences = letterSequences;
		}

		
		public Dictionary<char, char>[] ToDictionary()
		{
			var dictionaries = new Dictionary<char, char>[NumberOfAlphabets];

			for(int alph = 0; alph < NumberOfAlphabets; alph++)
			{
				dictionaries[alph] = new Dictionary<char, char>();
				for (int i = 0; i < Alphabet.Length; i++)
				{
					dictionaries[alph].Add(LetterSequences[alph][i], Alphabet[i]);
				}
			}

			return dictionaries;
		}
		
		public static PolyalphabeticSubstitution FromDictionary(Dictionary<char, char>[] cypher)
		{
			StringBuilder stringBuilder = new StringBuilder("");
			string[] alphabets = new string[cypher.Length];

			for (int alph = 0; alph < cypher.Length; alph++)
			{
				for (int i = 0; i < Alphabet.Length; i++)
				{
					stringBuilder.Append(cypher[Alphabet[i]]);
				}
				alphabets[alph] = stringBuilder.ToString();
				stringBuilder.Clear();
			}
			return new PolyalphabeticSubstitution(alphabets);
		}
	}

	class PolyalphabeticComparer : IEqualityComparer<PolyalphabeticSubstitution>
	{
		public bool Equals(PolyalphabeticSubstitution x, PolyalphabeticSubstitution y)
		{
			if (x.NumberOfAlphabets != y.NumberOfAlphabets)
				return false;

			for(int i = 0; i < x.NumberOfAlphabets; i++)
			{
				if (x.LetterSequences[i] != y.LetterSequences[i])
					return false;
			}
			return true;
		}

		public int GetHashCode([DisallowNull] PolyalphabeticSubstitution obj)
		{
			int hash = 0;

			for (int i = 0; i < obj.NumberOfAlphabets; i++)
			{
				hash += obj.LetterSequences[i].GetHashCode();
			}
			return hash;
		}
	}
}
