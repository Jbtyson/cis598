using System.Collections.Generic;
using System.Linq;

namespace MusicGenerator.Music
{
   public static class PitchCode
   {
      private static Dictionary<string, int> pitchCodes = new Dictionary<string, int>
      {
         {"C4", 72},
         {"C#4", 73},
         {"D4", 74},
         {"D#4", 75},
         {"E4", 76},
         {"F4", 77},
         {"F#4", 78},
         {"F4", 79},
         {"G#4", 80},
         {"A4", 81},
         {"A#4", 82},
         {"B4", 83}
      };

      public static string GetNoteName(int noteId)
      {
         return pitchCodes.First(p => p.Value == noteId).Key;
      }

      public static int GetNoteId(string noteName)
      {
         return pitchCodes[noteName];
      }
   }
}
