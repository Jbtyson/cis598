using System;
using System.Collections.Generic;
using System.Linq;
using MusicGenerator.Music;

namespace MusicGenerator.Probability
{
   public class MotifMatrix
   {
      private const int SIZE = 12;
      private double[,] values, scaleValues;
      private NoteLength[] noteLengths = {NoteLength.Sixteenth, NoteLength.Eigth, NoteLength.Quarter, NoteLength.Half};
      private Random random;
      private int root;
      private int[] scale;

      public MotifMatrix(string[] values)
      {
         this.values = new double[SIZE, SIZE];
         random = new Random();

         for (var i = 1; i < SIZE + 1; i++)
         {
            for (var j = 1; j < SIZE + 1; j++)

               this.values[i - 1, j - 1] = double.Parse(values[i].Split(',')[j]);
         }
      }

      public int GetNextNoteId(int currentId)
      {
         var sum = scale.Sum();
         var rand = random.NextDouble()*sum;
         for (var i = 0; i < SIZE; i++)
         {
            rand -= values[currentId, i];
            if (rand <= 0)
            {
               return scale[i];
            }
         }

         throw new ArgumentOutOfRangeException("Random number unable to be found.");
      }

      public NoteLength GetNextNoteLength(NoteLength prevNote)
      {
         var rand = random.Next(100);

         if (rand < 50)
            return NoteLength.Quarter;
         if (rand < 75)
            return NoteLength.Eigth;
         if (rand < 85)
            return NoteLength.Half;
         if (rand < 95)
            return NoteLength.Quarter;
         return NoteLength.Sixteenth;
      }

      public IEnumerable<Note> GenerateMotif(int numNotes)
      {
         var notes = new List<Note>();
         var lastNote = 0;
         var lastLength = NoteLength.Quarter;
         var startTick = 0;

         for (var i = 0; i < numNotes; i++)
         {
            lastNote = GetNextNoteId(lastNote);
            lastLength = GetNextNoteLength(lastLength);
            notes.Add(new Note(Note.GetNoteName(lastNote), startTick, lastLength));
            startTick += (int) lastLength;
         }

         return notes;
      }

      public void SetKey(int noteId, int[] scale)
      {
         var m = new Matrix(0,0);

         root = noteId;
         this.scale = new int[scale.Length];
         for (var i = 0; i < scale.Length; i++)
         {
            this.scale[i] = root + scale[i];
            //scaleValues[i] = values[i];
         }
      }
   }
}
