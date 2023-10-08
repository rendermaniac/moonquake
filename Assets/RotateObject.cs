using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 turnPerSecond;
    public bool loop = true;
    public GameObject spinningObject;

    private void FixedUpdate()
    {
        spinningObject.transform.rotation *= Quaternion.Euler(turnPerSecond * Time.fixedDeltaTime);
    }
}
