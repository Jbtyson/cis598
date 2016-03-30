using System;
using System.Windows.Forms;
using MusicGenerator;
using MidiWriter;

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

         //var test = new MidiWriter.MidiWriter();
         //test.TestWrite();

         var musicController = new MusicController();
         musicController.Init();
         musicController.Generate();
         musicController.Play();

         var mv = new MusicViewer();
         Application.Run(mv);
      }
   }
}
