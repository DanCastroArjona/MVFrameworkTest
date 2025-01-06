using MVFramework.Models.Interfaces;
using UnityEngine;

namespace MVFramework.Controllers.Interfaces
{
    public interface IController
    {
        T GetModel<T>() where T : class, IModel;
        T GetController<T>() where T : class, IController;
        T GetScriptable<T>() where T : ScriptableObject;
    }
}