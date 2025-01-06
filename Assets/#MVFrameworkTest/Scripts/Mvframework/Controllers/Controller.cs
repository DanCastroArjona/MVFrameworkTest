using System;
using MVFramework.Contexts;
using MVFramework.Controllers.Interfaces;
using MVFramework.Models.Interfaces;
using MVFramework.Signals;
using MVFramework.Signals.Types;
using UnityEngine;

namespace MVFramework.Controllers
{
    public abstract class Controller : IController
    {
        public T GetModel<T>() where T : class, IModel
        {
            return GlobalContext.Instance.GetModel<T>();
        }
        
        public T GetController<T>() where T : class, IController
        {
            return GlobalContext.Instance.GetController<T>();
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