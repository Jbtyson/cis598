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
      private const int markovChainOrder = 3;
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
         metronome.SetSpeed(60);
         sixteenthTickCount = 0;
      }

      public IEnumerable<Note> Generate()
      {
         var midiCsvReader = new MidiCsvReader();
         var trainingDataNotes = midiCsvReader.ConvertFileToNoteList("..\\..\\..\\MusicGenerator\\data\\odeToJoy.csv");

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
         for (var i = 0; i < noteIdList.Count; i++)
         {
            notesToPlay.Enqueue(new Note(PitchCode.GetNoteName(noteIdList[i]), currentNotePosition, (NoteLength)noteLengthList[i]));
            currentNotePosition += noteLengthList[i];
         }

         return notesToPlay;
      }

      public void Play()
      {
         MidiPlayer.OpenMidi();
         metronome.Start();
      }

      public void OnSixteenthTick()
      {
         sixteenthTickCount++;

         if (notesToStop.Count > 0 && notesToStop.Peek().EndInterval <= sixteenthTickCount)
         {
            notesToStop.Dequeue().Stop();
         }

         if (notesToPlay.Count > 0 && notesToPlay.Peek().StartInterval <= sixteenthTickCount)
         {
            var note = notesToPlay.Dequeue();
            note.Play();
            notesToStop.Enqueue(note);
         }
      }
   }
}
