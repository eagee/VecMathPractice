using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GizmoRadialTrigger : MonoBehaviour
{
    [Range(0, 360)]
    public float angleDeg;
    public int circleDetail = 3;

    [Range(0, 8)]
    public float radius = 1f;

    private const float TAU = 6.28318530718f;

    
    Vector2 AngleToDirection( float angleRad )
    {
        float x = Mathf.Cos(angleRad);
        float y = Mathf.Sin(angleRad);
        return new Vector2(x, y);
    }

    public void OnDrawGizmos()
    {
        float angleRad = angleDeg * Mathf.Deg2Rad;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(AngleToDirection(angleRad)));

        Vector3[] circlePoints = new Vector3[circleDetail];
        for(int i = 0; i < circleDetail; i++)
        {
            float t = i / (float)circleDetail;
            float angleRadian = t * TAU;
            circlePoints[i] = transform.TransformPoint(AngleToDirection(angleRadian) * radius);
            Gizmos.color = Color.HSVToRGB(t, 1, 1);
            Gizmos.DrawSphere(circlePoints[i], 0.25f);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
