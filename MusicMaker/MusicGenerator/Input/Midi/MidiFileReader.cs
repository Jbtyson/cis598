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
      private readonly byte[] data;

      public MidiFileReader(string file)
      {
         data = File.ReadAllBytes(file);
      }

      public IEnumerable<Note> GetNotes()
      {
         return null;
      }

      public HeaderChunk GetHeaderChunk()
      {
         return new HeaderChunk
         {
            Label = data.ToInt32(0),
            HeaderLength = data.ToInt32(4),
            Format = (Format)data.ToInt16(8),
            NumberOfTracks = data.ToInt16(10),
            Division = data.ToInt16(12),
            Size = 14
         };
      }

      public IEnumerable<TrackChunk> GetTrackChunks(int tracks, int index)
      {
         var trackChunks = new List<TrackChunk>();
         var offset = 0;
         for (var i = 0; i < tracks; i++)
         {
            var trackChunk = GetTrackChunk(index + offset);
            trackChunks.Add(trackChunk);
            offset += trackChunk.Size;
         }

         return trackChunks;
      }

      public TrackChunk GetTrackChunk(int index)
      {
         var trackChunk = new TrackChunk
         {
            Label = data.ToInt32(index),
            Length = data.ToInt32(index + 4),
            TrackEvents = new List<TrackEvent>()
         };
         trackChunk.Size = trackChunk.Length + 8;

         return trackChunk;
      }
   }
}
