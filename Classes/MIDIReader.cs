using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MidiProject.Classes
{
    public class MIDIReader : BaseMIDI
    {
        public MIDIReader(string fileName) : base(fileName) { }

        public override void Analyze()
        {
            ReadAndAnalyze();
            AnalyzeChannels();
            AnalyzeNoteFrequencies();
            AnalyzeTempoAndDynamics();
        }

        public void ReadAndAnalyze()
        {
            try
            {
                var midiFile = MidiFile.Read(FileName);
                Console.WriteLine($"Successfully loaded MIDI file: {FileName}");

                var notes = midiFile.GetNotes();
                Console.WriteLine("Notes (first 10):");
                foreach (var note in notes.Take(10))
                {
                    Console.WriteLine($"Note: {note.NoteName}, Time: {note.Time}");
                }

                Console.WriteLine($"Total Notes: {notes.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading MIDI file: {ex.Message}");
            }
        }

        public void AnalyzeChannels()
        {
            try
            {
                var midiFile = MidiFile.Read(FileName);
                Console.WriteLine("\nChannel Analysis:");

                var events = midiFile.GetTimedEvents();
                var channels = new Dictionary<int, List<string>>();

                foreach (var midiEvent in events)
                {
                    if (midiEvent.Event is NoteOnEvent noteOn)
                    {
                        int channel = noteOn.Channel;
                        string note = ConvertNoteNumberToName(noteOn.NoteNumber);

                        if (!channels.ContainsKey(channel))
                        {
                            channels[channel] = new List<string>();
                        }
                        channels[channel].Add(note);
                    }
                }

                foreach (var channel in channels)
                {
                    Console.WriteLine($"Channel {channel.Key} (first 10 notes): {string.Join(", ", channel.Value.Take(10))}...");
                    Console.WriteLine($"Total Notes in Channel {channel.Key}: {channel.Value.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error analyzing channels: {ex.Message}");
            }
        }

        public void AnalyzeNoteFrequencies()
        {
            try
            {
                var midiFile = MidiFile.Read(FileName);
                var events = midiFile.GetTimedEvents();
                var noteFrequencies = new Dictionary<string, int>();

                foreach (var midiEvent in events)
                {
                    if (midiEvent.Event is NoteOnEvent noteOn)
                    {
                        string note = ConvertNoteNumberToName(noteOn.NoteNumber);

                        if (!noteFrequencies.ContainsKey(note))
                        {
                            noteFrequencies[note] = 0;
                        }
                        noteFrequencies[note]++;
                    }
                }

                Console.WriteLine("\nNote Frequencies:");
                foreach (var entry in noteFrequencies.OrderBy(e => e.Key))
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value} times");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error analyzing note frequencies: {ex.Message}");
            }
        }

        public void AnalyzeTempoAndDynamics()
        {
            try
            {
                var midiFile = MidiFile.Read(FileName);
                var tempoMap = midiFile.GetTempoMap();
                var tempos = tempoMap.GetTempoChanges();

                Console.WriteLine("\nTempo Changes:");
                foreach (var tempo in tempos)
                {
                    Console.WriteLine($"Tempo: {tempo.Value.BeatsPerMinute} BPM");
                }

                var dynamics = midiFile.GetTimedEvents()
                    .Where(e => e.Event is NoteOnEvent)
                    .Select(e => (int)((NoteOnEvent)e.Event).Velocity);

                Console.WriteLine("\nDynamics:");
                Console.WriteLine($"Average Velocity: {dynamics.Average():F2}");
                Console.WriteLine($"Max Velocity: {dynamics.Max()}");
                Console.WriteLine($"Min Velocity: {dynamics.Min()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error analyzing tempo and dynamics: {ex.Message}");
            }
        }

        public void SaveAnalysisToFile(string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Analysis of MIDI file:");

                    var midiFile = MidiFile.Read(FileName);

                    var notes = midiFile.GetNotes();
                    writer.WriteLine("\nNotes (first 10):");
                    foreach (var note in notes.Take(10))
                    {
                        writer.WriteLine($"Note: {note.NoteName}, Time: {note.Time}");
                    }
                    writer.WriteLine($"Total Notes: {notes.Count}");

                    var events = midiFile.GetTimedEvents();
                    var noteFrequencies = new Dictionary<string, int>();
                    foreach (var midiEvent in events)
                    {
                        if (midiEvent.Event is NoteOnEvent noteOn)
                        {
                            string note = ConvertNoteNumberToName(noteOn.NoteNumber);
                            if (!noteFrequencies.ContainsKey(note))
                            {
                                noteFrequencies[note] = 0;
                            }
                            noteFrequencies[note]++;
                        }
                    }

                    writer.WriteLine("\nNote Frequencies:");
                    foreach (var entry in noteFrequencies.OrderBy(e => e.Key))
                    {
                        writer.WriteLine($"{entry.Key}: {entry.Value} times");
                    }

                    var channels = new Dictionary<int, List<string>>();
                    foreach (var midiEvent in events)
                    {
                        if (midiEvent.Event is NoteOnEvent noteOn)
                        {
                            int channel = noteOn.Channel;
                            string note = ConvertNoteNumberToName(noteOn.NoteNumber);
                            if (!channels.ContainsKey(channel))
                            {
                                channels[channel] = new List<string>();
                            }
                            channels[channel].Add(note);
                        }
                    }

                    writer.WriteLine("\nChannel Analysis:");
                    foreach (var channel in channels)
                    {
                        writer.WriteLine($"Channel {channel.Key}: {string.Join(", ", channel.Value.Take(10))}...");
                        writer.WriteLine($"Total Notes in Channel {channel.Key}: {channel.Value.Count}");
                    }

                    var tempoMap = midiFile.GetTempoMap();
                    var tempos = tempoMap.GetTempoChanges();
                    writer.WriteLine("\nTempo Changes:");
                    foreach (var tempo in tempos)
                    {
                        writer.WriteLine($"Tempo: {tempo.Value.BeatsPerMinute} BPM");
                    }

                    var dynamics = midiFile.GetTimedEvents()
                        .Where(e => e.Event is NoteOnEvent)
                        .Select(e => (int)((NoteOnEvent)e.Event).Velocity);

                    writer.WriteLine("\nDynamics:");
                    writer.WriteLine($"Average Velocity: {dynamics.Average():F2}");
                    writer.WriteLine($"Max Velocity: {dynamics.Max()}");
                    writer.WriteLine($"Min Velocity: {dynamics.Min()}");
                }

                Console.WriteLine($"Analysis saved to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving analysis: {ex.Message}");
            }
        }

        private string ConvertNoteNumberToName(int noteNumber)
        {
            var note = Melanchall.DryWetMidi.MusicTheory.Note.Get((SevenBitNumber)noteNumber);
            return note.ToString();
        }
    }
}