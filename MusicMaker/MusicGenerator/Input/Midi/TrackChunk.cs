using System.Collections.Generic;

namespace MusicGenerator.Input.Midi
{
   public class TrackChunk
   {
      public int Label;
      public int Length;
      public IEnumerable<TrackEvent> TrackEvents { get; set; }
      public int Size;
   }
}
