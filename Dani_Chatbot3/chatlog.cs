using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*chatlog.cs
 * 
 * Records user messages and handles the ability to create a chatlog file of your conversation with DANI.
 * 
 */

namespace Dani_Chatbot3 {
	class chatlog {

		public List<string> Log;

		public string message;

		private string FileName;

		private ConsoleKeyInfo FileExistsOverwrite;

		private Boolean ErrorChk;


		//Constructor, initializes everything to default values.
		public chatlog() {
			Log = new List<string>();
			message = "";
			ErrorChk = false;
			FileName = "";
		}


		//Records the users message, erasing the line beforehand and adding the users message to the chat log.
		public string RecordUserMessage() {
			//Defaults the message string to be empty from last time.
			message = "";

			//Reads in a line from the user
			message = Console.ReadLine();

			//Checks the input from the user to see if certain commands were accessed, such as a clear screen, a save method and an exit method.
			//When these methods are called, a flag is passed to DANI, telling it not to react to this text input.
			//If none of these are accessed, it simply adds the message to the log, before returning the message for DANI.

			if (message == "") {
				Program.DANI.ResponseFlag = false;
			}
			else if (message != "" && message[0] == '*') {

				Program.DANI.ResponseFlag = false;

				if (String.Compare(message.ToUpper(), "*SAVE") == 0) {

					//Hands off to the CreateLogFile() method, saving your conversation.
					CreateLogFile();

				}
				else if (String.Compare(message.ToUpper(), "*HELP") == 0) {

					//Displays a help menu of commands that you can use
					Console.WriteLine("\n\tLIST OF COMMANDS: \n\n\t*HELP: You are here. Lists all available commands.");
					Console.WriteLine("\t*SAVE: Saves a chat log between you and DANI.");
					Console.WriteLine("\t*EVENTS: Turns chat events on and off, so you can talk freely.");
					Console.WriteLine("\t*CLEAR: Clears the screen of previous messages.");
					Console.WriteLine("\t*EXIT: Exits the program, saying goodbye to DANI.\n");

				}
				else if ((String.Compare(message.ToUpper(), "*EXIT") == 0) || (String.Compare(message.ToUpper(), "*QUIT")) == 0) {

					//Sets an exit flag in the main program to break the main program loop.
					Program.ExitFlag = 0;
					Program.DANI.ResponseFlag = false;
					return (message);

				}
				else if (String.Compare(message.ToUpper(), "*CLEAR") == 0) {

					//Clears the screens log of messages to make things more readable. Doesn't affect the save log in any way.
					Console.Clear();

				}
				else if (String.Compare(message.ToUpper(), "*EVENTS") == 0) {

					Program.DANI.ResponseFlag = false;

					if (Program.DANI.EventFlag == true) {
						Program.DANI.EventFlag = false;
						Console.WriteLine("\n\tEvents are now turned off.\n");
					}
					else if (Program.DANI.EventFlag == false) {
						Program.DANI.EventFlag = true;
						Console.WriteLine("\n\tEvents are now turned on.\n");
					}

				}
				else {

					//Tells the user the backslash command was invalid
					Console.WriteLine("\n\tERROR: Invalid Option Command.\n");

				}

			}
			else {

				Program.DANI.ResponseFlag = true;
				Log.Add("USER: " + message);

			}

			return (message);
		}




		//Creates a chat log when the user types the word "SAVE"
		private void CreateLogFile(){
			FileName = "";
			
			//Asks for a file name to save it to, and appends ".txt" so it is saved as a text file
			Console.Write("\n\tChoose a file name: ");

			FileName = Console.ReadLine();
			FileName = FileName + ".txt";

			//If the file exists, it asks you if you want to overwrite it. 
			if (File.Exists(FileName) == true) {
				Console.Write("\n\tThis file already exists. Would you like to overwrite it? (Y/N): ");

				//Enters a loop to check correct input, and do the correct actions if the user responds properly.
				do {
					ErrorChk = false;
					FileExistsOverwrite = Console.ReadKey();
					Console.WriteLine();

					//If the user typed 'y', this opens the file and writes the chat log to the file, line by line, before closing it.
					if (FileExistsOverwrite.Key.ToString().ToUpper() == "Y") {
						try {
							FileStream Save = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

							using (StreamWriter LogWriter = new StreamWriter(Save)) {
								foreach (string line in Log) {
									LogWriter.WriteLine(line);
								}
							}

							Save.Close();
							Console.WriteLine("\n\n\tFile Saved\n");
						}
						catch {
							Console.WriteLine("\n\n\tERROR: File could not be read!\n");
						}


					}
						//If the user typed 'n', it returns from the method.
					else if (FileExistsOverwrite.Key.ToString().ToUpper() == "N") {
						Console.WriteLine();
						return;
					}
						//Otherwise it asks the user to try again, in a loop.
					else {
						ErrorChk = true;
						Console.Write("\n\tERROR: Incorrect input. Try again (Y/N): ");
					}
				} while (ErrorChk == true);
			}
				//If the file doesn't exist already, it simply creates a new one and writes the chat log into the file, before closing it.
			else {

				try {
					FileStream Save = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);

					using (StreamWriter LogWriter = new StreamWriter(Save)) {
						foreach (string line in Log) {
							LogWriter.WriteLine(line);
						}
					}

					Save.Close();

					Console.WriteLine("\n\n\tFile Saved\n");
				}
				catch {
					Console.WriteLine("\n\n\tERROR: File could not be read!\n");
				}

			}
		}

	}
}
