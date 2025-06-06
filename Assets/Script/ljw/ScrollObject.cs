using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrollObject : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float leftLimit = -30f;
    public float rightLimit = 30f;
    public int direction = 0;

    public List<int> activeChapters;
    protected float initialX;

    protected virtual void Awake()
    {
        initialX = transform.position.x;
    }

    protected virtual void Update()
    {
        if (ScrollManager.Instance == null) return;

        if (activeChapters == null || !activeChapters.Contains(GameManager.instance.GetCurrentChapter())) return;

        Move();
        CheckDestroy();
    }

    protected virtual void Move()
    {
        direction = ScrollManager.Instance.GetDirection();
        if (direction == 0) return;
        transform.Translate(Vector3.right* moveSpeed * direction * Time.deltaTime);
    }
        
    protected virtual void CheckDestroy()
    {
        if (initialX < 0 && direction == 1 && transform.position.x >= rightLimit)
        {
            Destroy(gameObject);
        }
        else if (initialX > 0 && direction == -1 && transform.position.x <= leftLimit)
        {
            Destroy(gameObject);
        }
    }
}
