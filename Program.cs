using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gorkana
{
    class Program
    {
        const string errGeneric = "ERROR";
        const string errFileNotExists = "FILENOTEXISTS";
        const string exit = "Q";

        /// <summary>
        /// Reads the name of file from the user. If the user insert the exit key, the programm will close.
        /// </summary>
        /// <returns>Name of file or an error</returns>
        static string AskFile()
        {
            string rtn = "";

            Console.WriteLine("Insert the name of file (you can use with path) [Q for exit]");
            string filename = Console.ReadLine();
            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("File name is required!");
                return errGeneric;
            }

            // verifies if the name of file is equals at exit string
            if (filename == exit)
            {
                return exit;
            }

            // if the file exists, it returns the name of filename
            // in other case the error
            if (File.Exists(filename))
            {
                rtn = filename;
            }
            else
            {
                Console.WriteLine("File doesn't exists!");
                rtn = errFileNotExists;
            }

            return rtn;
        }

        /// <summary>
        /// Calculates commands in the file
        /// </summary>
        /// <param name="cmd">List of OperationCommand from the file</param>
        /// <returns>Calcolate value</returns>
        static decimal Calculate(List<OperationCommand> cmd)
        {
            decimal rtn = 0;

            string lastCommand = "";
            foreach (OperationCommand c in cmd)
            {
                if (string.IsNullOrEmpty(lastCommand))
                {
                    rtn = c.Number;
                }
                else
                {
                    switch (lastCommand.ToLower())
                    {
                        case "add":
                            rtn += c.Number;
                            break;
                        case "divide":
                            rtn /= c.Number;
                            break;
                        case "multiply":
                            rtn *= c.Number;
                            break;
                        case "subtract":
                            rtn -= c.Number;
                            break;
                        case "apply":
                            break;
                    }
                }
                lastCommand = c.Command;
            }

            return rtn;
        }

        // the application starts here
        static void Main(string[] args)
        {
            Console.WriteLine("GORTANA Test");
            Console.WriteLine();

            string filepath = "";
            while (true)
            {
                filepath = AskFile();
                if (filepath == exit)
                {
                    Console.WriteLine();
                    Console.WriteLine("Thank you!");
                    break;
                }
                if (filepath != errFileNotExists)
                {
                    List<OperationCommand> cmd = ReadFile(filepath);
                    if (cmd.Count() == 0)
                    {
                        Console.WriteLine("No items in the file!");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Result -> " + Calculate(cmd));
                        Console.WriteLine();
                    }
                }
            }
        }

        /// <summary>
        /// Reards the file ans returns the list of OperationCommand in the file
        /// </summary>
        /// <param name="filename">Filename to read</param>
        /// <returns>List of OperationCommand</returns>
        static List<OperationCommand> ReadFile(string filename)
        {
            List<OperationCommand> cmd = new List<OperationCommand>();

            // Read each line of the file into a string array. Each element 
            // of the array is one line of the file. 
            string[] lines = System.IO.File.ReadAllLines(filename);

            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Contents of " + filename + " = ");
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Console.WriteLine("\t" + line);

                if (!string.IsNullOrEmpty(line))
                {
                    string[] c = line.Split(' ');
                    cmd.Add(new OperationCommand { Command = c[0].Trim(), Number = Convert.ToDecimal('0' + c[1].Trim()) });
                }
            }

            return cmd;
        }
    }

    /// <summary>
    /// Class for saving the couple number and command
    /// </summary>
    class OperationCommand
    {
        public string Command { get; set; }
        public decimal Number { get; set; }
    }
}