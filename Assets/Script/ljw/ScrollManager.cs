using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public static ScrollManager Instance;

    public int Direction = -1;
    public int CurrentChapter = 1;

    public SpriteRenderer[] backgrounds;
    public float speed = 10f;
    private float offset = 24f;
    private float resetPosX = -23.5f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        ScrollBackgrounds();
    }

    private void ScrollBackgrounds()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position += Vector3.right * Direction * speed * Time.deltaTime;

            if (Direction == 1)
            {
                if (backgrounds[i].transform.position.x >= -resetPosX)
                {
                    Vector3 newPos = backgrounds[i].transform.position;
                    newPos.x = backgrounds[1 - i].transform.position.x - offset;
                    backgrounds[i].transform.position = newPos;
                }
            }
            else if (Direction == -1)
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

    public void ChangeChapter(int newChapter)
    {
        CurrentChapter = newChapter;
        Direction = (newChapter == 2) ? 1 : -1;
    }

    public void FlipDirection()
    {
        Direction = -Direction;
    }

    public void SetDirection(int newDirection)
    {
        Direction = newDirection;
    }
}

