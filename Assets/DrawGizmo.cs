using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{

    [SerializeField] Color color = Color.red;
    [SerializeField] float radius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
