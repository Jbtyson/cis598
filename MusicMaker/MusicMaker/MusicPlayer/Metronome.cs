using System.Diagnostics;

namespace MusicMaker.MusicPlayer
{
   public delegate void OnTick();

   public class Metronome
   {
      private readonly Stopwatch stopwatch;
      private int millisPerBeat;
      private int millisPerSixteenth;
      private long currentMs;
      private bool playing;

      public OnTick OnSixteenthTick;

      public Metronome()
      {
         stopwatch = new Stopwatch();
      }

      public void SetSpeed(int beatsPerMinute)
      {
         millisPerBeat = 60000 / beatsPerMinute;
         millisPerSixteenth = millisPerBeat / 16;
      }

      public void Start()
      {
         playing = true;
         stopwatch.Start();
         currentMs = stopwatch.ElapsedMilliseconds;
         OnSixteenthTick();
         Loop();
      }

      public void Loop()
      {
         while (playing)
         {
            if (stopwatch.ElapsedMilliseconds - currentMs >= millisPerSixteenth)
            {
               currentMs = stopwatch.ElapsedMilliseconds;
               OnSixteenthTick();
            }
         }
      }

      public void Stop()
      {
         playing = false;
      }
   }
}