using UnityEngine;

namespace _Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Money money;
        
        private void Awake()
        {
            /*if (Instance == null)
            {
                Instance = this;
            }*/
            
            money.MoneyText.text = money.MoneyAmount.ToString();
        }
    }
}