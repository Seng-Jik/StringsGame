namespace Strings.Engine
{
    public abstract class GameObject
    {
        public virtual void OnUpdate(float deltaTime) { }
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
    }
}