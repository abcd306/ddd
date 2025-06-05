using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DreamEnergy : ScrollObject
{
    protected override void Awake()
    {
        base.Awake();
        activeChapters = new List<int> { 1,2,3 };
    }

    protected override void Move()
    {
        direction = ScrollManager.Instance.Direction;

        if (direction == 0) return;

        float moveX = moveSpeed * direction * Time.deltaTime;
        transform.Translate(Vector3.right * moveX);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance?.AddCoin();
            Destroy(gameObject);
        }
    }
}
