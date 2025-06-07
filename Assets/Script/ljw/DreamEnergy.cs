using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DreamEnergy : ScrollObject
{
    protected override void Awake()
    {
        base.Awake();
        float cameraCenterX = Camera.main.transform.position.x;

        if (transform.position.x < cameraCenterX)
        {
            // 왼쪽에 있으면 2챕터 활성화
            activeChapters = new List<int> { 2 };
        }
        else
        {
            // 오른쪽에 있으면 1,3챕터 활성화
            activeChapters = new List<int> { 1, 3 };
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activeChapters.Contains(GameManager.instance.GetCurrentChapter())) return;

        if (other.CompareTag("Player"))
        {
            GameManager.instance?.AddCoin();
            Destroy(gameObject);
        }
    }
}
