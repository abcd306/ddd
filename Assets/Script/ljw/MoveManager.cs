using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MoveManager : ScrollObject
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activeChapters.Contains(GameManager.instance.GetCurrentChapter())) return;

        if (other.CompareTag("Player"))
        {
            PlayerController.instance?.TakeDamage();
        }
    }
}
