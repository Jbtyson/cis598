using System;
using System.Windows.Forms;
using MusicGenerator;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using MusicGenerator.Music;

namespace MusicMaker
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         var musicController = new MusicController();
         musicController.Init();
         var melody = musicController.GenerateMelody();
         var bass = musicController.GenerateBass();

         var notes = new List<Note>();
         melody.ToList().ForEach(m => notes.Add(m));
         bass.ToList().ForEach(b => notes.Add(b));
         notes.Sort((a, b) => a.StartInterval.CompareTo(b.StartInterval));

         new Thread(delegate () { musicController.Play(notes); }).Start();

         var mv = new MusicViewer(notes);
         Application.Run(mv);
      }
   }
}
