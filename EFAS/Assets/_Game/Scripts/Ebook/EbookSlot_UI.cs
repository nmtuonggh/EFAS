
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Ebook
{
    public class EbookSlot_UI : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _itemPrice;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private EbookSlot assingnedInventorySlot;
        [SerializeField] private List<Image> _ingredientSlots;

        private Button button;
        public GameObject focus_Line;
        public EbookDisplay ParentDisplay { get; private set; }
        public EbookSlot AssingnedInventorySlot => assingnedInventorySlot;

        public GameObject FocusLine
        {
            get => focus_Line;
            set => focus_Line = value;
        }

        private void Awake()
        {
            ClearSlot();
            button = GetComponent<Button>();
            button.onClick.AddListener(OnEbookUISlotClick);

            ParentDisplay = transform.parent.GetComponent<EbookDisplay>();
        }
        
        public void Init(EbookSlot slot)
        {
            assingnedInventorySlot = slot;
            UpdateEbookUISlot(slot);
        }
        
        public void UpdateEbookUISlot(EbookSlot slot)
        {
            if (slot.Data != null)
            {
                _itemImage.sprite = slot.Data.Icon;
                _itemImage.color = Color.white;
                _itemPrice.text = slot.Data.Price.ToString();
                _itemName.text = slot.Data.DisplayName;
                
                int ingredientCount = slot.Data.Ingredients.Count;
                for (int i = 0; i < Mathf.Min(ingredientCount, _ingredientSlots.Count); i++)
                {
                    _ingredientSlots[i].sprite = slot.Data.Ingredients[i].Icon;
                }
            }
            else
            {
                ClearSlot();
            }
        }
        
        public void ClearSlot()
        {
            _itemImage.sprite = null;   
            _itemImage.color = Color.clear;
        }
        
        public void OnEbookUISlotClick()
        {
            if(ParentDisplay!= null) ParentDisplay.EbookSlotClicked(this);
        }
    }
}