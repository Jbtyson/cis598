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

      public int GetNoteId()
      {
         
      }
   }
}
