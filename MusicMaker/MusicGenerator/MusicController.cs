using Toub.Sound.Midi;
using MusicGenerator.Music;
using System.Collections.Generic;
using MusicGenerator.Input;
using MusicGenerator.Probability;
using System.Linq;

namespace MusicGenerator
{
   public class MusicController
   {
      private const int markovChainOrder = 5;
      private readonly Metronome metronome;
      private Queue<Note> notesToPlay;
      private Queue<Note> notesToStop;
      private int sixteenthTickCount;

      public MusicController()
      {
         metronome = new Metronome();
         notesToPlay = new Queue<Note>();
         notesToStop = new Queue<Note>();
      }

      public void Init()
      {
         metronome.OnSixteenthTick = OnSixteenthTick;
         metronome.SetSpeed(30);
         sixteenthTickCount = 0;
      }

      public IEnumerable<Note> GenerateMelody()
      {
         var midiCsvReader = new MidiCsvReader();
         var trainingDataNotes = midiCsvReader.ConvertFileToNoteList("..\\..\\..\\MusicGenerator\\data\\odeToJoyMelody.csv");

         var noteMarkovChain = new MarkovChain(markovChainOrder);
         noteMarkovChain.AddNoteList(trainingDataNotes);
         noteMarkovChain.Create();
         var noteIdList = new List<int>();
         var queue = new Queue<int>();
         for (var i = 0; i < 100; i++)
         {
            var noteId = noteMarkovChain.GetNext(queue);
            queue.Enqueue(noteId);

            if (queue.Count > markovChainOrder)
               queue.Dequeue();
            noteIdList.Add(noteId);
         }

         var noteLengthMarkovChain = new MarkovChain(markovChainOrder);
         noteLengthMarkovChain.AddList(trainingDataNotes.Select(n => n.EndInterval - n.StartInterval));
         noteLengthMarkovChain.Create();
         var noteLengthList = new List<int>();
         var noteLengthQueue = new Queue<int>();
         for (var i = 0; i < 100; i++)
         {
            var noteLength = noteLengthMarkovChain.GetNext(noteLengthQueue);
            noteLengthQueue.Enqueue(noteLength);

            if (noteLengthQueue.Count > markovChainOrder)
               noteLengthQueue.Dequeue();
            noteLengthList.Add(noteLength);
         }

         var currentNotePosition = 0;
         var notes = new List<Note>();
         for (var i = 0; i < noteIdList.Count; i++)
         {
            notes.Add(new Note(PitchCode.GetNoteName(noteIdList[i]), currentNotePosition, (NoteLength)noteLengthList[i]));
            currentNotePosition += noteLengthList[i];
         }

         return notes;
      }

      public IEnumerable<Note> GenerateBass()
      {
         var midiCsvReader = new MidiCsvReader();
         var trainingDataNotes = midiCsvReader.ConvertFileToNoteList("..\\..\\..\\MusicGenerator\\data\\odeToJoyBass.csv");

         var noteMarkovChain = new MarkovChain(markovChainOrder);
         noteMarkovChain.AddNoteList(trainingDataNotes);
         noteMarkovChain.Create();
         var noteIdList = new List<int>();
         var queue = new Queue<int>();
         for (var i = 0; i < 100; i++)
         {
            var noteId = noteMarkovChain.GetNext(queue);
            queue.Enqueue(noteId);

            if (queue.Count > markovChainOrder)
               queue.Dequeue();
            noteIdList.Add(noteId);
         }

         var noteLengthMarkovChain = new MarkovChain(markovChainOrder);
         noteLengthMarkovChain.AddList(trainingDataNotes.Select(n => n.EndInterval - n.StartInterval));
         noteLengthMarkovChain.Create();
         var noteLengthList = new List<int>();
         var noteLengthQueue = new Queue<int>();
         for (var i = 0; i < 100; i++)
         {
            var noteLength = noteLengthMarkovChain.GetNext(noteLengthQueue);
            noteLengthQueue.Enqueue(noteLength);

            if (noteLengthQueue.Count > markovChainOrder)
               noteLengthQueue.Dequeue();
            noteLengthList.Add(noteLength);
         }

         var currentNotePosition = 0;
         var notes = new List<Note>();
         for (var i = 0; i < noteIdList.Count; i++)
         {
            notes.Add(new Note(PitchCode.GetNoteName(noteIdList[i]), currentNotePosition, (NoteLength)noteLengthList[i], channelOffset:1));
            currentNotePosition += noteLengthList[i];
         }

         return notes;
      }

      public void Play(IEnumerable<Note> notes)
      {
         foreach(var note in notes)
         {
            notesToPlay.Enqueue(note);
         }
         MidiPlayer.OpenMidi();
         metronome.Start();
      }

      public void OnSixteenthTick()
      {
         sixteenthTickCount++;

         while (notesToStop.Count > 0 && notesToStop.Peek().EndInterval <= sixteenthTickCount)
         {
            notesToStop.Dequeue().Stop();
         }

         while (notesToPlay.Count > 0 && notesToPlay.Peek().StartInterval <= sixteenthTickCount)
         {
            var note = notesToPlay.Dequeue();
            note.Play();
            notesToStop.Enqueue(note);
         }
      }
   }
}
