using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Shop
{
    public class ShopSlot_UI : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _itemPrice;
        [SerializeField] private ShopInvenSlot assingnedInventorySlot;

        private Button button;
        public GameObject focus_Line;
        public ShopDisplay ParentDisplay { get; private set; }
        public ShopInvenSlot AssingnedInventorySlot => assingnedInventorySlot;

        public GameObject FocusLine
        {
            get => focus_Line;
            set => focus_Line = value;
        }

        private void Awake()
        {
            ClearSlot();
            button = GetComponent<Button>();
            button.onClick.AddListener(OnShopUISlotClick);

            ParentDisplay = transform.parent.GetComponent<ShopDisplay>();
        }
        
        public void Init(ShopInvenSlot slot)
        {
            assingnedInventorySlot = slot;
            UpdateShopUISlot(slot);
        }
        
        public void UpdateShopUISlot(ShopInvenSlot slot)
        {
            if (slot.Data != null)
            {
                _itemImage.sprite = slot.Data.Icon;
                _itemImage.color = Color.white;
                _itemPrice.text = slot.Data.Price.ToString();
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
        
        public void OnShopUISlotClick()
        {
            if(ParentDisplay!= null) ParentDisplay.ShopSlotClicked(this);
        }
    }
}