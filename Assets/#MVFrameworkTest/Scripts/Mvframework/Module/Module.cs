using System;
using MVFramework.Contexts;
using MVFramework.Controllers.Interfaces;
using MVFramework.Module.Interfaces;
using MVFramework.Presenters.Interfaces;
using MVFramework.Signals;
using UnityEngine;

namespace MVFramework.Module
{
    public abstract class Module : IModule
    {
        public T GetScriptable<T>() where T : ScriptableObject
        {
            return (T)GlobalContext.Instance.GetScriptableObject<T>();
        }

        public T GetController<T>() where T : class, IController
        {
            return GlobalContext.Instance.GetController<T>();
        }

        public T GetPresenter<T>() where T : class, IPresenter
        {
            return GlobalContext.Instance.GetPresenter<T>();
        }
        
        protected void SendSignal<T>() where T : Signal, new()
        {
            SignalHub.GetSignal<T>().Dispatch();
        }

        protected void SendSignal<T, TU>(TU payload) where T : Signal<TU>, new()
        {
            SignalHub.GetSignal<T>().Dispatch(payload);
        }
        
        protected void AddSignalListener<T>(Action action) where T : Signal, new()
        {
            SignalHub.GetSignal<T>().AddListener(action);
        }

        protected void AddSignalListener<T, TU>(Action<TU> action) where T : Signal<TU>, new()
        {
            SignalHub.GetSignal<T>().AddListener(action);
        }
        
        protected void RemoveSignalListener<T>(Action action) where T: Signal, new()
        {
            try
            {
                SignalHub.GetSignal<T>().RemoveListener(action);
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"{typeof(T)} has been destroyed already. {exception}");
            }
        }
        
        protected void RemoveSignalListener<T, TU>(Action<TU> action) where T : Signal<TU>, new()
        {
            try
            {
                SignalHub.GetSignal<T>().RemoveListener(action);
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"{typeof(T)} has been destroyed already. {exception}");
            }
        }
    }
}