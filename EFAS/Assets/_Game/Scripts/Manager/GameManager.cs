using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Money money;
        public Streng streng;
        public InventoryManager inventoryManager;
        public GameObject loadingScreen;
        public CanvasGroup LoadingScreenCanvasGroup;
        public Slider loadingScreenSlider;
        public CanvasGroup gameOverScreen;
        public GameObject WinScreen;
        public CanvasGroup tutorialScreen;
        public GameObject[] tutorialSlides;
        private int currentSlide = 0;
        
        private void OnEnable()
        {
            
        }
        private void Awake()
        {
            loadingScreen.SetActive(true);
            LoadingScreen();
            money.MoneyText.text = money.MoneyAmount.ToString();
            streng.StrengSlider.value = 100;
            DieState.OnDie += GameOver;
        }

        private void Update()
        {
            if (money.MoneyAmount >= 500)
            {
                WinScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }

        private void Tutorial()
        {
            foreach (GameObject slide in tutorialSlides)
            {
                slide.SetActive(false);
            }
            
            if (currentSlide < tutorialSlides.Length)
            {
                tutorialSlides[currentSlide].SetActive(true);
            }
            else
            {
                tutorialScreen.DOFade(0f, 3f).OnComplete(() =>
                {
                    tutorialScreen.gameObject.SetActive(false);
                });
            }
        }
        
        
        public void SkipTutorialSlide()
        {
            currentSlide++;
            
            Tutorial();
        }

        private void GameOver()
        {
            gameOverScreen.gameObject.SetActive(true);
            
            gameOverScreen.DOFade(1, 4.5f).OnComplete(() =>
            {
                DOTween.KillAll();
            });
        }

        private void LoadingScreen()
        {
            gameOverScreen.gameObject.SetActive(false);
            StartCoroutine(LoadingScreenCoroutine());
        }
        
        private IEnumerator LoadingScreenCoroutine()
        {
            float timeElapsed = 0;
            float duration = 10f;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                loadingScreenSlider.value += timeElapsed / duration; 
                yield return null; 
            }
            loadingScreenSlider.value = 100;

            LoadingScreenCanvasGroup.DOFade(0, 3f).OnComplete(() =>
            {
                tutorialScreen.gameObject.SetActive(true);
                loadingScreen.SetActive(false);
                Tutorial();
            });
        }
    }
}