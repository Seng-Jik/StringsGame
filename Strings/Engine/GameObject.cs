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

namespace Strings.Engine
{
    public abstract class GameObject
    {
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnDraw();
        public abstract void Kill();

        public virtual bool Died { get; protected set; } = false;

        internal void OnAttached(GameObject parent)
        {
            Parent = parent;
        }

        public GameObject Parent { get; private set; }
    }
}