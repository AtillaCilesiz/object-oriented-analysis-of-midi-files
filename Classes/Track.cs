using System;
using MidiProject.Classes;

namespace MidiProject.Classes
{
    public class Track : BaseMIDI
    {
        public string TrackName { get; set; }

        public Track(string fileName, string trackName) : base(fileName)
        {
            TrackName = trackName;
        }

        public override void Analyze()
        {
            Console.WriteLine($"Analyzing track: {TrackName} in file {FileName}");
        }
    }
}
