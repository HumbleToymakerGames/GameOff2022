using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public float mSpeed = 5f;

    //All testing don't worry
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            //move up and left (1, 0.5)
            transform.position += new Vector3(0.5f, 0.25f, 0);
            up = false;
        }
        if (down)
        {
            //move up and left (1, 0.5)
            transform.position += new Vector3(-0.5f, -0.25f, 0);
            down = false;
        }
        if (left)
        {
            //move up and left (1, 0.5)
            transform.position += new Vector3(-0.5f, 0.25f, 0);
            left = false;
        }
        if (right)
        {
            //move up and left (1, 0.5)
            transform.position += new Vector3(0.5f, -0.25f, 0);
            right = false;
        }

    }
}
