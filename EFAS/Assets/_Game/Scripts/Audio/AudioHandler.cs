using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Cooking;
using _Game.Scripts.Shop;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioClip footstepSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip turnSound;
    public AudioClip dieSound;
    public AudioClip buyselSound;
    public AudioClip pickSound;
    public AudioClip popupSound;
    public AudioClip eatSound;
    public AudioClip fishSound;
    public AudioClip cookSuccessSound;
    public AudioClip cookFailSound;
    
    public AudioSource AudioSource;

    private void Awake()
    {
        SellItem.AudioSellItem += SellBuy;
        BuyItem.AudioBuyItem += SellBuy;
        Cook.OnPop+=Pop;
        Cook.OnCookSuccess += CookSuccess;
        Cook.OnCookeFail += CookFail;
        _Game.Scripts.Inventory.InventoryAction.Eat.OnEatSound += Eat;
    }

    public void FootStep()
    {
        AudioSource.PlayOneShot(footstepSound);
    }
    public void JumpSound()
    {
        AudioSource.PlayOneShot(jumpSound);
    }

    public void Land()
    {
        AudioSource.PlayOneShot(landSound);
    }

    public void Turn()
    {
        AudioSource.PlayOneShot(turnSound);
    }
    
    public void Die()
    {
        AudioSource.PlayOneShot(dieSound);
    }
    
    public void SellBuy()
    {
        AudioSource.PlayOneShot(buyselSound);
    }
    
    public void Pick()
    {
        AudioSource.PlayOneShot(pickSound);
    }

    public void Pop()
    {
        AudioSource.PlayOneShot(popupSound);
    }
    
    public void Eat()
    {
        AudioSource.PlayOneShot(eatSound);
    }
    
    public void Fish()
    {
        AudioSource.PlayOneShot(fishSound);
    }
    
    public void CookSuccess()
    {
        AudioSource.PlayOneShot(cookSuccessSound);
    }
    
    public void CookFail()
    {
        AudioSource.PlayOneShot(cookFailSound);
    }
}
