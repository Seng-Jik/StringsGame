using System.Collections.Generic;

namespace Strings.Engine
{
    public class GameObjectList : GameObject
    {
        public override bool Died =>
            killed && objList.Count == 0;

        public override void OnDraw()
        {
            foreach(var i in objList)
                i.OnDraw();
        }

        public override void Kill()
        {
            foreach (var i in objList)
                i.Kill();
            killed = true;
        }

        public override void OnUpdate(float time)
        {
            base.OnUpdate(time);
            for (int i = 0; i < objList.Count; ++i)
                objList[i].OnUpdate(time);

            objList.RemoveAll(x => x.Died);

            if (objList.Count == 0 && KillSelfWhenEmpty) Kill();
        }

        public override void OnTouched(TouchEvent te)
        {
            if (ListenTouchEvent)
            {
                foreach (var i in objList)
                    i.OnTouched(te);
            }
        }

        public override void OnPaused()
        {
            foreach (var i in objList)
                i.OnPaused();
        }

        public override void OnResume()
        {
            foreach (var i in objList)
                i.OnResume();
        }

        public void Attach(GameObject obj)
        {
            obj.OnAttached(this);
            ActionsNextFrame.Add( () =>
                 objList.Add(obj));
        }

        public bool KillSelfWhenEmpty { get; protected set; } = false;

        public bool ListenTouchEvent { get; set; } = true;

        List<GameObject> objList = new List<GameObject>();
        bool killed = false;
    }
}