using _MVFrameworkTest.Scripts.StaminaModule.Signals;
using MVFramework.Models;

namespace _MVFrameworkTest.Scripts.StaminaModule.Models
{
    public class StaminaModel : Model
    {
        private StaminaState _staminaState = StaminaState.Enable;
        
        public void InitSystem()
        {
            _staminaState = StaminaState.Enable;
            SendUpdateSignal<InitStaminaSignal>();
        }
        
        public void Show()
        {
            SendUpdateSignal<ShowStaminaSignal>();
        }
        
        public void Hide()
        {
            SendUpdateSignal<HideStaminaSignal>();
        }
        
        public void UseStamina(bool use)
        {
            if(_staminaState == StaminaState.Disable) return;
            
            SendUpdateSignal<UseStaminaSignal, bool>(use);
        }

        public void WithoutEnergy()
        {
            SendUpdateSignal<WithoutStaminaSignal>();
        }

        public void Death()
        {
            _staminaState = StaminaState.Disable;
            
            Hide();
            SendUpdateSignal<DeathStaminaSignal>();
        }
    }

    public enum StaminaState
    {
        Enable,
        Disable
    }
}