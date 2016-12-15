namespace MusicGenerator.Input.Midi
{
   public enum EventType
   {
      MidiEvent = 0,
      MetaEvent,
      SystemExclusive
   }

   public class Event
   {
      public EventType EventType { get; set; }
      public int ElapsedTime { get; set; }
      public int StartTime { get; set; }
      public byte[] Data { get; set; }
      public MetaEventType MetaEventType { get; set; }
      public int MetaDataLength { get; set; }
      public int Channel { get; set; }
      public bool IsNoteOn { get; set; }
      public int PitchCode { get; set; }
      public int Volume { get; set; }
      public int DeltaTime { get; set; }
   }
}
