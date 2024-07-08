using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class Streng : MonoBehaviour
    {
        [SerializeField] private Slider _strengSlider;
        public GameEventListenerT<float> OnDecreaseStreng;
        public GameEventListenerT<float> OnIncreaseStreng;
        public Slider StrengSlider
        {
            get => _strengSlider;
            set => _strengSlider = value;
        }

        private void OnEnable()
        {
            OnIncreaseStreng.OnEnable();
            OnDecreaseStreng.OnEnable();
        }
        
        private void OnDisable()
        {
            OnIncreaseStreng.OnDisable();
            OnDecreaseStreng.OnDisable();
        }

        public void DecreaseStreng(float value)
        {
            _strengSlider.value -= value;
        }
        
        public void IncreaseStreng(float value)
        {
            _strengSlider.value += value;
        }
    }
}