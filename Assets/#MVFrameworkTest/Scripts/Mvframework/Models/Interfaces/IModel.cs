using UnityEngine;

namespace MVFramework.Models.Interfaces
{
    public interface IModel
    {
        T GetScriptable<T>() where T : ScriptableObject;
    }
}