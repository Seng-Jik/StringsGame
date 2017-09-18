using System;
using System.Collections.Generic;

namespace Strings.Engine
{
    public abstract class GameObject
    {
        public virtual void OnUpdate(double deltaTime)
        {
            foreach (var i in ActionsNextFrame)
                i();
            ActionsNextFrame.Clear();
        }

        public virtual void OnDraw() { }
        public virtual void OnTouched(TouchEvent te) { }
        public virtual void OnPaused() { }
        public virtual void OnResume() { }
        public virtual void Kill() { Died = true; }

        public virtual bool Died { get; protected set; } = false;

        public virtual void OnAttached(GameObjectList parent)
        {
            Parent = parent;
        }

        public GameObjectList Parent { get; private set; }
        protected List<Action> ActionsNextFrame { get; } = new List<Action>();
    }
}