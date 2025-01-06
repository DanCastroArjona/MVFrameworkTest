using UnityEngine;

namespace _MVFrameworkTest.Scripts.StaminaModule.Utils
{
    [CreateAssetMenu(fileName = "StaminaConfiguration", menuName = "Mvc/Config/Stamina")]
    public class StaminaConfiguration : ScriptableObject
    {
        public bool autoShow = false;
        
        [Header("General energy")]
        public float maxGeneralEnergy = 100f;
        public float initGeneralEnergy = 100f;
        public int segments = 4;

        [Space]
        public float segmentPenalty = 5f;
        public float extraPenalty = 15f;
        public float criticalThreshold = 25f;

        [Space]
        public Color generalBarColor;
        public Color criticalBarColor;
        public Color extraPenaltyBarColor;
        public Color penaltyBarColor;
        public float ColorBarFadeTime = 0.5f;

        [Space]
        [Header("Temporal energy")]
        public float temporalEnergyDepletionRate = 10f;  
        public float temporalEnergyRecoveryRate = 5f;
        
        [Space]
        public Color temporalUseBarColor;
        public Color temporalRecoveryBarColor;
    }
}