using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Ebook
{
    public class ItemDetail : MonoBehaviour
    {
        [SerializeField] private EbookDisplay _ebookDisplay;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _price;

        private void Awake()
        {
            // Subscribe to the EbookSlotClicked event
            _ebookDisplay.EbookSlotClick += OnEbookSlotClicked;
        }

        private void OnEbookSlotClicked(EbookSlot_UI clickedSlot)
        {
            // Get the data of the clicked slot
            ItemData itemData = clickedSlot.AssingnedInventorySlot.Data;

            // Update the UI elements with the data of the clicked slot
            _icon.sprite = itemData.Icon;
            _name.text = itemData.DisplayName;
            _description.text = itemData.Description;
            _price.text = itemData.Price.ToString();
        }
    }
}