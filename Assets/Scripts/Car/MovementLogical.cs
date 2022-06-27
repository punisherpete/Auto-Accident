using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Mover))]
public class MovementLogical : MonoBehaviour
{
    private Mover _mover;
    private bool _isCarInFrontDetected;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(transform.position.x,transform.position.y+1f,transform.position.z), transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.TryGetComponent(out Mover _))
                _isCarInFrontDetected = true;
            else
                _isCarInFrontDetected = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {    
        if(collision.gameObject.TryGetComponent(out Mover _) && _isCarInFrontDetected)
            _mover.PauseMoving(0.3f);
    }
}
