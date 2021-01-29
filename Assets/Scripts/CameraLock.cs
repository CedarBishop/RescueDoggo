using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLock : MonoBehaviour
{
    private Vector3 lockedPosition;
    private Vector3 lockedRotation;
    private CinemachineVirtualCamera cvc;

    private void Awake()
    {
        lockedPosition = transform.position;
        lockedRotation = new Vector3(transform.eulerAngles.x, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = lockedPosition;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);
        //cvc.GetCinemachineComponent
    }
}
