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
            thisSong = songID;
            ListenTouchEvent = true;
            var song = SongSelectScene.SongInfo.SongInfo.Songs[songID];
            bgm = new BGMPlayer(song.MusicID);
            map = new BeatmapEditor.BeatMap();
            map.LoadBeatmap(GameLoop.Context.Resources.OpenRawResource(song.NoteID));

            TimeMs = new Stopwatch();
        }

        readonly int thisSong;

        

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            Attach(bgm);
            Attach(sideLine);
            bgm.Volume.Value = 1;
            TimeMs.Start();

            scoreInfo = new ScoreScene.ScoreInfo(thisSong);
        }

        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(deltaTime);

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

            if(now>= map.Notes.Count && !finished)
            {
                finished = true;
                Attach(new Task(Finished, 6));
            }
        }

        void Finished()
        {
            Parent.Attach(new ScoreScene.ScoreScene(scoreInfo));
            Kill();
        }

        public enum NoteClickType
        {
            Perfect,
            Great,
            Good,
            Miss
        }

        public void NoteClicked(NoteClickType type,int handMul)
        {
            int hintID = 0;
            switch(type)
            {
                case NoteClickType.Perfect:
                    hintID = Resource.Raw.perfect;
                    scoreInfo.Perfect++;
                    combo++;
                    break;
                case NoteClickType.Great:
                    hintID = Resource.Raw.great;
                    scoreInfo.Great++;
                    combo++;
                    break;
                case NoteClickType.Good:
                    hintID = Resource.Raw.good;
                    scoreInfo.Good++;
                    combo++;
                    break;
                case NoteClickType.Miss:
                    hintID = Resource.Raw.miss;
                    scoreInfo.Miss++;
                    combo = 0;
                    break;
            }

            if (combo > scoreInfo.MaxCombo) scoreInfo.MaxCombo = combo;
            var hint = new Sprite(hintID);
            Attach(hint);
            hint.Alpha.Value = 1;
            hint.Alpha.Lerp(0.5f, 0);
            hint.KillWhenAlphaIs0 = true;
            hint.Zoom.Value = 2;
            hint.Zoom.Lerp(0.5F, 1);
            hint.PosX.Value = 200 * handMul;
            //hint.Rotate = handMul * -90;
            hint.Kill();
        }

        public override void OnPaused()
        {
            base.OnPaused();
            TimeMs.Stop();
        }

        public override void OnResume()
        {
            base.OnResume();
            TimeMs.Start();
        }

        bool finished = false;

        BeatmapEditor.BeatMap map;
        BGMPlayer bgm;
        SideLine sideLine = new SideLine();

        int now = 0;

        double Time { get; set; } = 0;
        public Stopwatch TimeMs { get; } = new Stopwatch();

        ScoreScene.ScoreInfo scoreInfo;

        int combo = 0;
    }
}