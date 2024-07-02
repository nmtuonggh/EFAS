using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Cooking
{
    public class Cook : MonoBehaviour
    {
        public Pot pot;
        public List<CookedFoodItem> cookedFoodItemList;
        public SpawnWorldItem spawnWorldItem;

        public void CookFood()
        {
            // sap xep lai danh sach nguyen lieu trong noi
            pot.Ingredients.Sort((a, b) => a.ID.CompareTo(b.ID));

            foreach (var cookedItem in cookedFoodItemList)
            {
                // neu so luong nguyen lieu khong bang nhau thi bo qua
                if (cookedItem.Ingredients.Count != pot.Ingredients.Count)
                    continue;

                // sap xep lai danh sach nguyen lieu trong mon an
                cookedItem.Ingredients.Sort((a, b) => a.ID.CompareTo(b.ID));

                // kiem tra xem nguyen lieu co trung khop khong
                bool ingredientsMatch = true;
                for (int i = 0; i < cookedItem.Ingredients.Count; i++)
                {
                    if (cookedItem.Ingredients[i].ID != pot.Ingredients[i].ID)
                    {
                        ingredientsMatch = false;
                        break;
                    }
                }

                // neu trung khop thi thuc hien nau mon an -------- :) Qua long vong nen toi phai dung tieng viet, Lmao
                if (ingredientsMatch)
                {
                    Debug.Log("Cooked Food: " + cookedItem.DisplayName);
                    spawnWorldItem.SpawnCookedFoodItem(cookedItem.ID);
                    break;
                }
            }
        }
    }
}