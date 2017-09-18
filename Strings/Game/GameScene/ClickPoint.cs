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
using OpenTK;

namespace Strings.Game.GameScene
{
    class ClickPoint : GameObject
    {
        public ClickPoint(BeatmapEditor.BeatMap.Note.HandEnum hand,int clickPos, uint ms)
        {
            this.ms = ms;
            handMul = hand == BeatmapEditor.BeatMap.Note.HandEnum.Left ? -1 : 1;
            switch (clickPos)
            {
                case 0:
                    heightTop = 640;
                    heightBottom = 320;
                    break;
                case 1:
                    heightTop = 320;
                    heightBottom = 0;
                    break;
                case 2:
                    heightTop = 0;
                    heightBottom = -320;
                    break;
                case 3:
                    heightTop = -320;
                    heightBottom = -640;
                    break;
            }
        }

        public override void OnDraw()
        {
            base.OnDraw();

            float progress = ms / 1000.0f;
            float x = progress * 800 * handMul;

            line[0] = new Vector2(x, heightTop);
            line[1] = new Vector2(x, heightBottom);

            Renderer.DrawLines(line);
        }

        Vector2[] line = new Vector2[2];
        float heightTop, heightBottom;
        uint ms;
        int handMul;
    }
}