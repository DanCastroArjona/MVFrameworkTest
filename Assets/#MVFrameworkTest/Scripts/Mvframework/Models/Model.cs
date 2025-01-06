using MVFramework.Contexts;
using MVFramework.Models.Interfaces;
using MVFramework.Signals;
using MVFramework.Signals.Types;
using UnityEngine;

namespace MVFramework.Models
{
    public abstract class Model : IModel
    {
        public T GetScriptable<T>() where T : ScriptableObject
        {
            return (T)GlobalContext.Instance.GetScriptableObject<T>();
        }

        protected void SendUpdateSignal<T>() where T : UpdateSignal, new()
        {
            SignalHub.GetSignal<T>().Dispatch();
        }

        protected void SendUpdateSignal<T, TU>(TU payload) where T : UpdateSignal<TU>, new()
        {
            SignalHub.GetSignal<T>().Dispatch(payload);
        }
    }
}