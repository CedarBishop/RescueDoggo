using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform target;
    private void Start()
    {
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            transform.position = hit.point;
            transform.forward = (new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position).normalized;
        }
    }
}
