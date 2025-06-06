using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChapterType
{
    MainMenu = 0,
    Chapter1 = 1,
    Chapter2 = 2,
    Chapter3 = 3,
    ImageBoard = 100,
    Ending = 999
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverUI;

    public SpriteRenderer backgroundRenderer;
    public Sprite BG1;
    public Sprite BG2;
    public Sprite BG3;

    private int collectedCoins = 0;
    private int requiredCoins = 0;
    public int currentChapter = (int)ChapterType.Chapter1;

    public int GetCurrentChapter() { return currentChapter; }
    public int GetCollectedCoins() { return collectedCoins; }
    public int GetRequiredCoins() { return requiredCoins; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddCoin()
    {
        collectedCoins++;
        UpdateCoinUI();
    }

    public void ResetCoins()
    {
        collectedCoins = 0;
        UpdateCoinUI();
    }

    public void SetRequiredCoins()
    {
        requiredCoins = currentChapter;
        UpdateCoinUI();
    }

    public bool HasEnoughCoins() { return collectedCoins >= requiredCoins; }


    public void GoToChapter(int chapter)
    {
        currentChapter = chapter;
        ResetCoins();
        UpdateBackground(chapter);
        SetRequiredCoins();
        ScrollManager.Instance?.SetDirectionForChapter(chapter);
    }

    private void UpdateBackground(int chapter)
    {
        if (backgroundRenderer == null) return;

        switch ((ChapterType)chapter)
        {
            case ChapterType.Chapter1:
                backgroundRenderer.sprite = BG1;
                break;
            case ChapterType.Chapter2:
                backgroundRenderer.sprite = BG2;
                break;
            case ChapterType.Chapter3:
                backgroundRenderer.sprite = BG3;
                break;
        }
    }

    private void UpdateCoinUI()
    {
        UIManager.instance?.UpdateCoinUI(collectedCoins, requiredCoins);
    }

    public void GameOver()
    {
        UIManager.instance?.HideLivesUI();
        UIManager.instance?.HideCoinUI();
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }
}