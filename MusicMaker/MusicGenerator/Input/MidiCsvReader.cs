using System.Collections;
using System.Collections.Generic;
using System.IO;
using MusicGenerator.Music;

namespace MusicGenerator.Input
{
    public class MidiCsvReader
    {
        private string path;
        private int currentTime;

        public MidiCsvReader(string path)
        {
            this.path = path;
        }

        private IEnumerable<Note> ConvertFileToNoteList()
        {
            var notes = new List<Note>();

            using (var sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    notes.Add(ParseInputLine(sr.ReadLine()));
                }
            }

            return notes;
        }

        private Note ParseInputLine(string line)
        {
            var parts = line.Split(',');
            var note = new Note(parts[0], currentTime, (NoteLength)int.Parse(parts[1]));
            currentTime += int.Parse(parts[1]);
            return note;
        }

    }
}
