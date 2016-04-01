using System.Collections.Generic;
using System.Security.Policy;
using MusicGenerator.Music;

namespace MusicGenerator.MusicStructure
{
   public class Chord
   {
      public enum Inversion
      {
         None,
         First,
         Second,
         Third
      }

      public enum Variation
      {
         Major,
         Minor,
         Diminshed,
         Suspended,
         Seventh,
         MinorSeventh
      }

      public enum Interval
      {
         PerfectUnison = 0,
         MinorSecond,
         MajorSecond,
         MinorThird,
         MajorThird,
         PerfectFourth,
         DiminishedFifth,
         PerfectFifth,
         MinorSixth,
         MajorSixth,
         MinorSeventh,
         MajorSeventh,
         PerfectOctave
      }

      public List<Note> Notes;
      public Note Root;
      public Variation ChordVariation;
      public Inversion ChordInversion;

      public Chord(Note root)
      {
         Notes = new List<Note>();
         Root = root;
      }

      public void Add(Note note)
      {
         Notes.Add(note);
      }

      public void AddInterval(Interval interval)
      {
      }
   }
}
