using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BeatmapEditor
{
    public class BeatMap
    {
        public BeatMap()
        {
            Notes = new List<Note>();
        }

        public void LoadBeatmap(Stream beatmap)
        {
            var bm = new StreamReader(beatmap);

            Notes = new List<Note>();
            while(true)
            {
                if (bm.EndOfStream)
                    break;
                string noteLine = bm.ReadLine();
                if (noteLine == string.Empty) continue;
                var noteInfo = noteLine.Split(',');

                Note n = new Note()
                {
                    Type = (Note.TypeEnum)int.Parse(noteInfo[0]),
                    Hand = (Note.HandEnum)int.Parse(noteInfo[1]),
                    Begin = uint.Parse(noteInfo[2]),
                    Arg = uint.Parse(noteInfo[3]),
                    ClickPos = uint.Parse(noteInfo[4])
                };

                Notes.Add(n);
            }

            SortNotes();
        }

        public void SaveBeatmap(Stream beatmap)
        {
            throw new Exception("Error");
        }

        public void SortNotes()
        {
            Notes.Sort(nc);
        }

        public struct Note
        {
            public enum TypeEnum
            {
                Short = 0,
                Long = 1,
                ResetBpm = 2
            }
            public enum HandEnum
            {
                Left = 0,
                Right = 1
            }

            public TypeEnum Type;
            public HandEnum Hand;
            public uint Begin, Arg, ClickPos;
        }
        public List<Note> Notes { get; private set; }

        class NoteComparer : IComparer<Note>
        {
            public int Compare(Note x, Note y)
            {
                return (int)x.Begin - (int)y.Begin;
            }
        }

        static NoteComparer nc = new NoteComparer();

    }
}
