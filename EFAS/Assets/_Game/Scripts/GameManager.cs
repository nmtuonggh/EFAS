using UnityEngine;

namespace _Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Money money;
        public Streng streng;
        public InventoryManager inventoryManager;
        
        private void OnEnable()
        {
            inventoryManager.LoadFromFile();
        }
        private void Awake()
        {
            money.MoneyText.text = money.MoneyAmount.ToString();
            streng.StrengSlider.value = 100;
            DieState.OnDie += GameOver;
        }
        
        private void GameOver()
        {
            Time.timeScale = 0;
        }
    }
}