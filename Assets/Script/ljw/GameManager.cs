using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int collectedCoins = 0;
    public int requiredCoins = 0;
    public int currentChapter = 1;

    public GameObject gameOverUI;

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

    public void SetRequiredCoins(int amount)
    {
        requiredCoins = amount;
        UIManager.instance?.UpdateCoinUI(collectedCoins, requiredCoins);
    }

    public bool HasEnoughCoins() => collectedCoins >= requiredCoins;

    public int GetCollectedCoins() => collectedCoins;

    public int GetRequiredCoins() => requiredCoins;

    private void UpdateCoinUI()
    {
        UIManager.instance?.UpdateCoinUI(collectedCoins, requiredCoins);

    }
    void GameOver()
    {
        UIManager.instance?.HideLivesUI();
        UIManager.instance?.HideCoinUI();

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }
}
