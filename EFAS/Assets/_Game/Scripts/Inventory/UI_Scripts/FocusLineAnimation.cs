using System;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

namespace _Game.Scripts.Inventory.UI_Scripts
{
    public class FocusLineAnimation : MonoBehaviour
    {
        private Tween _focusLineTween;
        public GameObject _focusLine;
        public float fadeTime;
        public Ease _easeForOutLine;
        public StaticInventoryDisplay staticInventoryDisplay;
        private void OnEnable()
        {
            staticInventoryDisplay = GetComponentInParent<StaticInventoryDisplay>();
            _focusLineTween = _focusLine.transform.DOScale(1.1f, fadeTime).SetEase(_easeForOutLine).SetLoops(-1, LoopType.Yoyo);
        }
    }
}