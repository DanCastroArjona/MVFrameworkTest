using MVFramework.Models.Interfaces;
using UnityEngine;

namespace MVFramework.Presenters.Interfaces
{
    public interface IPresenter
    {
        T GetModel<T>() where T : class, IModel;
        T GetPresenter<T>() where T : class, IPresenter;
        T GetScriptable<T>() where T : ScriptableObject;
    }
}