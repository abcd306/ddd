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
            // ���ʿ� ������ 2é�� Ȱ��ȭ
            activeChapters = new List<int> { 2 };
        }
        else
        {
            // �����ʿ� ������ 1,3é�� Ȱ��ȭ
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
