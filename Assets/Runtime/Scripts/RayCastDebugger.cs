using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDebugger : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1;
    [SerializeField] private LayerMask collisionMask;
    private Vector3 RayOrigin => transform.position;
    private Vector3 RayDir => transform.forward;


    private void FixedUpdate()
    {
        Debug.DrawRay(RayOrigin, RayDir * rayDistance, Color.green, 0);
        if(Physics.Raycast(RayOrigin, RayDir, out var hit, rayDistance, collisionMask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(RayOrigin, hit.point, Color.red);
            Debug.Log(hit.collider.name);
        }
    }

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(RayOrigin, RayDir * rayDistance);
        }
    }
}
