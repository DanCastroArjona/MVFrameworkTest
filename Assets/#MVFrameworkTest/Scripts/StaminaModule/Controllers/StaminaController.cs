using _MVFrameworkTest.Scripts.StaminaModule.Models;
using MVFramework.Controllers;

namespace _MVFrameworkTest.Scripts.StaminaModule.Controllers
{
    public class StaminaController : Controller
    {
        private readonly StaminaModel _model;
        
        public StaminaController(StaminaModel model)
        {
            _model = model;
        }

        public void Init()
        {
            _model.InitSystem();
        }
        
        public void Show()
        {
            _model.Show();
        }
        
        public void Hide()
        {
            _model.Hide();
        }

        public void UseStamina(bool state)
        {
            _model.UseStamina(state);
        }

        public void WithoutEnergy()
        {
            _model.WithoutEnergy();
        }

        public void Death()
        {
            _model.Death();
        }
    }
}