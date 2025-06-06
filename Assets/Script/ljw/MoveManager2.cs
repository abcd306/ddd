using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager2 : ScrollObject
{
    protected override void Awake()
    {
        base.Awake();
        activeChapters = new List<int> { 2 };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerController.instance != null)
        {
            PlayerController.instance.TakeDamage();
        }
    }
}
