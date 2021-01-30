using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class PlayerController : MonoBehaviour
{

    // Camera
    [SerializeField] private CinemachineVirtualCamera mainCam;
    [SerializeField] private Vector3 mainCamOffset;
    [SerializeField] private float mainCamLookDistance;
    [SerializeField] private Transform mainCamLookTarget;

    // Movement
    private Vector2 movementValue;
    [SerializeField] private float speed;
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private CharacterController cc;

    // Jump
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityMultiplier;

    // Interact
    private BoxCollider interactionArea;
    [HideInInspector] public List<Transform> interactableObjects;

    public LayerMask WorldLayerMask;

    private PlayerInput playerInput;
    private UIManager uiManager;

    [SerializeField] private ParticleSystem psBark;

    private void Awake()
    {
        mainCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        mainCam.Follow = transform;
        cc = GetComponent<CharacterController>();
        interactionArea = GetComponentInChildren<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

    }

    private void Update()
    {
            Movement();

        // Whilst in runtime, allow rotation. You can rotate doggo paused otherwise
        if (Time.timeScale != 0)
        {
            RotateTowardsDirection();
        }
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
        //cc.Move(Physics.gravity * Time.deltaTime);    
    }

    void RotateTowardsDirection()
    {
        Vector3 direction = Vector3.Normalize((Vector3.right * movementValue.x) + (Vector3.forward * movementValue.y));
        //Debug.DrawLine(transform.position, transform.position + direction, Color.red);

        if (movementValue != Vector2.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.05F);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, WorldLayerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }


        // Rotate the player to the angel of the terrain
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hit.normal), 0.15F);
        //Debug.Log(hit.normal);
    }

    void OnBark()
    {
        psBark.Play();
    }

    void OnInteract()
    {
        Transform closestObject = null;
        float distance = Mathf.Infinity;
        foreach(Transform t in interactableObjects)
        {
            if (Vector3.Distance(transform.position, t.position) < distance)
            {
                closestObject = t;
            }
        }
        if (closestObject != null)
        {
            closestObject.GetComponent<Interactables>().Interact();
        }
    }

    void OnJump()
    {
        cc.Move(Vector3.up * jumpHeight * Time.deltaTime);
    }

    void OnPause()
    {
        uiManager.TogglePause();
    }
}
