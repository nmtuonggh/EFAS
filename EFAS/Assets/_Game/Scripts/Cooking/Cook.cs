using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Cooking
{
    public class Cook : MonoBehaviour
    {
        public Pot pot;
        public List<ItemData> cookedFoodItemList;
        public SpawnWorldItem spawnWorldItem;

        public void CookFood()
        {
            foreach (var cookedFood in cookedFoodItemList)
            {//Duyet qua danh sach cac mon an
                if (IsMatchedIngredients(cookedFood.Ingredients, pot.Ingredients))
                {//kiem tra xem nguyen lieu trong noi co phu hop voi cong thuc cua mon an khong
                 // neu co thi tao mon an va xoa nguyen lieu trong noi
                    spawnWorldItem.SpawnCookedFoodItem(cookedFood.ID);
                    pot.Ingredients.Clear();
                    return;
                }
            }
            
            //TODO: sua thanh lam mon sida
            //neu khong, thong bao cho nguoi choi va xoa nguyen lieu trong noi ---> sua thanh lam mon sida
            Debug.Log("No matched recipe found. Please check the ingredients.");
            pot.Ingredients.Clear();
        }

        private bool IsMatchedIngredients(List<ItemData> recipeIngredients, List<ItemData> potIngredients)
        {
            // kiem tra so luong nguyen lieu trong noi va cong thuc
            if (recipeIngredients.Count != potIngredients.Count)
            {
                return false;
            }

            // tao 2 dictionary de dem so luong nguyen lieu trong cong thuc va noi
            var recipeIngredientCounts = new Dictionary<int, int>();
            var potIngredientCounts = new Dictionary<int, int>();

            // dem so luong nguyen lieu trong cong thuc
            foreach (var ingredient in recipeIngredients)
            {
                if (recipeIngredientCounts.ContainsKey(ingredient.ID))
                {
                    recipeIngredientCounts[ingredient.ID]++;
                }
                else
                {
                    recipeIngredientCounts[ingredient.ID] = 1;
                }
            }

            // dem so luong nguyen lieu trong noi
            foreach (var ingredient in potIngredients)
            {
                if (potIngredientCounts.ContainsKey(ingredient.ID))
                {
                    potIngredientCounts[ingredient.ID]++;
                }
                else
                {
                    potIngredientCounts[ingredient.ID] = 1;
                }
            }

            // so sanh so luong nguyen lieu trong cong thuc va noi
            foreach (var pair in recipeIngredientCounts)
            {
                if (!potIngredientCounts.ContainsKey(pair.Key) || potIngredientCounts[pair.Key] != pair.Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}