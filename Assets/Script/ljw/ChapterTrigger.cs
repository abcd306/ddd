using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterTrigger : ScrollObject
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activeChapters.Contains(GameManager.instance.GetCurrentChapter())) return;

        if (other.CompareTag("Player"))
        {
            GameManager.instance.GoToChapter((int)ChapterType.Chapter2);

            // �¿� ����
            SpriteRenderer spr = other.GetComponent<SpriteRenderer>();
            if (spr != null)
                spr.flipX = !spr.flipX;

            // �÷��̾� ��ġ �ݻ� �̵�
            Vector3 playerPos = other.transform.position;
            float cameraX = Camera.main.transform.position.x;
            float mirroredX = cameraX - (playerPos.x - cameraX);
            other.transform.position = new Vector3(mirroredX, playerPos.y, playerPos.z);

            PlayerController.instance.RestoreFullHP();
            Destroy(gameObject);
        }
    }
}

