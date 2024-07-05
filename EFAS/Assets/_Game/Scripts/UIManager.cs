using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Event;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Game.Scripts.Inventory.UI_Scripts
{
    public class UIManager : MonoBehaviour
    {
        public float fadeTime = 1f;
        public List<GameObject> _inventoryItem;
        
        [FormerlySerializedAs("canvasGroup")] public CanvasGroup inventoryCanvasGroup;
        [FormerlySerializedAs("rectTransform")] public RectTransform inventoRectTransform;
        public RectTransform ebookRectTransform;
        public CanvasGroup ebookCanvasGroup;
        public GameObject controlUI;
        public GameObject pickUpUI;
        public GameObject Rod;
        //btn
        public GameObject buttonDropWhileHolding;
        public GameObject buttonInventory;
        
        //event
        public GameEvent OnOutInventory;
        public GameEventListener OnHoldingState;
        public GameEventListener UnHoldingState;
        //public GameEventListener OnHoldState;
        

        private void Awake()
        {
            OnHoldingState.OnEnable();
            UnHoldingState.OnEnable();
        }
        
        private void OnDestroy()
        {
            OnHoldingState.OnDisable();
            UnHoldingState.OnDisable();
        }

        #region Inventory

        public void InventoryPanelFadeIn()
        {
            controlUI.SetActive(false);
            pickUpUI.SetActive(false);
            inventoryCanvasGroup.alpha = 0f;
            inventoRectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
            inventoRectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutElastic);
            inventoryCanvasGroup.DOFade(1f, fadeTime);
            StartCoroutine(nameof(SlotAnimation));
        }
        
        public void InventoryPanelFadeOut()
        {
            OnOutInventory.Raise(); 
            inventoryCanvasGroup.alpha = 1f;
            inventoRectTransform.transform.localPosition = new Vector3(0, 0f, 0);
            inventoRectTransform.DOAnchorPos(new Vector2(0f, -1300f), fadeTime, false).SetEase(Ease.InOutQuint);
            inventoryCanvasGroup.DOFade(0f, fadeTime);
            inventoryCanvasGroup.isActiveAndEnabled.Equals(false);
            controlUI.SetActive(true);
            pickUpUI.SetActive(true);
        }

        public IEnumerator SlotAnimation()
        {
            foreach (var item in _inventoryItem )
            {
                item.transform.localScale = Vector3.zero;
            }

            foreach (var item in _inventoryItem)
            {
                item.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
                yield return new WaitForSeconds(0.1f);
            }
        }

        #endregion

        #region BtnDropWhileHolding

        public void activeBtnDropWhileHolding()
        {
            buttonDropWhileHolding.SetActive(true);
        }
        public void unActiveBtnDropWhileHolding()
        {
            buttonDropWhileHolding.SetActive(false);
        }

        #endregion

        #region Ebook

        public void FadeInEbook()
        {
            ebookCanvasGroup.alpha = 0f;
            ebookRectTransform.DOAnchorPos(new Vector2(0f, 0f), 0.25f);
            ebookCanvasGroup.DOFade(1f, 0.25f);
        }
        
        public void FadeOutEbook()
        {
            ebookCanvasGroup.alpha = 1f;
            ebookRectTransform.DOAnchorPos(new Vector2(-2000f, 0f), 0.25f);
            ebookCanvasGroup.DOFade(0f, 0.25f);
        }

        #endregion
    }
}