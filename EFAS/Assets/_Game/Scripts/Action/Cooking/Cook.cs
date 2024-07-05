using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Event;
using UnityEngine;

namespace _Game.Scripts.Cooking
{
    public class Cook : MonoBehaviour
    {
        public Pot pot;
        public List<ItemData> cookedFoodItemList;
        public ItemData BadcookedFoodItem;
        public SpawnWorldItem spawnWorldItem;
        public ParticleSystem cookingEffect;
        public ParticleSystem fireEffect;
        public ParticleSystem boomEffect;
        public ParticleSystem failEffect;
        public GameEventT<float> DecreaseStrength;
        public static event System.Action<ItemData> OnCookedFoodItem;

        public void CookFood()
        {
            cookingEffect.Play();
            fireEffect.Play();
            foreach (var cookedFood in cookedFoodItemList)
            {//Duyet qua danh sach cac mon an
                if (IsMatchedIngredients(cookedFood.Ingredients, pot.Ingredients))
                {//kiem tra xem nguyen lieu trong noi co phu hop voi cong thuc cua mon an khong
                 // neu co thi tao mon an va xoa nguyen lieu trong noi
                 pot.btnCook.SetActive(false);
                 StartCoroutine(DelaySpawnCookedFoodItem(cookedFood.ID));
                 return;
                }
            }
            
            //TODO: sua thanh lam mon sida
            //neu khong, thong bao cho nguoi choi va xoa nguyen lieu trong noi ---> sua thanh lam mon sida
            pot.btnCook.SetActive(false);
            StartCoroutine(DelaySpawnFailFoodItem(BadcookedFoodItem.ID));
        }
        
        private IEnumerator DelaySpawnCookedFoodItem(int ID)
        {
            yield return new WaitForSeconds(2);
            boomEffect.gameObject.SetActive(true);
            spawnWorldItem.SpawnCookedFoodItem(ID);
            pot.Ingredients.Clear();
            cookingEffect.Stop();
            fireEffect.Stop();
            DecreaseStrength.Raise(0.05f);
            OnCookedFoodItem?.Invoke(cookedFoodItemList.Find(x => x.ID == ID));
            Invoke(nameof(WaitSuccess), 2f);
        }
        
        private void WaitSuccess()
        {
            boomEffect.gameObject.SetActive(false);
        }
        private IEnumerator DelaySpawnFailFoodItem(int ID)
        {
            yield return new WaitForSeconds(2);
            StartCoroutine(waitFail(ID));
        }

        private IEnumerator waitFail(int ID)
        {
            failEffect.Play();
            spawnWorldItem.SpawnCookedFoodItem(ID);
            OnCookedFoodItem?.Invoke(BadcookedFoodItem);
            yield return new WaitUntil(() => !failEffect.isPlaying);
            pot.Ingredients.Clear();
            cookingEffect.Stop();
            fireEffect.Stop();
            DecreaseStrength.Raise(0.2f);
        }
        
        private bool IsMatchedIngredients(List<ItemData> recipeIngredients, List<ItemData> potIngredients)
        {
            // kiem tra so luong nguyen lieu trong noi va cong thuc
            if (recipeIngredients.Count != potIngredients.Count)
            {
                return false;
            }
            Debug.Log("So luong trong cong thuc"+ recipeIngredients.Count + "So luong trong noi" + potIngredients.Count);
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