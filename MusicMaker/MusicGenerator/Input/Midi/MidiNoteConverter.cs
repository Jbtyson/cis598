﻿using System.Collections.Generic;
using MusicGenerator.MusicStructure;

namespace MusicGenerator.Input.Midi
{
   public static class MidiNoteConverter
   {

      #region noteCodes
      public static readonly Dictionary<int, string> midiNoteCodes = new Dictionary<int, string>
      {
         {0, "C0"},
         {1, "C#0"},
         {2, "D0"},
         {3, "D#0"},
         {4, "E0"},
         {5, "F0"},
         {6, "F#0"},
         {7, "G0"},
         {8, "G#0"},
         {9, "A0"},
         {10, "A#0"},
         {11, "B0"},
         {12, "C1"},
         {13, "C#1"},
         {14, "D1"},
         {15, "D#1"},
         {16, "E1"},
         {17, "F1"},
         {18, "F#1"},
         {19, "G1"},
         {20, "G#1"},
         {21, "A1"},
         {22, "A#1"},
         {23, "B1"},
         {24, "C2"},
         {25, "C#2"},
         {26, "D2"},
         {27, "D#2"},
         {28, "E2"},
         {29, "F2"},
         {30, "F#2"},
         {31, "G2"},
         {32, "G#2"},
         {33, "A2"},
         {34, "A#2"},
         {35, "B2"},
         {36, "C3"},
         {37, "C#3"},
         {38, "D3"},
         {39, "D#3"},
         {40, "E3"},
         {41, "F3"},
         {42, "F#3"},
         {43, "G3"},
         {44, "G#3"},
         {45, "A3"},
         {46, "A#3"},
         {47, "B3"},
         {48, "C4"},
         {49, "C#4"},
         {50, "D4"},
         {51, "D#4"},
         {52, "E4"},
         {53, "F4"},
         {54, "F#4"},
         {55, "G4"},
         {56, "G#4"},
         {57, "A4"},
         {58, "A#4"},
         {59, "B4"},
         {60, "C5"},
         {61, "C#5"},
         {62, "D5"},
         {63, "D#5"},
         {64, "E5"},
         {65, "F5"},
         {66, "F#5"},
         {67, "G5"},
         {68, "G#5"},
         {69, "A5"},
         {70, "A#5"},
         {71, "B5"},
         {72, "C6"},
         {73, "C#6"},
         {74, "D6"},
         {75, "D#6"},
         {76, "E6"},
         {77, "F6"},
         {78, "F#6"},
         {79, "G6"},
         {80, "G#6"},
         {81, "A6"},
         {82, "A#6"},
         {83, "B6"},
         {84, "C7"},
         {85, "C#7"},
         {86, "D7"},
         {87, "D#7"},
         {88, "E7"},
         {89, "F7"},
         {90, "F#7"},
         {91, "G7"},
         {92, "G#7"},
         {93, "A7"},
         {94, "A#7"},
         {95, "B7"},
         {96, "C8"},
         {97, "C#8"},
         {98, "D8"},
         {99, "D#8"},
         {100, "E8"},
         {101, "F8"},
         {102, "F#8"},
         {103, "G8"},
         {104, "G#8"},
         {105, "A8"},
         {106, "A#8"},
         {107, "B9"},
         {108, "C9"},
         {109, "C#9"},
         {110, "D9"},
         {111, "D#9"},
         {112, "E9"},
         {113, "F9"},
         {114, "F#9"},
         {115, "G9"},
         {116, "G#9"},
         {117, "A9"},
         {118, "A#9"},
         {119, "B9"},
         {120, "C10"},
         {121, "C#10"},
         {122, "D10"},
         {123, "D#10"},
         {124, "E10"},
         {125, "F10"},
         {126, "F#10"},
         {127, "G10"}
      };
      #endregion

      public static IEnumerable<Note> ConvertMidiEventsToNotes(IEnumerable<Event> events, int division)
      {
         var notes = new List<Note>();

         foreach (var e in events)
         {
            if (e.IsNoteOn)
            {
               var pitchCode = midiNoteCodes[e.PitchCode];
               var ratio = (int)(e.StartTime/(double)division * 16);
               var note = new Note(pitchCode, ratio, NoteLength.Quarter);
               notes.Add(note);
            }
         }

         return notes;
      }
   }
}