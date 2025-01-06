using System;
using System.Collections.Generic;
using MVFramework.Signals.Interfaces;

namespace MVFramework.Signals
{
    internal static class SignalHub
    {
        private static readonly Dictionary<Type, ISignal> _signals = new Dictionary<Type, ISignal>();

        public static SType AddSignal<SType>() where SType : ISignal, new()
        {
            Type signalType = typeof(SType);

            if (_signals.TryGetValue(signalType, out ISignal _))
            {
                throw new Exception($"Signal already exists in signal hub. Type {signalType}");
            }

            return (SType)Bind(signalType);
        }

        public static SType GetSignal<SType>() where SType : new()
        {
            Type signalType = typeof(SType);

            if (_signals.TryGetValue(signalType, out ISignal signal))
            {
                return (SType)signal;
            }

            throw new Exception($"Signal does not exists in signal hub. Type {signalType}");
        }

        public static void RemoveSignal(ISignal signalToRemove)
        {
            Type signalType = signalToRemove.GetType();

            if (_signals.TryGetValue (signalType, out ISignal _))
            {
                _signals.Remove(signalType);
                return;
            }

            throw new Exception($"Signal does not exists in signal hub. Type {signalType}");
        }

        private static ISignal Bind(Type signalType)
        {
            if(_signals.TryGetValue(signalType, out ISignal signal))
            {
                UnityEngine.Debug.LogError($"Signal already registered for type {signalType}");
                return signal;
            }

            signal = (ISignal)Activator.CreateInstance(signalType);
            _signals.Add(signalType, signal);
            return signal;
        }
    }
}