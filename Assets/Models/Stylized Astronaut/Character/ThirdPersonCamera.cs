using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform lookAt;
    public Transform camTransform;
    public float distance = 5.0f;

    private void LateUpdate()
    {
        camTransform.position = lookAt.position;
        camTransform.LookAt(lookAt.position);
    }
}