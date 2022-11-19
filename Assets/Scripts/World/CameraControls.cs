using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraControls : MonoBehaviour
{
    public float speed = 5f;
    public float zoomSpeed = 2f;

    void Update()
    {
        Vector3 inputs = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        transform.position += inputs * (speed * Time.deltaTime);
        Debug.Log(Camera.main);
        Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU += (int)(zoomSpeed * Input.mouseScrollDelta.y);
    }
}
