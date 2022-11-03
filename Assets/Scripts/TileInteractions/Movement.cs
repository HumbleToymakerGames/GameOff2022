using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilePos = GetComponent<MouseTileSelect>().GetSelectedTilePosition();
        if (tilePos != null)
        {
            Debug.Log(((transform.position + new Vector3(0, transform.localScale.y, 0)) - (tilePos + new Vector3(0, transform.localScale.y, 0))));
            //transform.position = tilePos + new Vector3(0, transform.localScale.y, 0);
            transform.position -= ((transform.position) - (tilePos + new Vector3(0, transform.localScale.y, 0))) * (speed * Time.deltaTime);
        }
    }
}
