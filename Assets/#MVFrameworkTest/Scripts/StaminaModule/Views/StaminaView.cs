using System.Collections.Generic;
using _MVFrameworkTest.Scripts.StaminaModule.Controllers;
using _MVFrameworkTest.Scripts.StaminaModule.Signals;
using _MVFrameworkTest.Scripts.StaminaModule.Utils;
using DG.Tweening;
using MVFramework.Views;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _MVFrameworkTest.Scripts.StaminaModule.Views
{
    /// <summary>
    /// Class used in the stamina bar, which visually modifies its behavior.
    /// </summary>
    public class StaminaView : View
    {       
        [Header("Character stamina")]
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private Image _generalEnergyBar;
        [SerializeField] private Image _temporalEnergyBar;
        [SerializeField] private Image _templateSegment;

        private readonly List<Image> _segments = new List<Image>();
        private StaminaController _staminaController;
        private StaminaConfiguration _staminaConfiguration;

        private float _currentGeneralEnergy;
        private float _currentTemporalEnergy;
        private int _lastPenalizedSegment;
        private bool _isTemporalActive;
        private bool _lastStanding;
        private bool _fadingColor;

        private void Awake()
        {
            _staminaController = GetController<StaminaController>();
            _staminaConfiguration = GetScriptable<StaminaConfiguration>();
            
            Assert.IsNotNull(_staminaController);
            Assert.IsNotNull(_staminaConfiguration);
            
            AddUpdateSignalListener<InitStaminaSignal>(Init);
            AddUpdateSignalListener<ShowStaminaSignal>(Show);
            AddUpdateSignalListener<HideStaminaSignal>(Hide);
            AddUpdateSignalListener<UseStaminaSignal, bool>(UseStamina);
        }

        private void Start()
        {
            Init();
            
            if (_staminaConfiguration.autoShow) Show();
        }

        /// <summary>
        /// Separates the total energy into segments and collects the data from the configuration scriptable.
        /// </summary>
        private void Init()
        {
            _currentGeneralEnergy = GetInitEnergy();
            _currentTemporalEnergy = _currentGeneralEnergy;
            _lastStanding = false;
            
            CreateSegments();
            UpdateGeneralEnergy();
            UpdateTemporalEnergy();
        }

        private float GetInitEnergy()
        {
            if (_staminaConfiguration.initGeneralEnergy > _staminaConfiguration.maxGeneralEnergy)
                return _staminaConfiguration.maxGeneralEnergy;

            return Mathf.Min(_staminaConfiguration.initGeneralEnergy, _staminaConfiguration.maxGeneralEnergy);
        }
        
        private void CreateSegments()
        {
            foreach (Image segment in _segments)
            {
                Destroy(segment.gameObject);
            }
            _segments.Clear();

            int totalSegments = _staminaConfiguration.segments;
            float segmentSize = 1f / totalSegments;

            for (int i = 0; i < totalSegments; i++)
            {
                Image newSegment = Instantiate(_templateSegment, _generalEnergyBar.transform);
                _segments.Add(newSegment);
                
                float minX = i * segmentSize;
                float maxX = (i + 1) * segmentSize;
                
                RectTransform segmentRectTransform = newSegment.GetComponent<RectTransform>();
                segmentRectTransform.anchorMin = new Vector2(minX, 0f);
                segmentRectTransform.anchorMax = new Vector2(maxX, 1f);
                
                segmentRectTransform.offsetMin = Vector2.zero;
                segmentRectTransform.offsetMax = Vector2.zero;
                
                newSegment.fillAmount = 1f;
            }

            _lastPenalizedSegment = _segments.Count - 1;
        }
        
        private void Show()
        {
            _group.DOFade(1f, 1f);    
        }

        private void Hide()
        {
            _group.DOFade(0f, 1f);
        }
        
        private void UseStamina(bool state)
        {
            if(!enabled)
                return;
            
            _isTemporalActive = state;
        }

        private void Update()
        {
            if(!enabled) return;
            
            if (_isTemporalActive)
            {
                UseTemporalEnergy();
            }
            else if(_currentTemporalEnergy < _currentGeneralEnergy)
            {
                RecoveryTemporalEnergy();
            }
        }

        private void UseTemporalEnergy()
        {
            _currentTemporalEnergy -= _staminaConfiguration.temporalEnergyDepletionRate * Time.deltaTime;
            UpdateTemporalEnergy(false);
            CheckPenalties(false);
        }
        
        private void RecoveryTemporalEnergy()
        {
            _currentTemporalEnergy += _staminaConfiguration.temporalEnergyRecoveryRate * Time.deltaTime;
            UpdateTemporalEnergy(true);
            CheckPenalties(true);
        }

        
        /// <summary>
        /// Adds penalties for crossing a segment and losing temporary energy
        /// </summary>
        private void CheckPenalties(bool isRecovery)
        {
            if (!isRecovery && _currentTemporalEnergy <= 0f)
            {
                ApplyExtraPenalty();
                _isTemporalActive = false;
                _currentTemporalEnergy = 0;
                _lastPenalizedSegment = -1;
            }
            else
            {
                float segmentSize = _staminaConfiguration.maxGeneralEnergy / _staminaConfiguration.segments;
                
                int currentSegment = Mathf.FloorToInt(_currentTemporalEnergy / segmentSize);
                currentSegment = Mathf.Clamp(currentSegment, 0, _staminaConfiguration.segments - 1);

                if (!isRecovery && _lastPenalizedSegment != -1 && currentSegment < _lastPenalizedSegment)
                {
                    ApplyPenalty();
                }
                
                _lastPenalizedSegment = currentSegment;
            }
        }
        
        private void ApplyPenalty()
        {
            if (_currentGeneralEnergy - _staminaConfiguration.segmentPenalty > _staminaConfiguration.criticalThreshold)
            {
                ChangeGeneralColorTemporally(_staminaConfiguration.penaltyBarColor);
                _currentGeneralEnergy -= _staminaConfiguration.segmentPenalty;
            }
            
            UpdateGeneralEnergy();
        }

        private void ApplyExtraPenalty()
        {
            _currentGeneralEnergy -= _staminaConfiguration.extraPenalty;

            if (_currentGeneralEnergy <= _staminaConfiguration.criticalThreshold && !_lastStanding)
            {
                _lastStanding = true;
                _currentGeneralEnergy = _staminaConfiguration.criticalThreshold;
                _staminaController.WithoutEnergy();
            }
            if (_currentGeneralEnergy < 0f && _lastStanding)
            {
                _staminaController.Death();
            }
            else
            {
                ChangeGeneralColorTemporally(_staminaConfiguration.extraPenaltyBarColor);
                _staminaController.WithoutEnergy();
            }

            UpdateGeneralEnergy();
        }

        private void UpdateTemporalEnergy(bool isRecovery = true)
        {
            float temporalAmount = _currentTemporalEnergy / _staminaConfiguration.maxGeneralEnergy;
            _temporalEnergyBar.fillAmount = Mathf.Min(temporalAmount, _generalEnergyBar.fillAmount);

            _temporalEnergyBar.color = (isRecovery) ?
                _staminaConfiguration.temporalRecoveryBarColor :
                _staminaConfiguration.temporalUseBarColor;
        }

        private void UpdateGeneralEnergy()
        {
            float generalAmount = _currentGeneralEnergy / _staminaConfiguration.maxGeneralEnergy;
            _generalEnergyBar.fillAmount = generalAmount;

            if(_fadingColor) return;
            
            _generalEnergyBar.color = (_currentGeneralEnergy < _staminaConfiguration.criticalThreshold) ?
                _staminaConfiguration.criticalBarColor :
                _staminaConfiguration.generalBarColor;
        }

        private void ChangeGeneralColorTemporally(Color targetColor)
        {
            _fadingColor = true;
            
            _generalEnergyBar.DOColor(targetColor, _staminaConfiguration.ColorBarFadeTime).OnComplete(() =>
            {
                _fadingColor = false;
                _generalEnergyBar.DOColor((_currentGeneralEnergy < _staminaConfiguration.criticalThreshold) ?
                    _staminaConfiguration.criticalBarColor :
                    _staminaConfiguration.generalBarColor, _staminaConfiguration.ColorBarFadeTime);
            });
        }

        private void OnDestroy()
        {
            RemoveUpdateSignalListener<InitStaminaSignal>(Init);
            RemoveUpdateSignalListener<ShowStaminaSignal>(Show);
            RemoveUpdateSignalListener<HideStaminaSignal>(Hide);
            RemoveUpdateSignalListener<UseStaminaSignal, bool>(UseStamina);
        }
    }
}