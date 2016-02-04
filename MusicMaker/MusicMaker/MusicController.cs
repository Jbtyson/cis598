using System;
using Toub.Sound.Midi;
using MusicMaker.Music;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace MusicMaker
{
    public class MusicController
    {
        private readonly Metronome metronome;
        private Note testNote;
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
            Generate();
        }

        public void Generate()
        {
            notesToPlay.Enqueue(new Note(Pitch.C4, 16, NoteLength.Quarter));
            notesToPlay.Enqueue(new Note(Pitch.D4, 20, NoteLength.Quarter));
            notesToPlay.Enqueue(new Note(Pitch.E4, 24, NoteLength.Eigth));
            notesToPlay.Enqueue(new Note(Pitch.F4, 26, NoteLength.Eigth));
            notesToPlay.Enqueue(new Note(Pitch.G4, 32, NoteLength.Quarter));
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
