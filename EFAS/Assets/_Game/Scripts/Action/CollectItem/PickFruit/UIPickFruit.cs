using UnityEngine;

namespace _Game.Scripts.Action.PickFruit
{
    /*public class UIPickFruit : MonoBehaviour
    {
        [SerializeField] private GameObject btnPickFruit;
        [SerializeField] private InPickFruitRange _pickFruitRange;
        [SerializeField] private RectTransform _popupPickFruitRectTransform;
        [SerializeField] private CanvasGroup _popupFishingCanvasGroup;
        [SerializeField] private FishingRate _fishingRate;

        private void OnEnable()
        {
            _fishingRate.OnPopup += setDataPopup;
        }

        private void Update()
        {
            btnFishing.SetActive(_fishingRange.CanFishing);
        }

        public void setDataPopup(ItemData itemData)
        {
            var image = _popupFishingCanvasGroup.transform.GetChild(4);
            var name = _popupFishingCanvasGroup.transform.GetChild(5);

            image.GetComponent<Image>().sprite = itemData.Icon;
            name.GetComponent<TextMeshProUGUI>().text = itemData.DisplayName;
            StartCoroutine(nameof(ShowPopup));
        }
        
        public void InPopupFishing()
        {
            _popupFishingCanvasGroup.gameObject.SetActive(true);
            _popupFishingCanvasGroup.alpha = 0;
            _popupFishingCanvasGroup.DOFade(1f, .5f);
            _popupFishingRectTransform.localScale = Vector3.zero;
            _popupFishingRectTransform.DOScale(Vector3.one, .5f);
        }
        
        public void OutPopupFishing()
        {
            _popupFishingCanvasGroup.DOFade(0f, .5f);
            _popupFishingRectTransform.DOScale(Vector3.zero, .5f);
            _popupFishingCanvasGroup.gameObject.SetActive(false);
        }
        
        IEnumerator ShowPopup()
        {
            InPopupFishing();
            yield return new WaitForSeconds(1f);
            OutPopupFishing();
        }
    }*/
}