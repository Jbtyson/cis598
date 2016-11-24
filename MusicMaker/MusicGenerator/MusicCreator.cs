using System.Collections.Generic;
using System.Linq;
using MusicGenerator.Input;
using MusicGenerator.MusicStructure;
using MusicGenerator.Probability;

namespace MusicGenerator
{
   public class MusicCreator
   {
      private readonly int markovChainOrder;

      public MusicCreator(int markovChainOrder)
      {
         this.markovChainOrder = markovChainOrder;
      }

      public IEnumerable<Note> GenerateNotesFromFile(string file)
      {
         var midiCsvReader = new MidiCsvReader();
         var trainingDataNotes = midiCsvReader.ConvertFileToNoteList(file).ToList();

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

      public IEnumerable<Note> MergeNoteLists(IEnumerable<Note> listOne, IEnumerable<Note> listTwo)
      {
         var notes = new List<Note>();
         listOne.ToList().ForEach(n => notes.Add(n));
         listTwo.ToList().ForEach(n => notes.Add(n));
         notes.Sort((a, b) => a.StartInterval.CompareTo(b.StartInterval));

         return notes;
      }
   }
}
