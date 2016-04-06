using System.Runtime.CompilerServices;

namespace MusicGenerator.Probability
{
   public class Matrix
   {
      public int Rows => rows;

      public int Cols => cols;

      private int rows, cols;
      private double[,] values;

      public Matrix(int rows, int cols)
      {
         this.rows = rows;
         this.cols = cols;
         values = new double[rows, cols];
      }

      public double this[int i, int j]
      {
         get { return values[i, j]; }
         set { values[i, j] = value; }
      }

      public Matrix AddRow()
      {
         var m = new Matrix(rows + 1, cols);

         for (int i = 0; i < rows; i++)
         {
            for (int j = 0; j < cols; j++)
            {
               m[i, j] = values[i, j];
            }
         }

         return m;
      }

      public Matrix InsertRow(int index)
      {
         var m = new Matrix(rows + 1, cols);

         for (int i = 0; i < rows; i++)
         {
            for (int j = 0; j < cols; j++)
            {
               if (i >= index)
               {
                  m[i + 1, j] = values[i, j];
               }
               else
               {
                  m[i, j] = values[i, j];
               }
            }
         }

         return m;
      }

      public Matrix RemoveRow(int index)
      {
         var m = new Matrix(rows - 1, cols);

         for (var i = 0; i < rows; i++)
         {
            for (var j = 0; j < cols; j++)
            {
               if (i < index)
               {
                  m[i, j] = values[i, j];
               }
               else if (i > index)
               {
                  m[i - 1, j] = values[i, j];
               }
            }   
         }

         return m;
      }

      public Matrix AddColumn()
      {
         var m = new Matrix(rows, cols + 1);

         for (int i = 0; i < rows; i++)
         {
            for (int j = 0; j < cols; j++)
            {
               m[i, j] = values[i, j];
            }
         }

         return m;
      }

      public Matrix InsertColumn(int index)
      {
         var m = new Matrix(rows, cols + 1);

         for (int i = 0; i < rows; i++)
         {
            for (int j = 0; j < cols; j++)
            {
               if (j >= index)
               {
                  m[i, j + 1] = values[i, j];
               }
               else
               {
                  m[i, j] = values[i, j];
               }
            }
         }

         return m;
      }

      public Matrix RemoveColumn(int index)
      {
         var m = new Matrix(rows, cols - 1);

         for (var i = 0; i < rows; i++)
         {
            for (var j = 0; j < cols; j++)
            {
               if (j < index)
               {
                  m[i, j] = values[i, j];
               }
               else if (j > index)
               {
                  m[i, j - 1] = values[i, j];
               }
            }
         }

         return m;
      }
   }
}
