using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public static ScrollManager Instance;

    private int direction = -1;

    public SpriteRenderer[] backgrounds;
    public float speed = 10f;
    private float offset = 24f;
    private float resetPosX = -23.5f;

    public int GetDirection() { return direction; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        ScrollBackgrounds();
    }

    public void SetDirectionForChapter(int chapter)
    {
        direction = (chapter == (int)ChapterType.Chapter2) ? 1 : -1;
    }

    private void ScrollBackgrounds()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position += Vector3.right * direction * speed * Time.deltaTime;

            if (direction == 1)
            {
                if (backgrounds[i].transform.position.x >= -resetPosX)
                {
                    Vector3 newPos = backgrounds[i].transform.position;
                    newPos.x = backgrounds[1 - i].transform.position.x - offset;
                    backgrounds[i].transform.position = newPos;
                }
            }
            else if (direction == -1)
            {
                if (backgrounds[i].transform.position.x <= resetPosX)
                {
                    Vector3 newPos = backgrounds[i].transform.position;
                    newPos.x = backgrounds[1 - i].transform.position.x + offset;
                    backgrounds[i].transform.position = newPos;
                }
            }
        }
    }
}