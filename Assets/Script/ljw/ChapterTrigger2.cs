using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterTrigger2 : ScrollObject
{
    protected override void Move()
    {
        direction = ScrollManager.Instance.Direction; // 방향 값 가져오기

        if (direction == 0) return;

        float moveX = moveSpeed * direction * Time.deltaTime;
        transform.Translate(Vector3.right * moveX);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScrollManager.Instance?.ChangeChapter(3);
            Destroy(gameObject);
        }
    }
}
