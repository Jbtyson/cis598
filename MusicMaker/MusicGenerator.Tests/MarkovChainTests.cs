using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicGenerator.Probability;
using System.Collections.Generic;

namespace MusicGenerator.Tests
{
   [TestClass]
   public class MarkovChainTests
   {
      [TestMethod]
      public void TestMarkovChain()
      {
         var mc = new MarkovChain(3);
         mc.AddList(new List<int>
            {
               0, 1, 2, 3, 4, 5, 6, 7, 8, 9
            }
         );
         mc.Create();

         Assert.AreEqual(2, mc.GetNext(new List<int> { 0, 1 }));
         Assert.AreEqual(3, mc.GetNext(new List<int> { 1, 2 }));
         Assert.AreEqual(4, mc.GetNext(new List<int> { 2, 3 }));
         Assert.AreEqual(5, mc.GetNext(new List<int> { 3, 4 }));
         Assert.AreEqual(6, mc.GetNext(new List<int> { 4, 5 }));
         Assert.AreEqual(7, mc.GetNext(new List<int> { 5, 6 }));
         Assert.AreEqual(8, mc.GetNext(new List<int> { 6, 7 }));
         Assert.AreEqual(9, mc.GetNext(new List<int> { 7, 8 }));
      }
   }
}
