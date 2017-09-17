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
                    Player.Stop();
                    Died = true;
                    Volume.Kill();
                }, KillFadeOutTime));

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
            Player.SetVolume(Volume.Value, Volume.Value);
        }

        public BGMPlayer(int bgmResID)
        {
            Player = Android.Media.MediaPlayer.Create(GameLoop.Context, bgmResID);
            Player.SetVolume(0, 0);
            Player.Start();
        }

        public override void OnPaused()
        {
            Player.Pause();
        }

        public override void OnResume()
        {
            Player.Start();
        }

        public bool IsBeatFrame { get; private set; }

        public float KillFadeOutTime { get; set; } = 3.0f;

        public Android.Media.MediaPlayer Player { get; }
        public Lerper Volume { get; private set; } = new Lerper();
    }
}