using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCam;
    [SerializeField] private Vector3 mainCamOffset;
    [SerializeField] private float mainCamLookDistance;
    [SerializeField] private Transform mainCamLookTarget;

    public Vector2 movementValue;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;

    private PlayerInput playerInput;

    private void Awake()
    {
        mainCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        mainCam.Follow = transform;
        rb = GetComponent<Rigidbody>();
    }
    //private void OnEnable()
    //{
    //    playerInput.enabled = true;
    //}

    //private void OnDisable()
    //{
    //    playerInput.enabled = false;
    //}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //mainCam.transform.position = transform.position + mainCamOffset;
        //mainCam.tar
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>();
        //Debug.Log(movementValue);
    }

    void Movement()
    {
        if (movementValue != Vector2.zero)
        {
            //rb.velocity = ((transform.right * movementValue.x) + (transform.forward * movementValue.y)) * speed;
            rb.velocity = ((Vector3.right * movementValue.x) + (Vector3.forward * movementValue.y)) * speed;
        }
        //Debug.Log(rb.velocity);
    }

    void OnBark()
    {

    }

    void OnInteract()
    {

    }

    void OnJump()
    {

    }
}
