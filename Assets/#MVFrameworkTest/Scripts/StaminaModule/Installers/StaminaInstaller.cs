using _MVFrameworkTest.Scripts.StaminaModule.Controllers;
using _MVFrameworkTest.Scripts.StaminaModule.Models;
using _MVFrameworkTest.Scripts.StaminaModule.Signals;
using MVFramework.Installer;
using UnityEngine;

namespace _MVFrameworkTest.Scripts.StaminaModule.Installers
{
    [CreateAssetMenu(fileName = "StaminaInstaller", menuName = "Mvc/Installer/Stamina")]
    public class StaminaInstaller : Installer
    {
        protected override void InstallSignals()
        {
            InstallSignal<InitStaminaSignal>();
            InstallSignal<ShowStaminaSignal>();
            InstallSignal<HideStaminaSignal>();
            InstallSignal<WithoutStaminaSignal>();
            InstallSignal<DeathStaminaSignal>();
            InstallSignal<UseStaminaSignal, bool>();
        }

        protected override void InstallComponents()
        {
            StaminaModel model = new StaminaModel();
            StaminaController controller = new StaminaController(model);
            
            InstallModel(model);
            InstallController(controller);
        }

        public override void Uninstall()
        {
            UninstallAll();
        }
    }
}