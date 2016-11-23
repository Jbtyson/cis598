using System;
using Toub.Sound.Midi;

namespace MusicGenerator.Music
{
   public class Note
   {
      public int StartInterval { get; set; }
      public int EndInterval { get; set; }
      public int NoteId { get; set; }
      public string Pitch { get; set; }

      private readonly NoteLength noteLength;
      private readonly byte velocity;

      private static string[] pitches = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };

      public Note(string pitch, int startInterval, NoteLength noteLength, byte velocity = 100)
      {
         this.Pitch = pitch;
         this.noteLength = noteLength;
         this.velocity = velocity;

         StartInterval = startInterval;
         EndInterval = startInterval + (int)noteLength;
         NoteId = PitchCode.GetNoteId(pitch);
      }

      public void Play()
      {
         MidiPlayer.Play(new NoteOn(0, MusicMakerConfig.DefaultChannel, Pitch, velocity));
      }

      public void Stop()
      {
         MidiPlayer.Play(new NoteOff(0, MusicMakerConfig.DefaultChannel, Pitch, velocity));
      }

      public static string GetNoteName(int id)
      {
         var pitch = pitches[id % 12];
         //return pitch + id/12;
         return pitch + 4;
      }

      public static int GetNoteId(string name)
      {
         if (name.Length == 2)
         {
            for (var i = 0; i < pitches.Length; i++)
            {
               if (name[0].ToString() == pitches[i])
               {
                  return int.Parse(name[1].ToString()) * 12 + i;
               }
            }
         }
         else if (name.Length == 3)
         {
            var pitch = name.Substring(0, 1);

            for (var i = 0; i < pitches.Length; i++)
            {
               if (pitch == pitches[i])
               {
                  return int.Parse(name[2].ToString()) * 12 + i;
               }
            }
         }

         throw new ArgumentException("Note name not found.");
      }
   }
}
