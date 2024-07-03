using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Cooking
{
    public class Pot : MonoBehaviour
    {
        public List<ItemData> _ingredients = new List<ItemData>();
        public GameObject btnCook;

        public List<ItemData> Ingredients
        {
            get => _ingredients;
            set => _ingredients = value;
        }

        private void Update()
        {
            btnCook.SetActive(_ingredients.Count > 0);
        }
        
    }
}