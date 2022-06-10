using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LerpBuddy : MonoBehaviour
{
    public Transform a;
    public Transform b;

    //[Range(-1,2)]  - if you want to use LerpUnclamped for linear extrapolation
    [Range(0, 1)]
    public float t = 0f;

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(a.position, b.position);
    }

    void Update()
    {
        Vector3 aPos = a.position;
        Vector3 bPos = b.position;
        transform.position = Vector3.Lerp(aPos, bPos, t);

        Quaternion aRot = a.rotation;
        Quaternion bRot = b.rotation;
        transform.rotation = Quaternion.Slerp(aRot, bRot, t);
    }

}
