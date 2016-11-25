namespace MusicGenerator.Input.Midi
{
   public enum MetaEventType
   {
      SequenceNumber = 0x00,
      TextEvent = 0x01,
      CopyrightNotiice = 0x02,
      SequenceOrTrackName = 0x03,
      InstrumentName = 0x04,
      LyricText = 0x05,
      MarkerText = 0x06,
      CuePoint = 0x07,
      MidiChannelPrefixAssignment = 0x20,
      EndOfTrack = 0x2F,
      TempoSetting = 0x51,
      SpmteOffset = 0x54,
      TimeSignature = 0x58,
      KeySignature = 0x59,
      SequencerSpecificEvent = 0x7F
   }
}
