using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Inventory.UI_Scripts
{
    public class UIManager : MonoBehaviour
    {
        public float fadeTime = 1f;
        [FormerlySerializedAs("canvasGroup")] public CanvasGroup inventoryCanvasGroup;
        [FormerlySerializedAs("rectTransform")] public RectTransform inventoRectTransform;
        public GameObject controlUI;
        public GameObject pickUpUI;
        public List<GameObject> _inventoryItem;
        
        public void InventoryPanelFadeIn()
        {
            controlUI.SetActive(false);
            pickUpUI.SetActive(false);
            inventoryCanvasGroup.alpha = 0f;
            inventoRectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
            inventoRectTransform.DOAnchorPos(new Vector2(0f, 10f), fadeTime, false).SetEase(Ease.OutElastic);
            inventoryCanvasGroup.DOFade(1f, fadeTime);
            StartCoroutine(nameof(SlotAnimation));
        }
        
        public void InventoryPanelFadeOut()
        {
           
            inventoryCanvasGroup.alpha = 1f;
            inventoRectTransform.transform.localPosition = new Vector3(0, 0f, 0);
            inventoRectTransform.DOAnchorPos(new Vector2(0f, -1300f), fadeTime, false).SetEase(Ease.InOutQuint);
            inventoryCanvasGroup.DOFade(1f, fadeTime);
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
    }
}