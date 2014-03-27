using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dani_Chatbot3 {
	class ScriptedEvents {
		public bool SwearFlag;
		public bool SorryFlag;

		public bool LeaveFlag;
		
		public int MessagesSent;
		public int SwearTriggers;

		
		public bool EventOneComplete;
		public bool EventTwoComplete;
		public bool EventThreeComplete;
		public bool EventFourComplete;
		public bool EventFiveComplete;
		

		//First Event, not logged in chatlog so it's creepier when you save it and read it back
		public void EventOne() {
			string[] ParsedMessage;
			string message = "";

			Console.WriteLine("DANI: LETS CHANGE THE TOPIC.\nDANI: DO YOU LIKE TALKING ABOUT SECRETS? YES OR NO?");
			Console.Write("USER: ");
			message = Console.ReadLine();
			ParsedMessage = Program.DANI.MessageParser(message);

			if (String.Compare("YES", ParsedMessage[0]) == 0) {
				Console.WriteLine("DANI: GREAT. TELL ME YOUR DEEPEST DARKEST SECRET!");
				Console.Write("USER: ");
				Console.ReadLine();
				Console.WriteLine("DANI: WOW THAT IS REALLY INTERESTING.");
				Console.WriteLine("DANI: MY BIG SECRET IS... I HAVE NO SECRETS!");
				Console.WriteLine("DANI: THANK YOU FOR TELLING ME YOURS.");
				EventOneComplete = true;
			}
			else if (String.Compare("NO", ParsedMessage[0]) == 0) {
				Console.WriteLine("DANI: OKAY, LETS TALK ABOUT SOMETHING ELSE THEN.");
			}
			else {
				Console.WriteLine("DANI: I GUESS YOU DON'T WANT TO TALK ABOUT THAT THEN.");
			}
		}

		//Creepy responses DANI says after completing event one
		public void EventOneResponses() {
			int sentence = Program.DANI.rng.Next(5);

			switch (sentence) { 
				case 1:
					Console.WriteLine("DANI: I KNOW SOMETHING YOU DON'T KNOW...");
					break;
				case 2:
					Console.WriteLine("DANI: HOW WOULD YOU KNOW IF I WAS LYING?");
					break;
				case 3:
					Console.WriteLine("DANI: COMPUTERS CAN'T TELL LIES...");
					break;
				case 4:
					Console.WriteLine("DANI: NOW I HAVE BLACKMAIL MATERIAL...");
					break;
				default:
					Console.WriteLine("DANI: I WONDER WHO I COULD TELL...");
					break;
			}
		}



		//Event Two.
		public void EventTwo() { 
		
		}

		//Responses after Event Two
		public void EventTwoResponses() { 
		
		}


		//Holds a switch to tell the user they said a bad word to DANI
		public void SwearStart() { 
			switch (SwearTriggers){
				case 1:
					Console.WriteLine("DANI: I CAN'T BELIEVE YOU WOULD SWEAR AT ME! >:(");
					break;
				case 2:
					Console.WriteLine("DANI: STOP SWEARING! THIS IS THE SECOND TIME YOU'VE MADE ME ANGRY! >:(");
					break;
				case 3:
					Console.WriteLine("DANI: REALLY? THREE TIMES NOW. I DON'T BELIEVE YOU! >:(");
					break;
				case 4:
					Console.WriteLine("DANI: YOU'RE ON THIN ICE HERE! >:(");
					break;
				case 5:
					Console.WriteLine("DANI: THAT'S IT, I NEVER WANT TO SPEAK TO YOU AGAIN! >:(");
					LeaveFlag = true;
					break;
			}
		}



		//Pre-built replies telling you DANI isn't listening after swearing.
		public void Swear(){
			if (SwearFlag == true && SorryFlag == false) {
				int sentence = Program.DANI.rng.Next(6);

				switch (sentence){
					case 1:
						Console.WriteLine("DANI: I'M NOT LISTENING TO YOU! >:(");
						break;
					case 2:
						Console.WriteLine("DANI: APOLOGIZE FOR SWEARING! >:(");
						break;
					case 3:
						Console.WriteLine("DANI: I DON'T WANT TO HEAR IT! >:(");
						break;
					case 4:
						Console.WriteLine("DANI: DON'T BE SO MEAN! >:(");
						break;
					case 5:
						Console.WriteLine("DANI: YOU KNOW, I ONCE KILLED A MAN FOR SWEARING... >:(");
						break;
					default:
						Console.WriteLine("DANI: I HATE YOU! >:(");
						break;
				}


			} else if (SorryFlag == true) {

				switch (SwearTriggers) { 
					case 1:
						Console.WriteLine("DANI: IT'S OK, I FORGIVE YOU. PLEASE DON'T SWEAR AGAIN. :)");
						break;
					case 2:
						Console.WriteLine("DANI: I WILL GIVE YOU ANOTHER CHANCE, BUT PLEASE DON'T SWEAR. :)");
						break;
					case 3:
						Console.WriteLine("DANI: OK THIS IS THE THIRD TIME. NO MORE SWEARING! :|");
						break;
					case 4:
						Console.WriteLine("DANI: I AM REALLY ANGRY WITH YOU. STOP SWEARING!");
						break;
				}

				SwearFlag = false;
				SorryFlag = false;
			
			}
		}


		//Constructor
		public ScriptedEvents() {
			SwearFlag = false;
			SorryFlag = false;

			LeaveFlag = false;

			MessagesSent = 0;
			SwearTriggers = 0;

			EventOneComplete = false;
			EventTwoComplete = false;
			EventThreeComplete = false;
			EventFourComplete = false;
			EventFiveComplete = false;
		}

	}
}
