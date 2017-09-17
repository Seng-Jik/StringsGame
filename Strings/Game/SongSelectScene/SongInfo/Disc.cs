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

namespace Strings.Game.SongSelectScene.SongInfo
{
    class Disc : GameObject
    {
        public Disc(int songID)
        {
            var song = SongInfo.Songs[songID];
            disc = new Sprite(song.CoverID);
            bgm = new BGMPlayer(song.MusicID)
            {
                KillFadeOutTime = 0.5F
            };

            bgm.Volume.Value = 0;
            bgm.Player.SeekTo(40 * 1000);
            bgm.Player.Looping = true;
            bgm.Volume.Lerp(0.5f, 1);
        }

        public override void OnAttached(GameObjectList parent)
        {
            parent.Attach(disc);
            disc.Zoom.Value = 0;
            disc.Zoom.Lerp(1, 1.5F);
            disc.Alpha.Value = 0;
            disc.Alpha.Lerp(1, 1);
            disc.PosX.Func = x => (float)Math.Sin(System.Math.PI / 2 * x);

            parent.Attach(new Task(() => disc.PosX.Lerp(0.5F, 400), 0.5f));

            parent.Attach(bgm);

            base.OnAttached(parent);
        }

        public override void Kill()
        {
            base.Kill();
            disc.KillWhenAlphaIs0 = true;
            disc.Alpha.Lerp(0.25f, 0);
            disc.Kill();
        }

        public override void OnUpdate(float time)
        {
            base.OnUpdate(time);

            if(disc != null)
                disc.Rotate += time * 16;
        }

        public void SetSong(int songID)
        {
            var song = SongInfo.Songs[songID];

            var oldDisc = disc;
            disc = new Sprite(song.CoverID);
            disc.PosX.Value = oldDisc.PosX.Value;
            disc.PosY.Value = oldDisc.PosY.Value;
            disc.Zoom.Value = oldDisc.Zoom.Value;
            disc.Rotate = oldDisc.Rotate;

            disc.Alpha.Value = 0;
            disc.Alpha.Lerp(0.25f, 1);
            Parent.Attach(new Task(() => oldDisc.Kill(), 0.25f));
            Parent.Attach(disc);

            bgm.Kill();
            bgm = new BGMPlayer(song.MusicID)
            {
                KillFadeOutTime = 0.5F
            };
            bgm.Volume.Value = 0;
            bgm.Player.SeekTo(40 * 1000);
            bgm.Player.Looping = true;
            bgm.Volume.Lerp(0.5f, 1);

            Parent.Attach(bgm);
        }

        public void EntryEffectKill()
        {
            bgm.Kill();

            Task kill = new Task(() =>
            {
                base.Kill();
                disc.KillWhenAlphaIs0 = false;
                disc.Kill();
            },2.1f);

            disc.PosX.Lerp(1, 0);

            Task fadeOut = new Task(() =>
            {
                disc.Zoom.Func = p => -p * p + 2 * p;
                disc.Zoom.Lerp(1, 35);
            },1.1f);

            Parent.Attach(fadeOut);
            Parent.Attach(kill);
        }

        Sprite disc;
        BGMPlayer bgm;
    }
}