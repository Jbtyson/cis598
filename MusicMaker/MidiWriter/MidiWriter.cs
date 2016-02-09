using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace MidiWriter
{
   public class MidiWriter
   {
      private const string testFile = "C:\\testMidi.mid";

      public MidiWriter()
      {
         
      }

      public string TestWrite()
      {
         var header = Encoding.ASCII.GetBytes("Mthd");
         var header_length = BitConverter.GetBytes((Int32)20);

         using (var sw = new BinaryWriter(File.OpenWrite(testFile)))
         {
            sw.Write(header);
            sw.Write(header_length);
         }

         return testFile;
      }
   }
}
