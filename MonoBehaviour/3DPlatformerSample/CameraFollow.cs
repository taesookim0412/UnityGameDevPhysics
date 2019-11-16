using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        Vector3 newPos = target.position + offset;
        // Or comment below to remove constant y;
        newPos.y = offset.y;

        transform.position = newPos;
    }
}
