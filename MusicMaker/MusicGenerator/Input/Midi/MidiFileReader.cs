﻿using System;
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
            offset += trackChunk.Size + 8;
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
         if (trackChunk.Label != 1297379947)
            throw new ArgumentOutOfRangeException("Invalid Track Header.");
         trackChunk.Size = trackChunk.Length + 8;
         trackChunk.TrackEvents = GetMidiEventsForTrack(index + 8, trackChunk.Size);

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
                  currentIndex += ProcessMidiEvent(newEvent, currentIndex);
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
         var variableBytes = new byte[4];

         while (!firstBitCleared)
         {
            var b = data[index + currentByte];
            if ((b & 0x80) > 0)
               b -= 0x80;
            else
               firstBitCleared = true;
            variableBytes[currentByte] = b;
         }

         // shift byte 2 down
         if ((variableBytes[1] & 0x01) > 0)
         {
            variableBytes[0] += 0x80;
            variableBytes[1] = (byte)(variableBytes[1] >> 1);
         }
         // shift byte 3 down
         if ((variableBytes[2] & 0x01) > 0)
         {
            variableBytes[1] += 0x40;
            variableBytes[2] = (byte)(variableBytes[2] >> 1);
            if ((variableBytes[2] & 0x01) > 0)
            {
               variableBytes[1] += 0x80;
               variableBytes[2] = (byte)(variableBytes[2] >> 1);
            }
         }
         // shift byte 4 down
         if ((variableBytes[3] & 0x01) > 0)
         {
            variableBytes[2] += 0x20;
         }

         value = 0;
         return currentByte;
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

      public int ProcessMidiEvent(Event midiEvent, int index)
      {
         int value;
         var bytesRead = ReadVariableLengthValue(index, out value);
         index += bytesRead;

         switch (data[index] & 240)
         {
            case 0x90:
               midiEvent.IsNoteOn = true;
               break;
            case 0x80:
               midiEvent.IsNoteOn = false;
               break;
            default:
               return 4;
         }

         midiEvent.Channel = data[index + 1] & 15;
         midiEvent.PitchCode = data[index + 2];
         midiEvent.Volume = data[index + 3];

         return 4;
      }
   }
}
