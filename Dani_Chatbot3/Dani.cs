using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace Dani_Chatbot3 {
	class dani {

		public bool ResponseFlag;
		public bool EventFlag;

		public string response;

		private string[] words;
		private int RandomChance;

		public Random rng = new Random();

		ScriptedEvents Events = new ScriptedEvents();

		

		Dictionary<string, Dictionary<string, int>> LearnedWords = new Dictionary<string, Dictionary <string, int>>();


		public dani() {
			ResponseFlag = true;
			EventFlag = true;
		}


		//Initializes DANI, loads the dictionary into memory and displays the opening screen.
		public void Initialize() {
			try {
				FileStream LoadDictionary = new FileStream("dictionary.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

				//TODO: Add File Loading

				LoadDictionary.Close();
			}
			catch {
				Console.WriteLine("\t ERROR: Cannot load or create dictionary.txt\n\n");
			}

			Console.WriteLine("\t Welcome to DANI, the interactive chat bot! \n\n");
			Console.WriteLine("\t DANI is a learning bot that will learn words from you.");
			Console.WriteLine("\t DANI also likes to talk about certain things. \n\t But DANI doesn't like mean people!");
			Console.WriteLine("\n\t DANI also has various features, type \"*HELP\" to learn more.\n\n");
			Console.WriteLine("DANI: Hello, I am DANI, what is your name?");
		}



		//Separate method to return arrays of strings from a message from user. Created to allow message parsing in Events.
		public string[] MessageParser(string message) {
			string[] ParsedMessage;

			ParsedMessage = message.ToUpper().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			return ParsedMessage;
		}



		//Parses a message for DANI outside of Events.
		public void MessageParse(string message){

			if (ResponseFlag == true && Events.LeaveFlag == false) {

				words = MessageParser(message);

				for (int i = 0; i < words.Length ; i++){

					if ((String.Compare("FUCK", words[i]) == 0) || (String.Compare("SHIT", words[i]) == 0) || (String.Compare("ASSHOLE", words[i]) == 0) || (String.Compare("CUNT", words[i]) == 0) || (String.Compare("BITCH", words[i]) == 0)) {

						//Only count triggers if the user did not swear already
						if (Events.SwearFlag == false) {
							Events.SwearFlag = true;
							Events.SwearTriggers++;
							Events.SwearStart();
						}

					}
					else if ((Events.SwearFlag == true) && (String.Compare ("SORRY", words[i]) == 0)) {
						Events.SorryFlag = true;
					}
					else if (Events.SwearFlag != true) {

						if (!LearnedWords.ContainsKey(words[i])) {
							LearnedWords.Add(words[i], new Dictionary<string, int>());
						}

						if (i + 1 != words.Length) {
							string NextWord = words[i + 1];

							try {
								LearnedWords[words[i]].Add(NextWord, 1);
							}
							catch {
								LearnedWords[words[i]][NextWord]++;
							}
						}
					}
					
				}

				if (Events.SwearFlag == true) {
					Events.Swear();
				}
				else {
					response = "";
					int ChosenWord = rng.Next(0, (words.Length));
					Respond(words[ChosenWord], 1);

					Program.Chat.Log.Add("DANI: " + response);
					Console.WriteLine("DANI: " + response);

					RandomChance = rng.Next(0, 100);


					if (EventFlag == true) {
						Events.MessagesSent++;

						//Random inserted responses if you have completed events
						if (Events.EventOneComplete == true && Events.EventTwoComplete == false && RandomChance > 94) {
							Events.EventOneResponses();
						}
						else if (Events.EventTwoComplete == true && Events.EventThreeComplete == false && RandomChance > 94) {
							//Events.EventTwoResponses();
						}
						else if (Events.EventThreeComplete == true && Events.EventFourComplete == false && RandomChance > 94) {
							//Events.EventThreeResponses();
						}
						else if (Events.EventFourComplete == true && Events.EventFiveComplete == false && RandomChance > 94) {
							//Events.EventFourResponses();
						}


						//Starts events
						if (Events.MessagesSent >= 20 && RandomChance < 6 && Events.EventOneComplete == false) {
							Events.EventOne();
						}
						else if (Events.MessagesSent >= 50 && RandomChance < 6 && Events.EventTwoComplete == false) {
							//Events.EventTwo();
						}
						else if (Events.MessagesSent >= 80 && RandomChance < 6 && Events.EventThreeComplete == false) {
							//Events.EventThree();
						}
						else if (Events.MessagesSent >= 120 && RandomChance < 6 && Events.EventFourComplete == false) {
							//Events.EventFour();
						}
						else if (Events.MessagesSent >= 150 && RandomChance < 6 && Events.EventFiveComplete == false) {
							//Events.EventFive();
						}
					}
				}
			}
			else if (Events.LeaveFlag == true && ResponseFlag == true) {

				if (Events.SwearTriggers == 5) {
					Console.WriteLine("\n\tYour bad words caused DANI to leave.\n\tDon't say so many swear words next time... :(\n");
				} 
				else {
					Console.WriteLine("\n\tDANI has left the chat, he cannot hear you.\n");
				}

			}
		}



		//Randomly selects the words used and creates a response for DANI
		public void Respond(string SeededWord, int chance) {
			response = response + SeededWord + " ";

			List<string> NextWords = new List<string>(LearnedWords[SeededWord].Keys);

			if (NextWords.Count == 0) {
				return;
			} else {
				string ChosenWord = NextWords[rng.Next(NextWords.Count)];

				if (chance >= 100) {
					response = response + ChosenWord;
					return;
				}
				else if (chance < rng.Next(0, 100)) {

					if (chance > 10) {
						Respond(ChosenWord, chance + 10);
					} else {
						Respond(ChosenWord, ++chance);
					}

				}
			}

			
		}


		//Saves the dictionary to a .txt file in memory
		public void DictionarySave() {

			FileStream SaveDictionary = new FileStream("dictionary.txt", FileMode.Create, FileAccess.Write, FileShare.None);

			//TODO: Save Dictionary on exit.

			SaveDictionary.Close();
		
		}

	}

}
