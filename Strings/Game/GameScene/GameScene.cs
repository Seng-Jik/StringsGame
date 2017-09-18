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
        }

        

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            Attach(bgm);
            Attach(sideLine);
            bgm.Volume.Value = 1;
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(time);

            time += deltaTime;
            
            for(;now < map.Notes.Count;++now)
            {
                if (map.Notes[now].Begin - time * 1000 > 1000)
                    break;
                else
                {
                    Attach(new ClickPoint(map.Notes[now].Hand, (int)map.Notes[now].ClickPos, map.Notes[now].Begin));
                }
            }
        }

        BeatmapEditor.BeatMap map;
        BGMPlayer bgm;
        SideLine sideLine = new SideLine();

        int now = 0;

        float time = 0;
    }
}