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
            for (int i = 0; i < objList.Count; ++i)
                objList[i].OnUpdate(time);

            objList.RemoveAll(x => x.Died);
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
            objList.Add(obj);
        }

        public bool ListenTouchEvent { get; set; }

        List<GameObject> objList = new List<GameObject>();
        bool killed = false;
    }
}