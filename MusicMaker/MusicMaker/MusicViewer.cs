using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MusicGenerator.MusicStructure;

namespace MusicMaker
{
   public partial class MusicViewer : Form
   {
      private const int sixteenthNoteWidth = 5;
      private const int noteHeight = 6;
      private readonly string[] noteNames = { "A   ", "A# ", "B   ", "C   ", "C# ", "D   ", "D# ", "E   ", "F   ", "F# ", "G   ", "G# " };
      private readonly IEnumerable<Note> notes;
      private int noteIndex;
      private Graphics graphics;

      public MusicViewer(IEnumerable<Note> notes)
      {
         this.notes = notes;
         InitializeComponent();
      }

      private void canvas_Paint(object sender, PaintEventArgs e)
      {
         graphics = e.Graphics;

         e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
         RenderKeyboard(e.Graphics);
      }

      private void RenderKeyboard(Graphics g)
      {
         for (int i = 0; i < 88; i++)
         {
            var y = i * noteHeight;
            g.DrawLine(new Pen(Color.Black), 0, y, 2000, y);
            var font = new Font(FontFamily.GenericSansSerif, 5);
            g.DrawString(GetNoteName(i), font, new SolidBrush(Color.Black), 0, y);
         }

         DrawNotes();
      }

      private string GetNoteName(int i)
      {
         if (++noteIndex > 11)
            noteIndex = 0;

         return noteNames[noteIndex] + i / 11;
      }

      public void DrawNotes()
      {
         var randomGen = new Random();
         var color = Color.FromArgb(randomGen.Next(255), randomGen.Next(255), randomGen.Next(255));

         foreach (var note in notes)
         {
            DrawNote(note.NoteId, note.StartInterval, note.EndInterval - note.StartInterval, color);
         }
      }

      public void DrawNote(int noteId, int noteBegin, int noteDuration, Color color)
      {
         int x1 = noteBegin * sixteenthNoteWidth;
         int y1 = noteId * noteHeight;
         int width = noteDuration * sixteenthNoteWidth;
         graphics.FillRectangle(new SolidBrush(color), x1, y1, width, noteHeight);
         graphics.DrawRectangle(new Pen(Color.Black), x1, y1, width, noteHeight);
      }
   }
}
