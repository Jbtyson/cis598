using System.Collections.Generic;

namespace MusicGenerator.Input.Midi
{
   public class TrackChunk
   {
      public int StartIndex { get; set; }
      public int Label { get; set; }
      public int Length { get; set; }
      public IEnumerable<Event> TrackEvents { get; set; }
      public int Size { get; set; }
   }
}
