using System;
using System.Collections.Generic;

namespace MusicGenerator.Music
{
    public class ChordGenerator
    {
        public static int[] NoInversion = new int[3] { 0, 2, 4 };
        public static int[] FirstInversion = new int[3] { 0, 2, -6 };

        public ChordGenerator()
        {
        }

        public Chord GenerateMajor(Pitch root, int inversion, int startInterval, NoteLength noteLength, byte velocity = 100)
        {
            var inversionOffset = new int[3];

            switch (inversion)
            {
                case 0:
                    inversionOffset = NoInversion;
                    break;
                case 1:
                    inversionOffset = FirstInversion;
                    break;
            };

            int note1 = (int)root + inversionOffset[0];
            int note2 = (int)root + inversionOffset[1];
            int note3 = (int)root + inversionOffset[2];

            var notes = new List<Note>
            {
                new Note(PitchCode.GetNoteName(note1), startInterval, noteLength, velocity),
                new Note(PitchCode.GetNoteName(note2), startInterval, noteLength, velocity),
                new Note(PitchCode.GetNoteName(note3), startInterval, noteLength, velocity)
            };

            return new Chord
            {
                Notes = notes
            };
        }

        public Chord GenerateMinor(Pitch root, int inversion)
        {
            throw new NotImplementedException();
        }
    }
}
