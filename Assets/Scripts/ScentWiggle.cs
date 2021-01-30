using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScentWiggle : MonoBehaviour
{
    [SerializeField] private float wiggleAmount;
    private float wiggle = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (transform.right * wiggleAmount * Mathf.Sin(Time.time));
    }
}
