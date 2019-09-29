using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        // The main entry point for the program
        // Arguments can be accepted when executing through the command line
        // The User's name can be entered as the first argument
        static void Main(string[] args)
        {
            // Declare the string variable UserName as null/empty
            string UserName = "";

            // The following code block will execute until a user name is entered
            while (UserName == "")
            {
                // Checks if the user entered an argument when executing through command line
                if (args.Length == 0)
                {
                    try
                    {
                        System.Console.Write("Please enter your name: "); // Provide information about what the user entry should be
                        string UserInput = Console.ReadLine(); // Capture user input
                        if (UserInput == "")
                        {
                            throw new Exception("Error: No argument entered."); // Throw an exception if the entry is still blank
                        }
                        else
                        {
                            UserName = UserInput; // Assign the input to the UserName string variable
                        }
                    }
                    catch (Exception e) // Catch the exception if thrown in the loop above
                    {
                        System.Console.WriteLine(e.Message); // Output the exception message to the console
                    }

                }
                else
                {
                    UserName = args[0]; // Assign the first argument to the UserName string variable
                }
            }
            
            System.Console.WriteLine("Hello, " + UserName + "!"); // Write a message to the user including the user's name

            System.Console.WriteLine("Press ENTER to exit the program."); // This was added to prevent the application from closing before you have an opportunity to view the program output 
            string NewInput = Console.ReadLine();
        }
        
    }
}
