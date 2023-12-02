using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;

    void FixedUpdate()
    {
        if(target != null)
        
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * speed);
            Vector3 temp = transform.position;
            temp.z = -10;
            transform.position = temp;
        
        }
        
    }
}
