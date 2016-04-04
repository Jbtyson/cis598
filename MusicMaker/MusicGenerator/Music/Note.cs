using Toub.Sound.Midi;

namespace MusicGenerator.Music
{
   public class Note
   {
      public int StartInterval { get; set; }
      public int EndInterval { get; set; }

      private readonly string pitch;
      private readonly NoteLength noteLength;
      private readonly byte velocity;

      private static string[] pitches = {"A","A#","B","C","C#","D","D#","E","F","F#","G","G#"};

      public Note(string pitch, int startInterval, NoteLength noteLength, byte velocity = 100)
      {
         this.pitch = pitch;
         this.noteLength = noteLength;
         this.velocity = velocity;

         StartInterval = startInterval;
         EndInterval = startInterval + (int)noteLength;
      }

      public void Play()
      {
         MidiPlayer.Play(new NoteOn(0, MusicMakerConfig.DefaultChannel, pitch, velocity));
      }

      public void Stop()
      {
         MidiPlayer.Play(new NoteOff(0, MusicMakerConfig.DefaultChannel, pitch, velocity));
      }

      public static string GetNoteName(int id)
      {
         var pitch = pitches[id%12];
         //return pitch + id/12;
         return pitch + 4;
      }
   }
}
