using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraControls : MonoBehaviour
{
    public float speed = 5f;
    public float zoomSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        Vector3 inputs = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        transform.position += inputs * (speed * Time.deltaTime);
        Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU += (int)(zoomSpeed * Input.mouseScrollDelta.y);
    }
}
