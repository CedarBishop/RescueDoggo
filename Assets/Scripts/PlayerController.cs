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
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private CharacterController cc;

    public LayerMask WorldLayerMask;

    private PlayerInput playerInput;

    private void Awake()
    {
        mainCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        mainCam.Follow = transform;
        //rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        Movement();
        RotateTowardsDirection();
    }

    private void FixedUpdate()
    {
    }

    void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>();
    }

    void Movement()
    {
        cc.Move(Vector3.Normalize((Vector3.right * movementValue.x) + (Vector3.forward * movementValue.y)) * speed * Time.deltaTime);
        cc.Move(Physics.gravity * Time.deltaTime);    
    }

    void RotateTowardsDirection()
    {
        Vector3 direction = Vector3.Normalize((Vector3.right * movementValue.x) + (Vector3.forward * movementValue.y));
        //Debug.DrawLine(transform.position, transform.position + direction, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, WorldLayerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }
        if (movementValue != Vector2.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15F);
        }

        // Rotate the player to the angel of the terrain
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hit.normal), 0.15F);
        //Debug.Log(hit.normal);
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
