using System;

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

      public static int ReadVariableLengthValue(this byte[] data, int index, out int bytesRead)
      {
         var binaryStr = "";
         var firstBitCleared = false;
         bytesRead = 0;
         while (!firstBitCleared)
         {
            if ((data[index + bytesRead] & 0x80) == 0)
               firstBitCleared = true;
            binaryStr += Convert.ToString(data[index + bytesRead++], 2).PadLeft(8, '0').Substring(1);
         }

         return Convert.ToInt32(binaryStr.PadLeft(32, '0'), 2);
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

      public static byte[] CopyRange(this byte[] data, int index, int size)
      {
         var copy = new byte[size];
         for (var i = 0; i < size; i++)
         {
            copy[i] = data[index + i];
         }
         return copy;
      }
   }
}
