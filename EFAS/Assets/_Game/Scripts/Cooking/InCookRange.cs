using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Cooking;
using _Game.Scripts.Inventory.UI_Scripts;
using UnityEngine;

public class InCookRange : MonoBehaviour
{
    public bool inCookRange;
    public GameObject btnThrow;
    public PreviewHolder PreviewHolder;
    public Pot pot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pot") )
        {
            inCookRange = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pot"))
        {
            inCookRange = false;
        }
    }

    private void Update()
    {
        if (inCookRange && PreviewHolder.ItemCount > 0 && pot.Ingredients.Count <= 4)
        {
            btnThrow.SetActive(inCookRange);
        }
        else if(!inCookRange || PreviewHolder.ItemCount == 0 || pot.Ingredients.Count > 4)
        {
            btnThrow.SetActive(false);
        }
        
    }
}
