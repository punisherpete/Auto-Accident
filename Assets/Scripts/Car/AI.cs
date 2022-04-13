using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
   
    private void OnCollisionEnter(Collision collision)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            if (hit.collider.TryGetComponent(out Mover mover) && collision.gameObject.TryGetComponent(out Mover _))
            {
                mover.Stop();
            }
        }
    }
}
