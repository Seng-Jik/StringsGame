namespace Strings.Engine
{
    public abstract class GameObject
    {
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnDraw();
        public virtual void OnTouched(TouchEvent te) { }
        public virtual void OnPaused() { }
        public virtual void OnResume() { }
        public abstract void Kill();

        public virtual bool Died { get; protected set; } = false;

        internal void OnAttached(GameObject parent)
        {
            Parent = parent;
        }

        public GameObject Parent { get; private set; }
    }
}