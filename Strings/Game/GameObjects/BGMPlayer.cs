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
            player.Stop();
            Died = true;
        }

        public override void OnUpdate(float deltaTime)
        {
            IsBeatFrame = false;
        }

        public BGMPlayer(int bgmResID,float bpm)
        {
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
    }
}