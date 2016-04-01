using System;

namespace MusicGenerator.Probability
{
   public class MotifMatrix
   {
      private const int SIZE = 12;
      private double[,] values;

      public MotifMatrix(string[] values)
      {
         this.values = new double[SIZE, SIZE];

         for (var i = 1; i < SIZE+1; i++)
         {
            for(var j = 1; j < SIZE+1; j++)

            this.values[i-1, j-1] = double.Parse(values[i].Split(',')[j]);
         }
      }

      public int GetNextNoteId(int currentId)
      {
         var rand = new Random().NextDouble()*100;
         for (var i = 0; i < SIZE; i++)
         {
            rand -= values[currentId, i];
            if (rand <= 0)
            {
               return i;
            }
         }

         throw new ArgumentOutOfRangeException("Random number unable to bee found.");
      }
   }
}
