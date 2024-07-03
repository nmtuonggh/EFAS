using _Game.Scripts.Cooking;
using UnityEngine;

namespace _Game.Scripts.Shop
{
    public class InShopRange : MonoBehaviour
    {
        public bool inShopRange;
        public GameObject btnShop;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Shop") )
            {
                inShopRange = true;
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Shop"))
            {
                inShopRange = false;
            }
        }

        private void Update()
        {
            if (inShopRange)
            {
                btnShop.SetActive(true);
            }
            else
            {
                btnShop.SetActive(false);
            }
        
        }
    }
}