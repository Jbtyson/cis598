using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MusicMaker
{
   public partial class MusicViewer : Form
   {
      private string[] noteNames = { "A   ", "A# ", "B   ", "C   ", "C# ", "D   ", "D# ", "E   ", "F   ", "F# ", "G   ", "G# " };
      private int noteIndex;
      private Graphics graphics;
      private int sixteenthNoteWidth = 5;
      private int noteHeight = 6;

      public MusicViewer()
      {
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

      private void DrawNotes()
      {
         var rand = new Random();
         for (int i = 0; i < 100; i++)
         {
            DrawNote(rand.Next(100), rand.Next(100) + 20, rand.Next(16) * 4);
         }
      }

      public void DrawNote(int noteId, int noteBegin, int noteDuration)
      {
         int x1 = noteBegin * sixteenthNoteWidth;
         int y1 = noteId * noteHeight;
         int width = noteDuration * sixteenthNoteWidth;
         var randonGen = new Random();
         var color = Color.FromArgb(randonGen.Next(255), randonGen.Next(255), randonGen.Next(255));
         graphics.FillRectangle(new SolidBrush(color), x1, y1, width, noteHeight);
         graphics.DrawRectangle(new Pen(Color.Black), x1, y1, width, noteHeight);
      }
   }
}
