using _MVFrameworkTest.Scripts.StaminaModule.Signals;
using MVFramework.Views;
using UnityEngine;
using UnityEngine.Assertions;

namespace _MVFrameworkTest.Scripts.StaminaModule.Views
{
    /// <summary>
    /// Class of the character on stage, who listens to signals that modify his visual behavior.
    /// </summary>
    public class CharacterStaminaView : View
    {
        private readonly int _death = Animator.StringToHash("Death");
        private readonly int _run = Animator.StringToHash("Run");

        [SerializeField] private Animator _animator;
        
        private void Awake()
        {
            Assert.IsNotNull(_animator);

            AddListeners();
        }

        private void AddListeners()
        {
            AddUpdateSignalListener<InitStaminaSignal>(ResetState);
            AddUpdateSignalListener<UseStaminaSignal, bool>(UseStamina);
            AddUpdateSignalListener<WithoutStaminaSignal>(ResetState);
            AddUpdateSignalListener<DeathStaminaSignal>(Death);
        }

        private void ResetState()
        {
            _animator.SetBool(_run, false);
        }

        private void UseStamina(bool isRun)
        {
            _animator.SetBool(_run, isRun);
        }
        
        private void Death()
        {
            _animator.SetTrigger(_death);
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            RemoveUpdateSignalListener<InitStaminaSignal>(ResetState);
            RemoveUpdateSignalListener<UseStaminaSignal, bool>(UseStamina);
            RemoveUpdateSignalListener<WithoutStaminaSignal>(ResetState);
            RemoveUpdateSignalListener<DeathStaminaSignal>(Death);
        }
    }
}