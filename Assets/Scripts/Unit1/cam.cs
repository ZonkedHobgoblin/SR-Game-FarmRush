using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public GameObject player;
    private Vector3 offset = new Vector3(0, 5, -7);
    void LateUpdate()
    {
        //update cam pos
        transform.position = player.transform.position + offset;
    }

}
