using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Cooking
{
    public class Pot : MonoBehaviour
    {
        private List<WorldItemData> _ingredients = new List<WorldItemData>();
        public GameObject btnCook;

        public List<WorldItemData> Ingredients
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