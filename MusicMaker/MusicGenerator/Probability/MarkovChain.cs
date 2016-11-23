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

         public OrderPage()
         {
            children = new Dictionary<int, OrderPage>();
         }
      }

      private readonly int order;
      private readonly List<int> values;
      private Dictionary<int, OrderPage>[] orders;
      private readonly Random random;

      public MarkovChain(int order)
      {
         this.order = order;

         values = new List<int>();
         orders = new Dictionary<int, OrderPage>[order];
         random = new Random((int)DateTime.Now.Ticks);
      }

      public void Add(int num)
      {
         values.Add(num);
      }

      public void AddNote(Note note)
      {
         values.Add(note.NoteId);
      }

      public void AddList(IEnumerable<int> list)
      {
         list.ToList().ForEach(l => values.Add(l));
      }

      public void AddNoteList(IEnumerable<Note> list)
      {
         list.ToList().ForEach(n => values.Add(n.NoteId));
      }

      public void Create()
      {
         for(var i = 0; i < order; i++)
         {
            orders[i] = CreateOrder(i + 1);
         }
      }

      private Dictionary<int, OrderPage> CreateOrder(int order)
      {
         var pages = new Dictionary<int, OrderPage>();
         var queue = new Queue<int>();
         var chain = new int[order];
         OrderPage page;

         for(var i = 0; i < order - 1; i++)
         {
            queue.Enqueue(values[i]);
         }

         for (var i = order - 1; i < values.Count; i++)
         {
            queue.Enqueue(values[i]);
            if (queue.Count > order)
               queue.Dequeue();

            queue.CopyTo(chain, 0);

            if(!pages.ContainsKey(chain[0]))
               pages[chain[0]] = new OrderPage();
            page = pages[chain[0]];

            for (var j = 1; j < queue.Count; j++)
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

         var pages = orders[count];
         OrderPage page;
         if (newList.Count > 0)
         {
            if (!pages.ContainsKey(newList[0]))
            {
               newList.RemoveRange(0, 1);
               return GetNext(newList);
            }

            page = pages[newList[0]];
            for (var i = 1; i < count; i++)
            {
               if (!page.children.ContainsKey(newList[i]))
               {
                  newList.RemoveRange(0, 1);
                  return GetNext(newList);
               }

               page = page.children[newList[i]];
            }
         }
         else
         {
            page = new OrderPage
            {
               children = pages
            };
         }

         return GetRandomValueFromOrderPage(page);
      }

      private int GetRandomValueFromOrderPage(OrderPage page)
      {
         var list = new List<int>();
         foreach(var p in page.children)
         {
            for(var i = 0; i < p.Value.occurences; i++)
            {
               list.Add(p.Key);
            }
         }

         var num = random.Next(list.Count - 1);
         return list[num];
      }
   }
}