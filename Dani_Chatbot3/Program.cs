using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Dani_ChatBot3.cs
 * 
 * Author: Christopher Jerrard-Dunne
 * Start Date: 12/11/2013
 * 
 * Contains the main logic loop for procedures using DANI, including an initialization from DANI, and instructions before handing off into a loop.
 */

namespace Dani_Chatbot3 {
	class Program {
		public static chatlog Chat = new chatlog();
		public static dani DANI = new dani();
		public static int ExitFlag = 1;
		private static string MessageToParse;

		static void Main(string[] args) {
			DANI.Initialize();

			do {
				MessageToParse = "";

				Console.Write("USER: ");
				MessageToParse = Chat.RecordUserMessage();

				DANI.MessageParse(MessageToParse);
				
			} while (ExitFlag == 1);

			//Holds the screen before exiting.
			Console.Write("\n\tPress any key to exit . . .");
			Console.ReadKey();
		}
	}
}
