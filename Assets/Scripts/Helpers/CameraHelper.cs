using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    [SerializeField] private Transform followTransform;

    private void FixedUpdate()
    {
        transform.localPosition = new Vector3(followTransform.localPosition.x, followTransform.localPosition.y, transform.localPosition.z);
    }
}
