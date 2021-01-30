using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBobber : MonoBehaviour
{
    public float speed;
    public float height;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y +  height * Mathf.Sin(Time.time * speed), transform.position.z);
    }
}
