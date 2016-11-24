using System.Collections.Generic;
using System.Linq;
using MusicGenerator.MusicStructure;
using Toub.Sound.Midi;

namespace MusicMaker.MusicPlayer
{
   public class MusicController
   {
      private readonly Metronome metronome;
      private readonly Queue<Note> notesToPlay;
      private readonly Queue<Note> notesToStop;
      private int sixteenthTickCount;

      public MusicController()
      {
         metronome = new Metronome();
         notesToPlay = new Queue<Note>();
         notesToStop = new Queue<Note>();
         metronome.OnSixteenthTick = OnSixteenthTick;
         metronome.SetSpeed(30);
         sixteenthTickCount = 0;
      }

      public void Play(IEnumerable<Note> notes)
      {
         notes.ToList().ForEach(n => notesToPlay.Enqueue(n));
         MidiPlayer.OpenMidi();
         metronome.Start();
      }

      void PlayNote(Note note)
      {
         MidiPlayer.Play(new NoteOn(0, MusicMakerConfig.DefaultChannel, note.Pitch, note.Velocity));
      }

      void StopNote(Note note)
      {
         MidiPlayer.Play(new NoteOff(0, MusicMakerConfig.DefaultChannel, note.Pitch, note.Velocity));
      }

      public void OnSixteenthTick()
      {
         sixteenthTickCount++;

         while (notesToStop.Count > 0 && notesToStop.Peek().EndInterval <= sixteenthTickCount)
         {
            StopNote(notesToStop.Dequeue());
         }

         while (notesToPlay.Count > 0 && notesToPlay.Peek().StartInterval <= sixteenthTickCount)
         {
            var note = notesToPlay.Dequeue();
            PlayNote(note);
            notesToStop.Enqueue(note);
         }
      }
   }
}