using System.Collections.Generic;
using UnityEngine;

namespace MVFramework.Contexts
{
    [DefaultExecutionOrder(-1)]
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] protected global::MVFramework.Installer.Installer[] _installersContext;
        [SerializeField] protected List<ScriptableObject> _scriptableObjectsContext;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            GlobalContext.Instance.AddScriptableObjectsToContextDictionary(_scriptableObjectsContext);
            
            foreach (global::MVFramework.Installer.Installer mvcInstaller in _installersContext)
            {
                mvcInstaller.Install(GlobalContext.Instance.Models, GlobalContext.Instance.Controllers, GlobalContext.Instance.Presenters);
            }
        }

        private void OnDestroy()
        {
            GlobalContext.Instance.RemoveScriptableObjectsFromDictionary(_scriptableObjectsContext);
            
            foreach (global::MVFramework.Installer.Installer mvcInstaller in _installersContext)
            {
                mvcInstaller.Uninstall();
            }
        }
    }
}