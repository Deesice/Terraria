using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform == null)
            return;

        transform.position = target.position + offset;        
    }
}
