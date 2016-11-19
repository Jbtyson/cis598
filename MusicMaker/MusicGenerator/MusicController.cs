using Toub.Sound.Midi;
using MusicGenerator.Music;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MusicGenerator.Input;
using MusicGenerator.MusicStructure;
using MusicGenerator.Probability;

namespace MusicGenerator
{
   public class MusicController
   {
      private readonly Metronome metronome;
      private Queue<Note> notesToPlay;
      private Queue<Note> notesToStop;
      private int sixteenthTickCount;


      public MusicController()
      {
         metronome = new Metronome();
         notesToPlay = new Queue<Note>();
         notesToStop = new Queue<Note>();
      }

      public void Init()
      {
         metronome.OnSixteenthTick = OnSixteenthTick;
         metronome.SetSpeed(60);
         sixteenthTickCount = 0;
      }

      public void Generate()
      {
         //notesToPlay.Enqueue(new Note("C4", 16, NoteLength.Quarter));
         //notesToPlay.Enqueue(new Note("D4", 20, NoteLength.Quarter));
         //notesToPlay.Enqueue(new Note("E4", 24, NoteLength.Eigth));
         //notesToPlay.Enqueue(new Note("F4", 26, NoteLength.Eigth));
         //notesToPlay.Enqueue(new Note("G4", 32, NoteLength.Half));

         var motifGen = Loader.LoadMotifMatrix();
         motifGen.SetKey(Note.GetNoteId("C4"), Scale.Major);
         //var notes = motifGen.GenerateMotif(100).ToList();
         //notes.ForEach(notesToPlay.Enqueue);

         var midiCsvReader = new MidiCsvReader();
         var trainingDataNotes = midiCsvReader.ConvertFileToNoteList("..\\..\\..\\MusicGenerator\\data\\training.csv");
      }

      public void Play()
      {
         MidiPlayer.OpenMidi();
         metronome.Start();
      }

      public void OnSixteenthTick()
      {
         sixteenthTickCount++;

         if (notesToPlay.Count > 0 && notesToPlay.Peek().StartInterval <= sixteenthTickCount)
         {
            var note = notesToPlay.Dequeue();
            note.Play();
            notesToStop.Enqueue(note);
         }

         if (notesToStop.Count > 0 && notesToStop.Peek().EndInterval <= sixteenthTickCount)
         {
            notesToStop.Dequeue().Stop();
         }
      }
   }
}
