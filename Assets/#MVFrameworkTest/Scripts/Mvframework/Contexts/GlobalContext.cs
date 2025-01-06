using System;
using System.Collections.Generic;
using MVFramework.Controllers.Interfaces;
using MVFramework.Models.Interfaces;
using MVFramework.Presenters.Interfaces;
using UnityEngine;

namespace MVFramework.Contexts
{
    [DefaultExecutionOrder(-2)]
    public sealed class GlobalContext : MonoBehaviour
    {
#region Singleton
        private static GlobalContext _instance;
        public static GlobalContext Instance => _instance;
#endregion
        
        [SerializeField] private global::MVFramework.Installer.Installer[] _installersContext;
        [SerializeField] private List<ScriptableObject> _scriptableObjectsContext;

        private Dictionary<string, ScriptableObject> _scriptableObjects;
        private Dictionary<string, IModel> _models;
        private Dictionary<string, IController> _controllers;
        private Dictionary<string, IPresenter> _presenters;

        public Dictionary<string, IModel> Models => _models;
        public Dictionary<string, IController> Controllers => _controllers;
        public Dictionary<string, IPresenter> Presenters => _presenters;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
                Initialize();
            }
            else
            {
                Destroy(this);
            }
        }

        private void Initialize()
        {
            _scriptableObjects = new Dictionary<string, ScriptableObject>();
            AddScriptableObjectsToContextDictionary(_scriptableObjectsContext);
            
            _models = new Dictionary<string, IModel>();
            _controllers = new Dictionary<string, IController>();
            _presenters = new Dictionary<string, IPresenter>();

            foreach (global::MVFramework.Installer.Installer installer in _installersContext)
            {
                installer.Install(Models, Controllers, Presenters);
            }
        }

        public void AddScriptableObjectsToContextDictionary(List<ScriptableObject> scriptableObjects)
        {
            foreach (ScriptableObject scriptableObject in scriptableObjects)
            {
                _scriptableObjects.Add(scriptableObject.GetType().ToString(), scriptableObject);
            }
        }

        public void RemoveScriptableObjectsFromDictionary(List<ScriptableObject> scriptableObjects)
        {
            foreach (ScriptableObject scriptableObject in scriptableObjects)
            {
                _scriptableObjects.Remove(scriptableObject.GetType().ToString());
            }
        }

        public T GetModel<T>() where T: class, IModel
        {
            foreach (IModel model in Models.Values)
            {
                if (model is T matchedModel)
                    return matchedModel;
            }

            throw new Exception($"Model not installed: {typeof(T)}");
        }
        
        public T GetController<T>() where T: class, IController
        {
            foreach (IController controller in Controllers.Values)
            {
                if (controller is T matchedController)
                    return matchedController;
            }

            throw new Exception($"Controller not installed: {typeof(T)}");
        }
        
        public T GetPresenter<T>() where T : class, IPresenter
        {
            foreach (IPresenter presenter in Presenters.Values)
            {
                if (presenter is T matchedPresenter)
                    return matchedPresenter;
            }

            throw new Exception($"Presenter not installed: {typeof(T)}");
        }

        public ScriptableObject GetScriptableObject<T>() where T : class
        {
            if (_scriptableObjects.ContainsKey(typeof(T).ToString()))
            {
                return _scriptableObjects[typeof(T).ToString()];
            }

            throw new Exception($"Scriptable Object {typeof(T)} is not installed");
        }
    }
}