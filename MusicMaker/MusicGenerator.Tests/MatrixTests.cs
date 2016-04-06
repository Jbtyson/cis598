using System;
using System.CodeDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicGenerator.Probability;

namespace MusicGenerator.Tests
{
   [TestClass]
   public class MatrixTests
   {
      private Matrix TestMatrix3x3()
      {
         var m = new Matrix(3, 3);
         for (var i = 0; i < 3; i++)
         {
            for (var j = 0; j < 3; j++)
            {
               m[i, j] = i*3 + j;
            }
         }
         return m;
      }

      [TestMethod]
      public void CanCreateMatrix()
      {
         var m = TestMatrix3x3();

         Assert.AreEqual(0, m[0, 0]);
         Assert.AreEqual(1, m[0, 1]);
         Assert.AreEqual(2, m[0, 2]);
         Assert.AreEqual(3, m[1, 0]);
         Assert.AreEqual(4, m[1, 1]);
         Assert.AreEqual(5, m[1, 2]);
         Assert.AreEqual(6, m[2, 0]);
         Assert.AreEqual(7, m[2, 1]);
         Assert.AreEqual(8, m[2, 2]);
      }

      [TestMethod]
      public void CanAddRow()
      {
         var m = TestMatrix3x3().AddRow();

         m[3, 0] = 0;
         m[3, 1] = 0;
         m[3, 2] = 0;
      }

      [TestMethod]
      public void CanAddColumn()
      {
         var m = TestMatrix3x3().AddColumn();

         m[0, 3] = 0;
         m[1, 3] = 0;
         m[2, 3] = 0;
      }

      [TestMethod]
      public void CanAddRowAndColumn()
      {
         var m = TestMatrix3x3().AddColumn().AddRow();

         m[3, 0] = 0;
         m[3, 1] = 0;
         m[3, 2] = 0;
         m[3, 3] = 0;

         m[0, 3] = 0;
         m[1, 3] = 0;
         m[2, 3] = 0;
      }

      [TestMethod]
      public void CanInsertRow()
      {
      }

      [TestMethod]
      public void CanInsertColumn()
      {
      }

      [TestMethod]
      public void CanInsertRowAndColumn()
      {
      }

      [TestMethod]
      public void CanRemoveRow()
      {
         var m = TestMatrix3x3().RemoveRow(1);
         
         Assert.AreEqual(2, m.Rows);
         Assert.AreEqual(3, m.Cols);

         Assert.AreEqual(0, m[0, 0]);
         Assert.AreEqual(1, m[0, 1]);
         Assert.AreEqual(2, m[0, 2]);

         Assert.AreEqual(6, m[1, 0]);
         Assert.AreEqual(7, m[1, 1]);
         Assert.AreEqual(8, m[1, 2]);
      }

      [TestMethod]
      public void CanRemoveColumn()
      {
         var m = TestMatrix3x3().RemoveColumn(1);

         Assert.AreEqual(3, m.Rows);
         Assert.AreEqual(2, m.Cols);

         Assert.AreEqual(0, m[0, 0]);
         Assert.AreEqual(2, m[0, 1]);

         Assert.AreEqual(3, m[1, 0]);
         Assert.AreEqual(5, m[1, 1]);

         Assert.AreEqual(6, m[2, 0]);
         Assert.AreEqual(8, m[2, 1]);
      }

      [TestMethod]
      public void CanRemoveRowAndColumn()
      {
         var m = TestMatrix3x3().RemoveColumn(1).RemoveRow(1);

         Assert.AreEqual(2, m.Rows);
         Assert.AreEqual(2, m.Cols);

         Assert.AreEqual(0, m[0, 0]);
         Assert.AreEqual(2, m[0, 1]);

         Assert.AreEqual(6, m[1, 0]);
         Assert.AreEqual(8, m[1, 1]);
      }
   }
}
