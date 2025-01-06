using MVFramework.Controllers.Interfaces;
using UnityEngine;

namespace MVFramework.Views.Interfaces
{
    public interface IView
    {
        T GetController<T>() where T : class, IController;
        T GetScriptable<T>() where T : ScriptableObject;
    }
}