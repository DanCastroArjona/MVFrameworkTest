using System;
using MVFramework.Contexts;
using MVFramework.Models.Interfaces;
using MVFramework.Presenters.Interfaces;
using MVFramework.Signals;
using MVFramework.Signals.Types;
using UnityEngine;

namespace MVFramework.Presenters
{
    public class Presenter: IPresenter
    {
        public T GetModel<T>() where T : class, IModel
        {
            return GlobalContext.Instance.GetModel<T>();
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

        protected void AddUserSignalListener<T>(Action action) where T : UserSignal, new()
        {
            SignalHub.GetSignal<T>().AddListener(action);
        }

        protected void AddUserSignalListener<T, TU>(Action<TU> action) where T : UserSignal<TU>, new()
        {
            SignalHub.GetSignal<T>().AddListener(action);
        }
        
        protected void RemoveUserSignalListener<T>(Action action) where T: UserSignal, new()
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
        
        protected void RemoveUserSignalListener<T, TU>(Action<TU> action) where T : UserSignal<TU>, new()
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
        
        protected void SendUpdateSignal<T>() where T : UpdateSignal, new()
        {
            SignalHub.GetSignal<T>().Dispatch();
        }

        protected void SendUpdateSignal<T, TU>(TU payload) where T : UpdateSignal<TU>, new()
        {
            SignalHub.GetSignal<T>().Dispatch(payload);
        }

#endregion

#region ActionSignals

        protected void AddActionSignalListener<T>(Action action) where T : ActionSignal, new()
        {
            SignalHub.GetSignal<T>().AddListener(action);
        }

        protected void AddActionSignalListener<T, TU>(Action<TU> action) where T : ActionSignal<TU>, new()
        {
            SignalHub.GetSignal<T>().AddListener(action);
        }
        
        protected void RemoveActionSignalListener<T>(Action action) where T: ActionSignal, new()
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
        
        protected void RemoveActionSignalListener<T, TU>(Action<TU> action) where T : ActionSignal<TU>, new()
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
        
        protected void SendActionSignal<T>() where T : ActionSignal, new()
        {
            SignalHub.GetSignal<T>().Dispatch();
        }

        protected void SendActionSignal<T, TU>(TU payload) where T : ActionSignal<TU>, new()
        {
            SignalHub.GetSignal<T>().Dispatch(payload);
        }

#endregion
    }
}