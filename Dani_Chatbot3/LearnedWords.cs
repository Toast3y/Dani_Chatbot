using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dani_Chatbot3 {
	class LearnedWords {

		string Word;

		Dictionary <int, string> AssociatedWord;



		public LearnedWords() {
			AssociatedWord = new Dictionary<int, string>();
			Word = "";
		}

	}
}
