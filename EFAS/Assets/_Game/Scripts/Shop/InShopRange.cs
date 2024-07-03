using _Game.Scripts.Cooking;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Shop
{
    public class InShopRange : MonoBehaviour
    {
        public bool inShopRange;
        public GameObject btnShop;
        public GameObject btnSellInventory;
        public GameObject btnSellAllInventory;
        public RectTransform ShopUI;
        public GameObject ControlUI;
            
        
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
                btnSellInventory.SetActive(true);
                btnSellAllInventory.SetActive(true);
            }
            else
            {
                btnShop.SetActive(false);
                btnSellInventory.SetActive(false);
                btnSellAllInventory.SetActive(false);
            }
        
        }
        
        public void OpenShop()
        {   
            ShopUI.DOAnchorPos(new Vector2(0f, 0f), 0.25f);
            ControlUI.SetActive(false);
        }
        
        public void CloseShop()
        {
            ShopUI.DOAnchorPos(new Vector2(2000f, 0f), 0.25f);
            ControlUI.SetActive(true);
        }
    }
}