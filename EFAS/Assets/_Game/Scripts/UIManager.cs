using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Cooking;
using _Game.Scripts.Event;
using _Game.Scripts.Shop;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        public GameObject Basket;
        //btn
        public GameObject buttonDropWhileHolding;
        public GameObject buttonInventory;
        
        //event
        public GameEvent OnOutInventory;
        public GameEventListener OnHoldingState;
        public GameEventListener UnHoldingState;
        //public GameEventListener OnHoldState;
        //popup ui
        public RectTransform cookPopupRect;
        public CanvasGroup cookPopupCanvas;
        public RectTransform moneyPopupRect;
        public CanvasGroup moneyPopupCanvas;
        //
        public BlackBoard blackBoard;
        public InputManager1 inputManager;
        public FloatingJoystick joystickMove;
        
        private void OnEnable()
        {
            OnHoldingState.OnEnable();
            UnHoldingState.OnEnable();
        }
        
        private void OnDestroy()
        {
            OnHoldingState.OnDisable();
            UnHoldingState.OnDisable();
        }

        private void Awake()
        {
            Cook.OnCookedFoodItem += SetDataPopupCook;
            BuyItem.OnOutOfMoney += SetDataPopupMoney;
        }

        #region Inventory

        public void InventoryPanelFadeIn()
        {
            joystickMove.ResetInput();
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
            
            blackBoard.stopMove = false;
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
            joystickMove.ResetInput();
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

        #region Popup

        public void ShowPopupCook()
        {
            cookPopupCanvas.gameObject.SetActive(true);
            cookPopupCanvas.alpha = 0;
            cookPopupCanvas.DOFade(1f, .5f);
            cookPopupRect.localScale = Vector3.zero;
            cookPopupRect.DOScale(Vector3.one, .5f);
        }
        
        public void HidePopupCook()
        {
            cookPopupCanvas.DOFade(0f, .5f);
            cookPopupRect.DOScale(Vector3.zero, .5f);
            cookPopupCanvas.gameObject.SetActive(false);
        }
        
        IEnumerator ShowPopup()
        {
            ShowPopupCook();
            yield return new WaitForSeconds(1f);
            HidePopupCook();
        }
        
        public void SetDataPopupCook(ItemData itemData)
        {
            var image = cookPopupCanvas.transform.GetChild(4);
            var name = cookPopupCanvas.transform.GetChild(3);

            image.GetComponent<Image>().sprite = itemData.Icon;
            name.GetComponent<TextMeshProUGUI>().text = itemData.DisplayName;
            StartCoroutine(nameof(ShowPopup));
        }
        
        
        public void ShowPopupMoney()
        {
            moneyPopupCanvas.gameObject.SetActive(true);
            moneyPopupCanvas.alpha = 0;
            moneyPopupCanvas.DOFade(1f, .5f);
            moneyPopupRect.localScale = Vector3.zero;
            moneyPopupRect.DOScale(Vector3.one, .5f);
        }
        
        public void HidePopupCookMoney()
        {
            moneyPopupCanvas.DOFade(0f, .5f);
            moneyPopupRect.DOScale(Vector3.zero, .5f);
            moneyPopupCanvas.gameObject.SetActive(false);
        }
        
        IEnumerator ShowPopupM()
        {
            ShowPopupMoney();
            yield return new WaitForSeconds(1f);
            HidePopupCookMoney();
        }
        
        public void SetDataPopupMoney()
        {
            StartCoroutine(nameof(ShowPopupM));
        }
        #endregion
    }
}