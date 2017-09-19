using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace Mapper
{
    class Program
    {
        struct Note
        {
            public int Hand;
            public int ClickPos;
            public long ms;
        }

        [STAThreadAttribute]
        static void Main(string[] args)
        {
            var i = new OpenFileDialog();
            i.ShowDialog();

            var m = new SoundPlayer(i.FileName);
            var s = new Stopwatch();

            Console.ReadKey();
            m.Play();
            s.Start();

            var f = File.Open("notes.csv", FileMode.Create);
            var fs = new StreamWriter(f);

            while (true)
            {
                var k = Console.ReadKey().KeyChar;
                if (k == 'Q') break;

                Note t = new Note()
                {
                    Hand = k == 'A' || k == 'S' || k == 'D' || k == 'F' ? 0 : 1,
                    ms = s.ElapsedMilliseconds - 75
                };

                if (k == 'A' || k == 'J') t.ClickPos = 0;
                else if (k == 'S' || k == 'K') t.ClickPos = 1;
                else if (k == 'D' || k == 'L') t.ClickPos = 2;
                else if (k == 'F' || k == ';') t.ClickPos = 3;

                string ts = "0," + t.Hand + "," + t.ms + ",0," + t.ClickPos;
                Console.WriteLine(ts);
                fs.WriteLine(ts);
            }

            fs.Close();
        }
    }
}
