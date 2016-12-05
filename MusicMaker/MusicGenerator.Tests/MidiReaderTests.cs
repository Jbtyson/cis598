using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicGenerator.Input.Midi;

namespace MusicGenerator.Tests
{
   [TestClass]
   public class MidiReaderTests
   {
      [TestMethod]
      public void CanReadVariableLengthValue()
      {
         var data = new byte[] { 0x7f };
         Assert.AreEqual(127, data.ReadVariableLengthValue(0));
         data = new byte[] { 0x81, 0x7f };
         Assert.AreEqual(255, data.ReadVariableLengthValue(0));
         data = new byte[] { 0x82, 0x80, 0x0 };
         Assert.AreEqual(32768, data.ReadVariableLengthValue(0));
      }

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

      [TestMethod]
      public void CanGetTrackChunk()
      {
         var mfr = new MidiFileReader("..\\..\\..\\MusicGenerator\\data\\maryHadALittleLamb.mid");
         var hc = mfr.GetHeaderChunk();
         var tracks = mfr.GetTrackChunks(hc.NumberOfTracks, hc.Size);

         Assert.AreEqual(0x4d54726b, tracks.ElementAt(0).Label);
         Assert.AreEqual(0x4d54726b, tracks.ElementAt(1).Label);
         Assert.AreEqual(0x4d54726b, tracks.ElementAt(2).Label);
         Assert.AreEqual(0x4d54726b, tracks.ElementAt(3).Label);
      }
   }
}
