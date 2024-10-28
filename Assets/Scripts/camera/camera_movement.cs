using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour
{

    public Transform playerTransform;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    public void LateUpdate()
    {
        Vector3 desired = transform.position = playerTransform.position + offset;
        Vector3 smooth = Vector3.Lerp(transform.position, desired, smoothSpeed);
        transform.position = smooth;
    }
}
