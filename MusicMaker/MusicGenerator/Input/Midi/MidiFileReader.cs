using System.Collections;
using System.Collections.Generic;
using System.IO;
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
         };
         trackChunk.Size = trackChunk.Length + 8;
         trackChunk.TrackEvents = GetMidiEventsForTrack(index + 8, trackChunk.Size - 8);

         return trackChunk;
      }

      public IEnumerable<Event> GetMidiEventsForTrack(int index, int size)
      {
         var events = new List<Event>();
         var currentIndex = index;
         while (currentIndex < index + size)
         {
            var newEvent = new Event();
            var eventType = data[currentIndex];
            switch (eventType)
            {
               case 0xFF:
                  newEvent.EventType = EventType.MetaEvent;
                  currentIndex += ProcessMetaEvent(newEvent, currentIndex);
                  break;
               case 0xF7:
               case 0xF0:
                  newEvent.EventType = EventType.SystemExclusive;
                  break;
               default:
                  newEvent.EventType = EventType.MidiEvent;
                  break;
            }

            events.Add(newEvent);
         }

         return events;
      }

      // Returns number of bytes read and sets value to the value read
      public int ReadVariableLengthValue(int index, out int value)
      {
         var firstBitCleared = false;
         var currentByte = 0;
         while (!firstBitCleared)
         {
            var b = data[index + currentByte];
            if ((b &= 0x80) > 0)
               b -= 0x80;
            else
               firstBitCleared = true;

         }
      }

      // Returns bytes read
      public int ProcessMetaEvent(Event metaEvent, int index)
      {
         metaEvent.MetaEventType = (MetaEventType) data[index + 1];
         int metaDataLength;
         var bytesRead = ReadVariableLengthValue(index + 2, out metaDataLength);
         metaEvent.MetaDataLength = metaDataLength;
         metaEvent.Data = data.CopyRange(index + 2 + bytesRead, metaDataLength);
         return 2 + bytesRead + metaDataLength;
      }
   }
}
