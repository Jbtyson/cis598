using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicGenerator.Input.Midi
{
   public static class ByteConversionMethods
   {
      public static short ToInt16(this byte[] data, int index)
      {
         var bytes = GetBytes(data, index, sizeof(short));
         return BitConverter.ToInt16(bytes, 0);
      }

      public static int ToInt32(this byte[] data, int index)
      {
         var bytes = GetBytes(data, index, sizeof(int));
         return BitConverter.ToInt32(bytes, 0);
      }

      private static byte[] GetBytes(byte[] data, int index, int size)
      {
         var bytes = new byte[size];
         for (var i = 0; i < size; i++)
         {
            bytes[i] = data[index + i];
         }

         if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);

         return bytes;
      }
   }
}
