using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardGameObject : MonoBehaviour
{
    private Transform _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.forward = _mainCamera.forward;
    }
}
