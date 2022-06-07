using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToGroundForwardDirection : MonoBehaviour
{
    public Transform objectToPlace;

    private void OnDrawGizmos()
    {
        Transform tf = this.transform;
        Vector3 rayDirection = tf.forward;
        Vector3 origin = tf.position;
        Ray ray = new Ray(origin, rayDirection);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1.0f);

        if(Physics.Raycast( ray, out RaycastHit hit ))
        {
            // Draw our ray and move our object there
            Gizmos.DrawLine(origin, hit.point);
            objectToPlace.position = hit.point;

            // Get the up direction by obtaining the normal of the hitpoint that we raycast to
            Vector3 upDirection = hit.normal;
            Gizmos.color = Color.green;
            Gizmos.DrawLine( hit.point, hit.point + upDirection*2f);

            // Get the forward direction by taking the cross product of the right vector and the normal of the surface
            Vector3 forwardDirection = Vector3.Cross(transform.right, hit.normal).normalized;
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(hit.point, hit.point + forwardDirection);

            // Get the right direction by taking the cross product of the up and forward directions
            Vector3 rightDirection = Vector3.Cross(upDirection, forwardDirection).normalized;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(hit.point, hit.point + rightDirection);

            //Now we rotate our turret object by the forward and up direction to get it pointing in the right direction!
            Quaternion objRot = Quaternion.LookRotation(forwardDirection, upDirection);
            objectToPlace.rotation = objRot;

            // Draw a line that follows a raycast from the forward direction until it hits something?
            Gizmos.color = Color.magenta;
            Ray newRay = new Ray(hit.point, objectToPlace.forward);
            Physics.Raycast(newRay, out RaycastHit newHit);
            Gizmos.DrawLine(hit.point, newHit.point);
        }
    }
    
}
