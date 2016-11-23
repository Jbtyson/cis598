namespace MusicGenerator.MusicStructure
{
   public class Note
   {
      private readonly NoteLength noteLength;

      public string Pitch { get; set; }
      public int StartInterval { get; set; }
      public int EndInterval { get; set; }
      public byte Velocity { get; set; }
      public int NoteId { get; set; }

      public Note(string pitch, int startInterval, NoteLength noteLength, byte velocity = 100)
      {
         this.noteLength = noteLength;

         Pitch = pitch;
         StartInterval = startInterval;
         EndInterval = startInterval + (int)noteLength;
         Velocity = velocity;
         NoteId = PitchCode.GetNoteId(pitch);
      }
   }
}
