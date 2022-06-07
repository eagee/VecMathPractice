using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

/// <summary>
///  This is a C# extension method that extends behavior in the Vector2 class
/// </summary>
public static class Vector2ExtensionMethods
{
    public static Vector2 Rotate90CW(this Vector2 v)
    {
        return new Vector2(v.y, -v.x);
    }

    public static Vector2 Rotate90CCW(this Vector2 v)
    {
        return new Vector2(-v.y, v.x);
    }

    public static Vector2 Rotate180(this Vector2 v)
    {
        return new Vector2(-v.x, -v.y);
    }

    public static Vector2 GetTopDown2DVector(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }
}

public class DotProductLookAt : MonoBehaviour
{
    public enum LookType
    {
        LookAtPlayer,
        LookLeftOrRightOfPlayer
    }
    
    public Transform player;
    public float threshold = 0.1f;
    public LookType playerLookType = LookType.LookAtPlayer;

    void OnDrawGizmos()
    {
        Vector3 posTrigger = transform.position;
        Vector3 posPlayer = player.position;
        Vector3 playerLookDir = player.forward;

        Vector3 playerToTrigger = posTrigger - posPlayer;
        Vector3 playerToTriggerDir = playerToTrigger.normalized;


        bool isValidLook = false;
        if (playerLookType == LookType.LookAtPlayer)
        {
            // if this is 1, the player is looking straight at it
            // if this is 0, the player is looking exactly sideways from the trigger
            // if this is -1, the player is looking straight away from it
            float lookTowardNess = Vector3.Dot(playerLookDir, playerToTriggerDir);
            isValidLook = (lookTowardNess >= threshold);

            
        }
        else // (playerLookType == LookType.LookLeftOrRightOfPlayer)
        {
            // This commented code is a left and right check using 2d space and the dot product
            // Set up a top down projected space using X and Z (since Y is up)
            // Vector2 triggerPosXZ = transform.position.GetTopDown2DVector();
            // Vector2 playerPosXZ = player.transform.position.GetTopDown2DVector();
            // Vector2 playerLookDirXZ = player.forward.GetTopDown2DVector().normalized;
            // Vector2 directionPlayerToTrigger = (triggerPosXZ - playerPosXZ).normalized;
            // Since we're checking whether the player is looking to the left or right, we'll get a perpendicular look direction
            // by doing a fast rotate by 90 degrees and using that vector
            // Vector2 playerPerpendicularLookDir = playerLookDirXZ.Rotate90CW();
            // float leftandrightness = Vector2.Dot(playerPerpendicularLookDir, directionPlayerToTrigger);
            // isValidLook = leftandrightness < 0f;
            // Render the perpendicular direction as a flashing blue line
            //Gizmos.color = Random.ColorHSV(0.5f, 0.6f, 1f, 1f, 0.5f, 1f); ;
            //Vector3 playerPerpendicularLookWorldSpace =
            //    new Vector3(playerPerpendicularLookDir.x, 0f, playerPerpendicularLookDir.y);
            //Gizmos.DrawLine(posPlayer, posPlayer + playerPerpendicularLookWorldSpace * 4f);

            // This code is a left and right check using 3d space and the cross product, which has some 3d edge cases (e.g. angles, upside down, etc)
            // but works if things are positioned reasonably
            // We just evaluate the y value of the cross product to determine whether we're to the left or right
            // < 0f means we're to the, "right" and >= 0f means we're to the, "left"
            float leftandrightness = Vector3.Cross(playerLookDir, playerToTriggerDir).y; 
            isValidLook = leftandrightness < 0f;
        }

        // Always render the objects look direction as a flashing line
        Gizmos.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); ;
        Gizmos.DrawLine(posPlayer, posPlayer + playerLookDir * 4f);


        Gizmos.color = isValidLook ? Color.green : Color.red;
        Gizmos.DrawLine(posPlayer, posPlayer + playerToTriggerDir * 4f);
        Gizmos.color = Color.white;

        Gizmos.DrawSphere(this.transform.position, 1.05f);
    }
}
