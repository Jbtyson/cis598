using System.IO;

namespace MusicGenerator.Probability
{
   public static class Loader
   {
      private static string motifPath = "..\\..\\..\\MusicGenerator\\data\\motifMatrix.csv";

      public static MotifMatrix LoadMotifMatrix()
      {
         var matrixMotif = new MotifMatrix(File.ReadAllLines(motifPath));
         return matrixMotif;
      }
   }
}
