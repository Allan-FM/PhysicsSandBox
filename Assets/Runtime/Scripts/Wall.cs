using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float repelFoce = 1;
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.TryGetComponent<AirPlaneMovement>(out var airPlaneMovement) &&
            other.gameObject.TryGetComponent<Rigidbody>(out var rb))
        {
            for (int i = 0; i < other.contactCount; i++)
            {
                var contact = other.GetContact(i);
                rb.AddForceAtPosition(-contact.normal * repelFoce, contact.point);

            }
        }
    }
}
