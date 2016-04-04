using System;
using MusicGenerator.Music;

namespace MusicGenerator.Probability
{
   public class MotifMatrix
   {
      private const int SIZE = 12;
      private double[,] values;
      private NoteLength[] noteLengths = { NoteLength.Sixteenth, NoteLength.Eigth, NoteLength.Quarter, NoteLength.Half };
      private Random random;

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
         var rand = random.NextDouble() * 100;
         for (var i = 0; i < SIZE; i++)
         {
            rand -= values[currentId, i];
            if (rand <= 0)
            {
               return i;
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
   }
}
