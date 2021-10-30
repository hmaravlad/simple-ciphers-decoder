using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgoDecipher
{
	class GeneticAlgoPolyalphabetic : GeneticAlgoBase<PolyalphabeticSubstitution, PolyalphabeticComparer>
	{
		int KeyLength { get { return config.KeyLength; } }

		PolyalphabeticCypher SubstitutionCypher = new PolyalphabeticCypher();

		public string TextToDecypher = "UMUPLYRXOYRCKTYYPDYZTOUYDZHYJYUNTOMYTOLTKAOHOKZCMKAVZDYBRORPTHQLSERUOERMKZGQJOIDJUDNDZATUVOTTLMQBOWNMERQTDTUFKZCMTAZMEOJJJOXMERKJHACMTAZATIZOEPPJKIJJNOCFEPLFBUNQHHPPKYYKQAZKTOTIKZNXPGQZQAZKTOTIZYNIUISZIAELMKSJOYUYYTHNEIEOESULOXLUEYGBEUGJLHAJTGGOEOSMJHNFJALFBOHOKAGPTIHKNMKTOUUUMUQUDATUEIRBKYUQTWKJKZNLDRZBLTJJJIDJYSULJARKHKUKBISBLTOJRATIOITHYULFBITOVHRZIAXFDRNIORLZEYUUJGEBEYLNMYCZDITKUXSJEJCFEUGJJOTQEZNORPNUDPNQIAYPEDYPDYTJAIGJYUZBLTJJYYNTMSEJYFNKHOTJARNLHHRXDUPZIALZEDUYAOSBBITKKYLXKZNQEYKKZTOKHWCOLKURTXSKKAGZEPLSYHTMKRKJIIQZDTNHDYXMEIRMROGJYUMHMDNZIOTQEKURTXSKKAGZEPLSYHTMKRKJIIQZDTNROAUYLOTIMDQJYQXZDPUMYMYPYRQNYFNUYUJJEBEOMDNIYUOHYYYJHAOQDRKKZRRJEPCFNRKJUHSJOIRQYDZBKZURKDNNEOYBTKYPEJCMKOAJORKTKJLFIOQHYPNBTAVZEUOBTKKBOWSBKOSKZUOZIHQSLIJJMSURHYZJJZUKOAYKNIYKKZNHMITBTRKBOPNUYPNTTPOKKZNKKZNLKZCFNYTKKQNUYGQJKZNXYDNJYYMEZRJJJOXMERKJVOSJIOSIQAGTZYNZIOYSMOHQDTHMEDWJKIULNOTBCALFBJNTOGSJKZNEEYYKUIXLEUNLNHNMYUOMWHHOOQNUYGQJKZLZJZLOLATSEHQKTAYPYRZJYDNQDTHBTKYKYFGJRRUFEWNTHAXFAHHODUPZMXUMKXUFEOTIMUNQIHGPAACFKATIKIZBTOTIKZNKKZNLORUKMLLFBUUQKZNLEOHIEOHEDRHXOTLMIRKLEAHUYXCZYTGUYXCZYTIUYXCZYTCVJOEBKOHE";
		//public string TextToDecypher = "EFF PQL EKVTVPC PY FLMVHQLU EWC NVWF YG HYTCETHQEKLPVM SAKSPVPAPVYW MVHQLUS PQL YWL ASLF VW PQL MVHQLUPLRPS QLUL QES POLWPC SVR VWFLHLWFLWP UEWFYBTC MQYSLW BYWYETHQEKLPVM SAKSPVPAPVYW HEPPLUWS GYU LEMQ TLPPLU GUYB LWDTVSQ ETHQEKLP VP VS MTLEU PQEP CYA MEW WY TYWDLU ULTC YW PQL EBL SVBHTL UYAPVWL YG DALSSVWD PQL NLC KC LRQEASPVXL LEUMQ OQVMQ CYA HUYKEKTC ASLF PY FLMVHQLU PQL HULXVYAS HEUEDUEHQ OVTT PQL VWFLR YG MYVWMVFLWML SPVTT OYUN ES E SADDLSPVYW CYA MEW PUC PY FVXVFL PQL BLSSEDL VW HEUPS KC PQL WABKLU YG MQEUEMPLUS VW E NLC EWF EHHTC GULJALWMC EWETCSVS PY LEMQ YG PQL B MEW CYA GVWF EOEC PY ASL QVDQLU YUFLU GULJALWMC SPEPVSPVMS OVPQ PQVS PCHL YG MVHQLU PQL WLRP BEDVMET OYUF OVTT PENL PY PQL WLRP TEK LWZYCKVPTCSTESQPOY MEHVPET C MEHVPET Z MEHVPET KT MEHVPET C MEHVPET T";

		public GeneticAlgoPolyalphabetic(AlgoConfig config, FrequencyAnalysis analysis, PolyalphabeticCypher substitutionCypher): base(config, analysis)
		{
			SubstitutionCypher = substitutionCypher;
		}	

		protected override void CreatePopulation()
		{
			for (int i = 0; i < PopulationSize; i++)
			{
				Substitutions.Add(new PolyalphabeticSubstitution(KeyLength));
			}			
		}

		protected override void Breeding()
		{
			for (int i = 0; i < PopulationSize; i += 2)
			{
				Crossover(Substitutions[i], Substitutions[i + 1]);				
			}

			void Crossover(PolyalphabeticSubstitution first, PolyalphabeticSubstitution second)
			{
				Random random = new Random();

				int alphabetNo = random.Next(KeyLength);

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

				StringBuilder changedSecuence1 = new StringBuilder(first.LetterSequences[alphabetNo]);
				StringBuilder changedSecuence2 = new StringBuilder(second.LetterSequences[alphabetNo]);

				for (int i = crossStart; i <= crossEnd; i++)
				{
					char firstLetter = first.LetterSequences[alphabetNo][i];
					char secondLetter = second.LetterSequences[alphabetNo][i];
					swapsFirst.Add(firstLetter, secondLetter);
					swapsSecond.Add(secondLetter, firstLetter);

					changedSecuence1[i] = secondLetter;
					changedSecuence2[i] = firstLetter;
				}

				for (int i = 0; i < crossStart; i++)
				{
					while (swapsSecond.ContainsKey(changedSecuence1[i]))
					{
						changedSecuence1[i] = swapsSecond[changedSecuence1[i]];
					}

					while (swapsFirst.ContainsKey(changedSecuence2[i]))
					{
						changedSecuence2[i] = swapsFirst[changedSecuence2[i]];
					}
				}

				for (int i = crossEnd + 1; i < Alphabet.Length; i++)
				{
					while (swapsSecond.ContainsKey(changedSecuence1[i]))
					{
						changedSecuence1[i] = swapsSecond[changedSecuence1[i]];
					}

					while (swapsFirst.ContainsKey(changedSecuence2[i]))
					{
						changedSecuence2[i] = swapsFirst[changedSecuence2[i]];
					}
				}

				string[] sub1 = new string[KeyLength];
				string[] sub2 = new string[KeyLength];

				for (int i = 0; i < KeyLength; i++)
				{
					if(alphabetNo == i)
					{
						sub1[i] = changedSecuence1.ToString();
						sub2[i] = changedSecuence2.ToString();
					}
					else
					{
						sub1[i] = first.LetterSequences[i];
						sub2[i] = second.LetterSequences[i];
					}					
				}

				Substitutions.Add(new PolyalphabeticSubstitution(sub1));
				Substitutions.Add(new PolyalphabeticSubstitution(sub2));
			}
		}

		protected override void Mutation()
		{
			var random = new Random();

			foreach (var item in Substitutions)
			{
				if (random.Next(100) <= 50)
				{
					Mutate(item);
				}
			}

			void Mutate(PolyalphabeticSubstitution substitution)
			{
				int alphabetNo = random.Next(KeyLength);
				StringBuilder stringBuilder = new StringBuilder(substitution.LetterSequences[alphabetNo]);
				int first = random.Next(stringBuilder.Length);
				int second = random.Next(stringBuilder.Length);

				char buff = stringBuilder[first];
				stringBuilder[first] = stringBuilder[second];
				stringBuilder[second] = buff;

				substitution.LetterSequences[alphabetNo] = stringBuilder.ToString();
			}
		}

		protected override string Decypher(PolyalphabeticSubstitution item)
		{
			return SubstitutionCypher.Decypher(TextToDecypher, item.ToDictionary()); 
		}
	}
}
