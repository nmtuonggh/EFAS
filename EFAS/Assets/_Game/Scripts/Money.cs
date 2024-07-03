using _Game.Scripts.Shop;
using TMPro;
using UnityEngine;

namespace _Game.Scripts
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private int _money;
        public int MoneyAmount => _money;

        public TextMeshProUGUI MoneyText
        {
            get => _moneyText;
            set => _moneyText = value;
        }

        [SerializeField] private TextMeshProUGUI _moneyText;
        public BuyItem BuyItem; 
        public SellItem SellItem;
        
        private void Awake()
        {
            BuyItem.OnBuyItemEvent += DecreaseMoney;
            //SellItem.OnSellItemEvent += AddMoney;
        }
        
        public void AddMoney(int amount)
        {
            _money += amount;
            Debug.Log("Money: " + _money);
            Debug.Log("amount: " + amount);
            _moneyText.text = _money.ToString();
        }
        
        public void DecreaseMoney(int amount)
        {
            _money -= amount;
            _moneyText.text = _money.ToString();
        }
    }
}