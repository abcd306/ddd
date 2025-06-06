using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterTrigger : ScrollObject
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
            GameManager.instance.GoToChapter((int)ChapterType.Chapter2);
           
            SpriteRenderer spr = other.GetComponent<SpriteRenderer>();
            if (spr != null)
                spr.flipX = !spr.flipX;

            //other.transform.position += Vector3.right * 10f;
            Vector3 playerPos = other.transform.position;
            float cameraX = Camera.main.transform.position.x;
            float mirroredX = cameraX - (playerPos.x - cameraX);
            other.transform.position = new Vector3(mirroredX, playerPos.y, playerPos.z);

            PlayerController.instance.RestoreFullHP();
            
            Destroy(gameObject);
        }
    }
}

