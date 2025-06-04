using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopManager2 : MonoBehaviour
{
    public float speed = 10f;
    public bool startFromRight = false;

    private Vector2 moveDirection;

    void Start()
    {
        moveDirection = startFromRight ? Vector2.left : Vector2.right;
    }

    void Update()
    {
        Invoke("Move", 75f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveDirection = -moveDirection;
        }
    }
    void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}