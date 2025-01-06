using System;
using System.Collections.Generic;
using MVFramework.Controllers.Interfaces;
using MVFramework.Models.Interfaces;
using MVFramework.Presenters.Interfaces;
using MVFramework.Signals;
using MVFramework.Signals.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace MVFramework.Installer
{
    public abstract class Installer : ScriptableObject
    {
        private Dictionary<string, IModel> _models;
        private Dictionary<string, IController> _controllers;
        private Dictionary<string, IPresenter> _presenters;

        private readonly Dictionary<string, IModel> _installedModels = new Dictionary<string, IModel>();
        private readonly Dictionary<string, IController> _installedControllers = new Dictionary<string, IController>();
        private readonly Dictionary<string, IPresenter> _installedPresenters = new Dictionary<string, IPresenter>();
        private readonly List<ISignal> _signals = new List<ISignal>();
        
        public void Install(Dictionary<string, IModel> models, Dictionary<string, IController> controllers, Dictionary<string, IPresenter> presenters)
        {
            _models = models;
            _controllers = controllers;
            _presenters = presenters;
            
            InstallSignals();
            InstallComponents();
        }

        protected void InstallModel(IModel model) 
        {
            Type interfaceType = model.GetType();
            string modelId = interfaceType.Name;
            
            Assert.IsFalse(_models.ContainsKey(modelId));

            _models.Add(modelId, model);
            _installedModels.Add(modelId, model);
        }
        
        protected void InstallController(IController controller)
        {
            Type interfaceType = controller.GetType();
            string controllerId = interfaceType.Name;
            
            Assert.IsFalse(_models.ContainsKey(controllerId));

            _controllers.Add(controllerId, controller);
            _installedControllers.Add(controllerId, controller);
        }
        
        protected void InstallPresenter(IPresenter presenter)
        {
            Type interfaceType = presenter.GetType();
            string presenterId = interfaceType.Name;
            
            Assert.IsFalse(_models.ContainsKey(presenterId));

            _presenters.Add(presenterId, presenter);
            _installedPresenters.Add(presenterId, presenter);
        }

        protected void InstallSignal<T>() where T : Signal, new()
        {
            Signal updateSignal = SignalHub.AddSignal<T>();
            _signals.Add(updateSignal);
        }
        
        protected void InstallSignal<T, TU>() where T : Signal<TU>, new()
        {
            Signal<TU> updateSignal = SignalHub.AddSignal<T>();
            _signals.Add(updateSignal);
        }

        private void UninstallModels()
        {
            foreach (KeyValuePair<string, IModel> model in _installedModels)
            {
                _models.Remove(model.Key);
            }

            _installedModels.Clear();
        }

        private void UninstallControllers()
        {
            foreach (KeyValuePair<string, IController> controller in _installedControllers)
            {
                _controllers.Remove(controller.Key);
            }
            
            _installedControllers.Clear();
        }
        
        private void UninstallPresenters()
        {
            foreach (KeyValuePair<string, IPresenter> presenter in _installedPresenters)
            {
                _controllers.Remove(presenter.Key);
            }
            
            _installedPresenters.Clear();
        }

        private void UninstallSignals()
        {
            foreach (ISignal signal in _signals)
            {
                SignalHub.RemoveSignal(signal);
            }
            
            _signals.Clear();
        }

        protected void UninstallAll()
        {
            UninstallSignals();
            UninstallControllers();
            UninstallPresenters();
            UninstallModels();
        }
        
        protected abstract void InstallSignals();
        protected abstract void InstallComponents();
        public abstract void Uninstall();
    }
}