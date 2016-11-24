using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MusicGenerator.MusicStructure;

namespace MusicGenerator.Input.Midi
{
   public class MidiFileReader
   {
      private readonly byte[] bytes;

      public MidiFileReader(string file)
      {
         bytes = File.ReadAllBytes(file);
      }

      public IEnumerable<Note> GetNotes()
      {
         return null;
      }

      public HeaderChunk GetHeaderChunk()
      {
         return new HeaderChunk
         {
            Label = bytes.ToInt32(0),
            HeaderLength = bytes.ToInt32(4),
            Format = (Format)bytes.ToInt16(8),
            NumberOfTracks = bytes.ToInt16(10),
            Division = bytes.ToInt16(12)
         };
      }
   }
}
