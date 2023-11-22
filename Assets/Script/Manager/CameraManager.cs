using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float xScrollMin;
    [SerializeField] private float xScrollMax;

    private Transform cam;

    public static CameraManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        cam = Camera.main.transform;
    }

    public void Scroll(float x)
    {
        cam.position += new Vector3(x * scrollSpeed, 0, 0);

        if (cam.position.x <= xScrollMin && x < 0) cam.position = new Vector3(xScrollMin, cam.position.y, cam.position.z);
        if (cam.position.x >= xScrollMax && x > 0) cam.position = new Vector3(xScrollMax, cam.position.y, cam.position.z);
    }
}
