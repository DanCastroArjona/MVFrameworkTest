using System;
using MVFramework.Signals.Interfaces;

namespace MVFramework.Signals
{
    public abstract class Signal : ISignal
    {
        protected Action _callback;
        
        public void AddListener(Action handler)
        {
            _callback += handler;
        }
        
        public void RemoveListener(Action handler)
        {
            _callback -= handler;
        }

        public void Dispatch()
        {
            _callback?.Invoke();
        }
    }

    public abstract class Signal<T>: ISignal
    {
        protected Action<T> _callback;
        
        public void AddListener(Action<T> handler)
        {
            _callback += handler;
        }
        
        public void RemoveListener(Action<T> handler)
        {
            _callback -= handler;
        }
        
        public void Dispatch(T arg1)
        {
            _callback?.Invoke(arg1);
        }
    }
}