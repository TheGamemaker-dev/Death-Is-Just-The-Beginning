using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetUp : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    CinemachineConfiner confiner;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();

        virtualCamera.Follow = FindObjectOfType<Body>().gameObject.transform;
        confiner.m_BoundingShape2D =
            FindObjectOfType<DynamicConfiner>().gameObject.GetComponent<PolygonCollider2D>();
    }
}
