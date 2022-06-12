using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEditor;
using System.Linq;
using System.Collections;

[ExecuteAlways]
public class BezierBuddy : MonoBehaviour
{
    [Range(0, 1)]
    public float t = 0f;
    public Transform[] p;

    void OnDrawGizmos()
    {
        Vector3[] points = p.Select(element => element.position).ToArray();
        Gizmos.DrawLine(points[0], points[1]);
        Gizmos.DrawLine(points[2], points[3]);
        GetBezierPoint(t, points, true);
        DrawBezierCurve(points);        
    }

    private void DrawBezierCurve(Vector3[] points)
    {
        const int DETAIL = 32;
        Vector3[] drawPoints = new Vector3[DETAIL];
        for (int i = 0; i < 32; i++)
        {
            float tDraw = i / (DETAIL - 1f);
            drawPoints[i] = GetBezierPoint(tDraw, points);
        }
        Handles.DrawAAPolyLine(drawPoints);
    }

    static Vector3 GetBezierPoint(float t, Vector3[] points, bool drawGizmos = false)
    {
        Vector3 a = Vector3.Lerp(points[0], points[1], t);
        Vector3 b = Vector3.Lerp(points[1], points[2], t);
        Vector3 c = Vector3.Lerp(points[2], points[3], t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 f = Vector3.Lerp(d, e, t);

        if (drawGizmos)
        { 
            Gizmos.color = Color.white;
            
            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawSphere(a, 0.125f);
            Gizmos.DrawSphere(b, 0.125f);
            Gizmos.DrawSphere(c, 0.125f);
            
            Gizmos.DrawLine(d, e);
            Gizmos.DrawSphere(d, 0.125f);
            Gizmos.DrawSphere(e, 0.125f);
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(f, 0.125f);
        }
        return f;
    }

}
