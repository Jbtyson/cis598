using System;
using System.Windows.Forms;
using MusicGenerator;
using System.Threading;

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
         var notes = musicController.Generate();
         new Thread(delegate () { musicController.Play(); }).Start();

         var mv = new MusicViewer(notes);
         Application.Run(mv);
      }
   }
}
