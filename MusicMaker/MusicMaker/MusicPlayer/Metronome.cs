using System.Diagnostics;

namespace MusicMaker.MusicPlayer
{
   public delegate void OnTick();

   public class Metronome
   {
      public OnTick OnSixteenthTick;

      private int beatsPerMinute;
      private int millisPerBeat;
      private int millisPerSixteenth;
      private Stopwatch stopwatch;
      private long currentMs;
      private bool playing;

      public Metronome()
      {
         stopwatch = new Stopwatch();
      }

      public void SetSpeed(int beatsPerMinute)
      {
         this.beatsPerMinute = beatsPerMinute;
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