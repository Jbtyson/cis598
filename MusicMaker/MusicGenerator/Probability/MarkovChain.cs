using System.Collections.Generic;
using System.Linq;
using MusicGenerator.Music;
using System;

namespace MusicGenerator.Probability
{
   public class MarkovChain
   {
      private class OrderPage
      {
         public Dictionary<int, OrderPage> children;
         public int occurences;
      }

      private readonly int order;
      private readonly List<int> notes;
      private Dictionary<int, OrderPage>[] orders;

      public MarkovChain(int order)
      {
         this.order = order;
         this.orders = new Dictionary<int, OrderPage>[order];
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

      public void Create()
      {
         for(var i = 1; i <= order; i++)
         {
            orders[i] = CreateOrder(i);
         }
      }

      private Dictionary<int, OrderPage> CreateOrder(int order)
      {
         var pages = new Dictionary<int, OrderPage>();
         var queue = new Queue<int>();
         var chain = new int[order];
         OrderPage page;

         for (var i = 0; i < notes.Count; i++)
         {
            queue.Enqueue(notes[i]);
            if (queue.Count > order)
               queue.Dequeue();

            queue.CopyTo(chain, 0);

            if(!pages.ContainsKey(chain[0]))
               pages[chain[0]] = new OrderPage();
            page = pages[chain[0]];

            for (var j = 0; j < queue.Count; j++)
            {
               if (!page.children.ContainsKey(chain[j]))
                  page.children[chain[j]] = new OrderPage();
               page = page.children[chain[j]];
            }
            page.occurences++;
         }

         return pages;
      }

      public int GetNext(IEnumerable<int> list)
      {
         var count = order > list.Count() ? list.Count() : order - 1;
         var newList = list.ToList();
         newList.RemoveRange(0, list.Count() - count);

         var pages = orders[order-1];
         var page = pages[newList[0]];
         for (var i = 1; i < count; i++)
         {
            page = page.children[newList[i]];
         }

         return GetRandomValueFromOrderPage(page);
      }

      private static int GetRandomValueFromOrderPage(OrderPage page)
      {
         var list = new List<int>();
         foreach(var p in page.children)
         {
            for(var i = 0; i < p.Value.occurences; i++)
            {
               list.Add(p.Key);
            }
         }

         var r = new Random();
         var num = r.Next(list.Count - 1);
         return list[num];
      }
   }
}