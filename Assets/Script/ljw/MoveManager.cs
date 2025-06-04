using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MoveManager : ScrollObject
{
    protected override void Awake()
    {
        base.Awake();
        activeChapters = new List<int> { 1, 3 };
    }

    protected override void Move()
    {
        direction = ScrollManager.Instance.Direction; // 방향 값 가져오기

        if (direction == 0) return;

        float moveX = moveSpeed * direction * Time.deltaTime;
        transform.Translate(Vector3.right * moveX);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerController.instance != null)
        {
            PlayerController.instance.TakeDamage();
        }
    }
}
