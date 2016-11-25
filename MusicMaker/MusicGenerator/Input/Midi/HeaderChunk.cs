namespace MusicGenerator.Input.Midi
{
   public class HeaderChunk
   {
      public int Label { get; set; }
      public int HeaderLength { get; set; }
      public Format Format { get; set; }
      public short NumberOfTracks { get; set; }
      public short Division { get; set; }
      public int Size { get; set; }
   }

   public enum Format
   {
      SingleTrack = 0,
      MultipleTrack,
      MultipleSong
   }
}
