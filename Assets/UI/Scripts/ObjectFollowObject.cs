using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowObject : MonoBehaviour
{
    [SerializeField] GameObject Object;
    [SerializeField] Vector3 HoverDistance = new Vector3(0, 3.5f, 0);
    [SerializeField] [Range(0f, 1f)] float SmoothTime = 0.030f;

    [SerializeField] bool FollowEulerX = false;
    [SerializeField] bool FollowEulerY = true;
    [SerializeField] bool FollowEulerZ = false;

    void LateUpdate()
    {
        Quaternion TargetRotation = Object.transform.rotation;
        
        if (!FollowEulerX) { TargetRotation *= Quaternion.Inverse(GetXAxisRotation(TargetRotation)); }
        if (!FollowEulerY) { TargetRotation *= Quaternion.Inverse(GetYAxisRotation(TargetRotation)); }
        if (!FollowEulerZ) { TargetRotation *= Quaternion.Inverse(GetZAxisRotation(TargetRotation)); }

        Vector3 TargetPosition = Quaternion.Inverse(TargetRotation) * Object.transform.position + HoverDistance;
        TargetPosition = TargetRotation * TargetPosition;

        transform.position = Vector3.Lerp(transform.position, TargetPosition, SmoothTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, SmoothTime);
    }

    // https://forum.unity.com/threads/quaternion-to-remove-pitch.822768/
    Quaternion GetXAxisRotation(Quaternion quaternion)
    {
        float a = Mathf.Sqrt((quaternion.w * quaternion.w) + (quaternion.x * quaternion.x));
        return new Quaternion(x: quaternion.x, y: 0, z: 0, w: quaternion.w / a);

    }
    Quaternion GetYAxisRotation(Quaternion quaternion)
    {
        float a = Mathf.Sqrt((quaternion.w * quaternion.w) + (quaternion.y * quaternion.y));
        return new Quaternion(x: 0, y: quaternion.y, z: 0, w: quaternion.w / a);

    }
    Quaternion GetZAxisRotation(Quaternion quaternion)
    {
        float a = Mathf.Sqrt((quaternion.w * quaternion.w) + (quaternion.z * quaternion.z));
        return new Quaternion(x: 0, y: 0, z: quaternion.z, w: quaternion.w / a);
    }
}
