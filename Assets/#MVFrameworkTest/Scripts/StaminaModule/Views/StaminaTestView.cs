using _MVFrameworkTest.Scripts.StaminaModule.Controllers;
using MVFramework.Views;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _MVFrameworkTest.Scripts.StaminaModule.Views
{
    /// <summary>
    /// Simple class to add behaviors to UI button presses.
    /// </summary>
    public class StaminaTestView : View
    {
        [SerializeField] private Button _showButton;
        [SerializeField] private Button _hideButton;
        [SerializeField] private Button _runButton;
        [SerializeField] private Button _restButton;
        [SerializeField] private Button _restartButton;

        private StaminaController _staminaController;
        
        private void Awake()
        {
            _staminaController = GetController<StaminaController>();
            Assert.IsNotNull(_staminaController);
            
            AddListeners();
        }

        private void AddListeners()
        {
            Assert.IsNotNull(_showButton);
            _showButton.onClick.AddListener(_staminaController.Show);
            
            Assert.IsNotNull(_hideButton);
            _hideButton.onClick.AddListener(_staminaController.Hide);
            
            Assert.IsNotNull(_runButton);
            _runButton.onClick.AddListener(()=>
            {
                _staminaController.UseStamina(true);
            });
            
            Assert.IsNotNull(_restButton);
            _restButton.onClick.AddListener(() =>
            {
                _staminaController.UseStamina(false);
            });
            
            Assert.IsNotNull(_restartButton);
            _restartButton.onClick.AddListener(()=>
            {
                _staminaController.UseStamina(false);
                _staminaController.Init();
                _staminaController.Show();
            });
        }
    }
}