using MVFramework.Controllers.Interfaces;
using MVFramework.Presenters.Interfaces;
using UnityEngine;

namespace MVFramework.Module.Interfaces
{
    public interface IModule
    {
        T GetController<T>() where T : class, IController;
        T GetPresenter<T>() where T : class, IPresenter;
        T GetScriptable<T>() where T : ScriptableObject;
    }
}