using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MusicGenerator.Music;

namespace MusicGenerator.Probability
{
   public class MarkovChain
   {
      private readonly int order;
      private readonly List<int> notes;
      private Dictionary<string, int> noteOccurences;
      private OrderPage[] pages;

      public MarkovChain(int order)
      {
         this.order = order;
         pages = new OrderPage[order];
      }

      public void AddNoteId(int noteId)
      {
         notes.Add(noteId);
      }

      public void AddNote(Note note)
      {
         notes.Add(note.NoteId);
      }

      public void AddNoteList(IEnumerable<Note> list)
      {
         list.ToList().ForEach(n => notes.Add(n.NoteId));
      }

      private int CountUniqueNotes()
      {
         var noteOccurences = new Dictionary<int, int>();
         foreach (var n in notes)
         {
            if (noteOccurences.ContainsKey(n))
               noteOccurences[n] = noteOccurences[n] + 1;
            else
               noteOccurences[n] = 0;
         }

         return noteOccurences.Keys.Count;
      }

      public void Create()
      {
         var queue = new Queue<string>();
         for (var i = 0; i < notes.Count; i++)
         {
            var noteCode = notes[i].ToString("###");
            queue.Enqueue(noteCode);
            if (queue.Count > order)
               queue.Dequeue();

            for (var j = 0; j < queue.Count; j++)
            {
               
            }

         }
      }

      private class OrderPage
      {
         public OrderPage ChildPage;
         public Dictionary<int, int> values;
      }
   }
}
