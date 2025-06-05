using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public int requiredCoins = 0;
    public int nextChapter = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (GameManager.instance == null || LightningManager.instance == null)
        {
            Debug.LogWarning("GameManager or LightningManager instance not found");
            return;
        }

        GameManager.instance.SetRequiredCoins(requiredCoins);

        bool hasEnoughCoins = GameManager.instance.HasEnoughCoins();

        LightningManager.instance.TriggerLightning(hasEnoughCoins);

        if (hasEnoughCoins)
        {
            Debug.Log($"é�� {nextChapter} ����! ���� ���.");

            GameManager.instance.currentChapter = nextChapter;
            GameManager.instance.ResetCoins();

        }
        else
        {
            Debug.Log("���� ����! �÷��̾� ���.");
        }
    }
}
