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
using OpenTK;

namespace Strings.Game.TitleScene
{
    class StarDrawer : GameObject
    {
        static StarDrawer()
        {
            var p = new Vector2[5];

            for(int i = 0;i < 5;++i)
            {
                var rot = i / 5.0f * Math.PI * 2.0F;
                p[i] = new Vector2((float)Math.Sin(rot), (float)Math.Cos(rot));
                p[i] *= 200.0F;
            }

            List<Vector2> mesh = new List<Vector2>
            {
                p[0],
                p[2],
                p[3],
                p[0],

                p[4],
                p[1],
                p[1],
                p[3],

                p[2],
                p[4]
            };

            starMesh = mesh.ToArray();
        }

        public override bool Died
        {
            get => base.Died && starProcess.Value >= 1;
        }

        public override void Kill()
        {
            base.Kill();
            buildNewStar = false;
            starProcess.Kill();
        }

        public override void OnAttached(GameObjectList parent)
        {
            base.OnAttached(parent);
            parent.Attach(starProcess);
            parent.Attach(new Task(() => { if (buildNewStar) parent.Attach(new StarDrawer()); }, 1));
            starProcess.Value = -1;
            starProcess.Lerp(5, 1);
        }

        public override void OnUpdate(double deltaTime)
        {
            base.OnUpdate(deltaTime);
            if (starProcess.Value >= 1) Kill();

            var speed = 1.5F - ((starProcess.Value + 1.0f) / 2.0f);
            rot += deltaTime * speed * 16.0F;
        }

        public override void OnDraw()
        {
            base.OnDraw();
            var starSize = (starProcess.Value + 1.0f) / 2.0f;
            if(starProcess.Value > 0)
                Renderer.DrawLines(starMesh, (float)rot, starSize * 24.0F);
            else
            {
                Vector2[] myMesh = (Vector2[])starMesh.Clone();
                for(int i = 0;i < 5; ++i)
                {
                    var len = myMesh[2 * i + 1] - myMesh[2 * i];
                    len *= 1-(-starProcess.Value);
                    myMesh[2 * i + 1] = myMesh[2 * i] + len;
                }
                Renderer.DrawLines(myMesh, (float)rot, starSize * 24.0F);
            }
        }

        bool buildNewStar = true;
        Lerper starProcess = new Lerper();

        double rot = 0;

        static Vector2[] starMesh;
    }
}