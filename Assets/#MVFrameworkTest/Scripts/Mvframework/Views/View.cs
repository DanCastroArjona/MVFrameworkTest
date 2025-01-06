using System;
using MVFramework.Contexts;
using MVFramework.Controllers.Interfaces;
using MVFramework.Presenters.Interfaces;
using MVFramework.Signals;
using MVFramework.Signals.Types;
using MVFramework.Views.Interfaces;
using UnityEngine;

namespace MVFramework.Views
{
    public abstract class View : MonoBehaviour, IView
    {
        public T GetController<T>() where T : class, IController
        {
            return GlobalContext.Instance.GetController<T>();
        }
        
        public T GetPresenter<T>() where T : class, IPresenter
        {
            return GlobalContext.Instance.GetPresenter<T>();
        }

        public T GetScriptable<T>() where T : ScriptableObject
        {
            return (T)GlobalContext.Instance.GetScriptableObject<T>();
        }

#region UserSignals

        protected void SendUserSignal<T>() where T : UserSignal, new()
        {
            SignalHub.GetSignal<T>().Dispatch();
        }

        protected void SendUserSignal<T, TU>(TU payload) where T : UserSignal<TU>, new()
        {
            SignalHub.GetSignal<T>().Dispatch(payload);
        }

#endregion

#region UpdateSignals

        protected void AddUpdateSignalListener<T>(Action action) where T : UpdateSignal, new()
        {
            SignalHub.GetSignal<T>().AddListener(action);
        }

        protected void AddUpdateSignalListener<T, TU>(Action<TU> action) where T : UpdateSignal<TU>, new()
        {
            SignalHub.GetSignal<T>().AddListener(action);
        }
                
        protected void RemoveUpdateSignalListener<T>(Action action) where T: UpdateSignal, new()
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
                
        protected void RemoveUpdateSignalListener<T, TU>(Action<TU> action) where T : UpdateSignal<TU>, new()
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

#endregion
        
        
    }
}