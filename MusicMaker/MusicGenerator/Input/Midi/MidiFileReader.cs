using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MusicGenerator.MusicStructure;

namespace MusicGenerator.Input.Midi
{
   public class MidiFileReader
   {
      private const int midiEventLength = 3;
      private readonly byte[] data;

      public MidiFileReader(string file)
      {
         data = File.ReadAllBytes(file);
      }

      public IEnumerable<Note> GetNotes()
      {
         var header = GetHeaderChunk();
         var tracks = GetTrackChunks(header.NumberOfTracks, header.Size);
         var notes = new List<Note>();

         // cheating
         //foreach (var track in tracks)
         //{
         //   var startInterval = 0;
         //   for (var i = track.StartIndex; i < track.Size; i++)
         //   {
         //      try
         //      {
         //         if ((data[i] & 0x90) == 0x90 && data[i+2] > 0)
         //         {
         //            notes.Add(new Note(MidiNoteConverter.midiNoteCodes[data[i + 1]], startInterval, NoteLength.Quarter));
         //            startInterval += 2;
         //         }
         //      }
         //      catch (Exception) { }
         //   }
         //}

         foreach (var track in tracks)
            foreach (var note in MidiNoteConverter.ConvertMidiEventsToNotes(track.TrackEvents))
               notes.Add(note);


         return notes;
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
            trackChunk.StartIndex = index;
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

         while (currentIndex < index + size && currentIndex < data.Length - 1)
         {
            var newEvent = new Event();
            int bytesRead;
            newEvent.DeltaTime = data.ReadVariableLengthValue(index, out bytesRead);
            currentIndex += bytesRead;

            var eventType = data[currentIndex];
            switch (eventType)
            {
               case 0xFF:
                  newEvent.EventType = EventType.MetaEvent;
                  // skip event type byte
                  currentIndex++;
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

      // Returns bytes read
      public int ProcessMetaEvent(Event metaEvent, int index)
      {
         metaEvent.MetaEventType = (MetaEventType)data[index++];

         int bytesRead;
         metaEvent.MetaDataLength = data.ReadVariableLengthValue(index, out bytesRead);
         metaEvent.Data = data.CopyRange(index + bytesRead, metaEvent.MetaDataLength);

         return 1 + bytesRead + metaEvent.MetaDataLength;
      }

      public int ProcessMidiEvent(Event midiEvent, int index)
      {
         switch (data[index] & 0xF0)
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

         midiEvent.Channel = data[index] & 15;
         midiEvent.PitchCode = data[index + 1];
         midiEvent.Volume = data[index + 2];

         return midiEventLength;
      }
   }
}
