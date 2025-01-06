using MVFramework.Signals;
using MVFramework.Signals.Types;

namespace MVFramework.Views
{
    public abstract class Mediator : View
    {
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