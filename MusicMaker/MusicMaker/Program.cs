using System;
using System.Windows.Forms;
using System.Threading;
using MusicGenerator;
using MusicGenerator.Input.Midi;
using MusicMaker.MusicPlayer;

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

         var musicCreator = new MusicCreator(5);
         var melody = musicCreator.GenerateNotesFromFile("..\\..\\..\\MusicGenerator\\data\\odeToJoyMelody.csv");
         var bass = musicCreator.GenerateNotesFromFile("..\\..\\..\\MusicGenerator\\data\\odeToJoyBass.csv");
         var notes = musicCreator.MergeNoteLists(melody, bass);

         //var mfr = new MidiFileReader("..\\..\\..\\MusicGenerator\\data\\maryHadALittleLamb.mid");
         var mfr = new MidiFileReader("..\\..\\..\\MusicGenerator\\data\\happy_birthday.mid");
         notes = mfr.GetNotes();

         var musicController = new MusicController();
         new Thread(delegate () { musicController.Play(notes); }).Start();

         var mv = new MusicViewer(notes);
         Application.Run(mv);
      }
   }
}
