using _Game.Scripts.Cooking;
using DG.Tweening;
using Unity.VisualScripting;
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
        public InputManager1 inputManager;
        public static event System.Action OnShop;
        public static event System.Action OutShop;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Shop") )
            {
                OnShop?.Invoke();
                inShopRange = true;
                other.GetComponent<Outline>().OutlineWidth = 2f;
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Shop"))
            {
                inShopRange = false;
                OutShop?.Invoke();
                other.GetComponent<Outline>().OutlineWidth = 0f;
            }
        }

        public void OpenShop()
        {   
            inputManager.joystickMove.ResetInput();
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