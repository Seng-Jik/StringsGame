using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Strings.Engine;
using Strings.Game.GameObjects;
using System.Diagnostics;

namespace Strings.Game.GameScene
{
    class GameScene : GameObjectList
    {
        public GameScene(int songID)
        {
            var song = SongSelectScene.SongInfo.SongInfo.Songs[songID];
            bgm = new BGMPlayer(song.MusicID);
            map = new BeatmapEditor.BeatMap();
            map.LoadBeatmap(GameLoop.Context.Resources.OpenRawResource(song.NoteID));

            TimeMs = new Stopwatch();
        }

        

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            Attach(bgm);
            Attach(sideLine);
            bgm.Volume.Value = 1;
            TimeMs.Start();
        }

        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(Time);

            Time += deltaTime;
            
            for(;now < map.Notes.Count;++now)
            {
                if (TimeMs.ElapsedMilliseconds + 2000 > map.Notes[now].Begin)
                {
                    var n = map.Notes[now];
                    Attach(new ClickPoint(n.Hand, (int)n.ClickPos, n.Begin));
                }
                else break;
            }
        }

        BeatmapEditor.BeatMap map;
        BGMPlayer bgm;
        SideLine sideLine = new SideLine();

        int now = 0;

        double Time { get; set; } = 0;
        public Stopwatch TimeMs { get; } = new Stopwatch();

    }
}