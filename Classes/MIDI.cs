using System;

namespace MidiProject.Classes
{
    public abstract class BaseMIDI
    {
        public string FileName { get; set; }
        public static int TotalFilesProcessed = 0;

        public BaseMIDI(string fileName)
        {
            FileName = fileName;
            TotalFilesProcessed++;
        }

        public abstract void Analyze();
    }
}
