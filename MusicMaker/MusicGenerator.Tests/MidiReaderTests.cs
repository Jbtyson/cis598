using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicGenerator.Input.Midi;

namespace MusicGenerator.Tests
{
   [TestClass]
   public class MidiReaderTests
   {
      [TestMethod]
      public void CanGetHeaderChunk()
      {
         var mfr = new MidiFileReader("..\\..\\..\\MusicGenerator\\data\\maryHadALittleLamb.mid");
         var hc = mfr.GetHeaderChunk();
         Assert.AreEqual(0x4d546864, hc.Label);
         Assert.AreEqual(6, hc.HeaderLength);
         Assert.AreEqual(Format.MultipleTrack, hc.Format);
         Assert.AreEqual(4, hc.NumberOfTracks);
         Assert.AreEqual(240, hc.Division);
      }
   }
}
