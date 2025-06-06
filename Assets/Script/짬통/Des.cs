using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [Tooltip("대칭 기준으로 삼을 카메라. 비워두면 메인 카메라 사용")]
    public Camera referenceCamera;

    void Start()
    {
        if (referenceCamera == null)
            referenceCamera = Camera.main;

        Vector3 objPos = transform.position;
        float camX = referenceCamera.transform.position.x;

        float mirroredX = 2 * camX - objPos.x;
        Vector3 mirroredPos = new Vector3(mirroredX, objPos.y, objPos.z);

        Debug.Log($"[{gameObject.name}] X축 대칭 위치: {mirroredPos}");
    }
}
