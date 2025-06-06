using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [Tooltip("��Ī �������� ���� ī�޶�. ����θ� ���� ī�޶� ���")]
    public Camera referenceCamera;

    void Start()
    {
        if (referenceCamera == null)
            referenceCamera = Camera.main;

        Vector3 objPos = transform.position;
        float camX = referenceCamera.transform.position.x;

        float mirroredX = 2 * camX - objPos.x;
        Vector3 mirroredPos = new Vector3(mirroredX, objPos.y, objPos.z);

        Debug.Log($"[{gameObject.name}] X�� ��Ī ��ġ: {mirroredPos}");
    }
}
