using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject lightning1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹 ��� ���� ���� ����
        if (collision.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            if (!lightning1.activeSelf)
            {
                lightning1.SetActive(true);
            }
        }
    }
}
