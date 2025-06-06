using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterTrigger2 : ScrollObject
{
    protected override void Awake()
    {
        base.Awake();
        activeChapters = new List<int> { 1, 3 };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GoToChapter((int)ChapterType.Chapter3);
            Destroy(gameObject);
        }
    }
}
