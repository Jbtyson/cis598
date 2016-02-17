using System.Collections.Generic;

namespace MusicGenerator.Music
{
    public class Chord
    {
        public List<Note> Notes;

        public Chord()
        {
            Notes = new List<Note>();
        }

        public Chord(List<Note> notes)
        {
            Notes = notes;
        }
    }
}
