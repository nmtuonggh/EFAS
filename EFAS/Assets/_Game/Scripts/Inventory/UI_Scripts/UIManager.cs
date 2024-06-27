using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Inventory.UI_Scripts
{
    public class UIManager : MonoBehaviour
    {
        public float fadeTime = 1f;
        public CanvasGroup canvasGroup;
        public RectTransform rectTransform;
        public List<GameObject> _inventoryItem;
        
        public void PanelFadeIn()
        {
            canvasGroup.alpha = 0f;
            rectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
            rectTransform.DOAnchorPos(new Vector2(0f, 10f), fadeTime, false).SetEase(Ease.Flash);
            canvasGroup.DOFade(1f, fadeTime);
            StartCoroutine(nameof(SlotAnimation));
        }
        
        public void PanelFadeOut()
        {
            canvasGroup.alpha = 1f;
            rectTransform.transform.localPosition = new Vector3(0, 0f, 0);
            rectTransform.DOAnchorPos(new Vector2(0f, -1300f), fadeTime, false).SetEase(Ease.Flash);
            canvasGroup.DOFade(1f, fadeTime);
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