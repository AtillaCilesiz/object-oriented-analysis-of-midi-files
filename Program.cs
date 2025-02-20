using System;
using MidiProject.Classes;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the MIDI Analyzer!");
        Console.WriteLine("Please select an analysis type:");
        Console.WriteLine("1. Note and Timing Analysis");
        Console.WriteLine("2. Channel Analysis");
        Console.WriteLine("3. Note Frequency Analysis");
        Console.WriteLine("4. Tempo and Dynamics Analysis");
        Console.WriteLine("5. Save All Analyses to File");

        var reader = new MIDIReader("example.mid");
        bool exit = false;

        while (!exit)
        {
            Console.Write("Enter your choice (or 'exit' to quit): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    reader.ReadAndAnalyze();
                    break;
                case "2":
                    reader.AnalyzeChannels();
                    break;
                case "3":
                    reader.AnalyzeNoteFrequencies();
                    break;
                case "4":
                    reader.AnalyzeTempoAndDynamics();
                    break;
                case "5":
                    reader.SaveAnalysisToFile("analysis.txt");
                    break;
                case "exit":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        Console.WriteLine("Thank you for using the MIDI Analyzer!");
    }
}
