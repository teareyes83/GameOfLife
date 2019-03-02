using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;

namespace CoreConsoleApp
{
    class MainClass
    {
        const string usageText = "Usage: ConsoleApp inputfile.txt outputfile.txt";

        public static int Main(string[] args)
        {
            StreamWriter writer = null;

            args = new string[] { "input.txt" };
            if (args.Length < 1)
            {
                Console.WriteLine(usageText);
                return 1;
            }

            try
            {
                if (args.Length > 1)
                {
                    writer = new StreamWriter(args[1]);
                    Console.SetOut(writer);
                }

                if (args.Length > 0)
                {
                    Console.SetIn(new StreamReader(args[0]));
                }
            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine(e.Message);
                errorWriter.WriteLine(usageText);
                return 1;
            }

            string firstLine = Console.ReadLine();
            var gridLength = firstLine.Split(' ').Select(x => int.Parse(x)).ToArray();
            var rowCount = gridLength[0];
            var columnCount = gridLength[1];
            var lines = new List<string>();
            for (int row = 0; row < rowCount; ++row)
            {
                var line = Console.ReadLine();
                if (line.Length != columnCount)
                {
                    throw new InvalidDataException($"line length should be {columnCount}");
                }

                lines.Add(line);
            }

            var game = new Game(lines.ToArray());

            System.Timers.Timer aTimer = new System.Timers.Timer(20000);
            aTimer.Elapsed += (s, e) =>
            {
                game.GenerateNext();
                Console.Clear();
                Console.WriteLine(game.ToString());
            };
            aTimer.Interval = 500;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            writer?.Close();

            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);

            StreamReader standardInput = new StreamReader(Console.OpenStandardInput());
            Console.SetIn(standardInput);

            Console.ReadKey();

            return 0;
        }
    }
}