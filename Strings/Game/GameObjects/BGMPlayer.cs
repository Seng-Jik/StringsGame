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

namespace Strings.Game.GameObjects
{
    class BGMPlayer : GameObject
    {
        public override void Kill()
        {
            Parent.Attach(new Task(
                () =>
                {
                    player.Stop();
                    Died = true;
                    Volume.Kill();
                }, 3));

            Volume.Lerp(3, 0);

        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            parent.Attach(Volume);
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            IsBeatFrame = false;
            player.SetVolume(Volume.Value, Volume.Value);
        }

        public BGMPlayer(int bgmResID,float bpm)
        {
            Volume.Value = 1.0f;
            player = Android.Media.MediaPlayer.Create(GameLoop.Context, bgmResID);
            player.Start();
        }

        public override void OnPaused()
        {
            player.Pause();
        }

        public override void OnResume()
        {
            player.Start();
        }

        public bool IsBeatFrame { get; private set; }

        Android.Media.MediaPlayer player;
        public Lerper Volume { get; private set; } = new Lerper();
    }
}