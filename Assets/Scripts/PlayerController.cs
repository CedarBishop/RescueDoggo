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
    [SerializeField] private Vector3 mainCamZoomOffset;
    public bool transitionCamera = false;
    [SerializeField] private float zoomSpeed;
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
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckRadius;

    // Interact
    private BoxCollider interactionArea;
    [HideInInspector] public List<Transform> interactableObjects;
    [HideInInspector] public List<Transform> barkableObjects;

    public LayerMask WorldLayerMask;

    private PlayerInput playerInput;
    private UIManager uiManager;

    [SerializeField] private ParticleSystem psBark;

    private float yVelocity;
    private bool isGrounded;

    // Animation
    Animator animator;


    private void Awake()
    {
        mainCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        mainCam.Follow = transform;
        cc = GetComponent<CharacterController>();
        interactionArea = GetComponentInChildren<BoxCollider>();
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, WorldLayerMask);
        UpdateYVelocity();

        Movement();

        // Whilst in runtime, allow rotation. You can rotate doggo paused otherwise
        if (Time.timeScale != 0)
        {
            RotateTowardsDirection();
        }
        Vector3 followOffset = mainCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;

        // Smoothly swivel camera towards camera target
        if (transitionCamera)
        {
            mainCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Slerp(followOffset, mainCamZoomOffset, zoomSpeed * Time.deltaTime);
        }
        else
        {
            mainCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Slerp(followOffset, mainCamOffset, zoomSpeed * Time.deltaTime);
        }

        //public void FocusObject(CinemachineVirtualCamera cvc, Vector3 targetPosition)
        //{
        //    cvc.LookAt = gameObject.transform;
        //    cvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset -= targetPosition;       
        
    }



    void UpdateYVelocity()
    {
        if (isGrounded && yVelocity <= 0)
        {
            yVelocity = -2f;
        }

        yVelocity += (-9.81f * gravityMultiplier) * Time.deltaTime;
    }

    void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>();
    }

    void Movement()
    {
        if (movementValue != Vector2.zero)
        {
            // Run Animation
            float temp = Mathf.Lerp(animator.GetFloat("Movement"), 1, 5 * Time.deltaTime);
            animator.SetFloat("Movement", temp);
        }
        else
        {
            // Idle Animation
            float temp = Mathf.Lerp(animator.GetFloat("Movement"), 0, 15 * Time.deltaTime);
            animator.SetFloat("Movement", temp);
        }

        // Static World Based Movement
        //cc.Move(Vector3.Normalize((Vector3.right * movementValue.x) + (Vector3.forward * movementValue.y)) * speed * Time.deltaTime);

        // Camera Relative Movement
        Vector3 camForward = mainCam.transform.forward;
        camForward = new Vector3(camForward.x, 0, camForward.z);

        Vector3 camRight = mainCam.transform.right;
        camRight = new Vector3(camRight.x, 0, camRight.z);

        Vector3 direction = Vector3.Normalize((camRight * movementValue.x) + (camForward * movementValue.y));
        cc.Move(direction * speed * Time.deltaTime);


        // Jump Velocity
        cc.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);    
    }

    void RotateTowardsDirection()
    {

        // Static World Rotation
        //Vector3 direction = Vector3.Normalize((Vector3.right * movementValue.x) + (Vector3.forward * movementValue.y));

        // Camera Relative Rotation
        Vector3 camForward = mainCam.transform.forward;
        camForward = new Vector3(camForward.x, 0, camForward.z);

        Vector3 camRight = mainCam.transform.right;
        camRight = new Vector3(camRight.x, 0, camRight.z);

        Vector3 direction = Vector3.Normalize((camRight * movementValue.x) + (camForward * movementValue.y));


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
        animator.SetTrigger("Bark");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Dog/bark", GetComponent<Transform>().position);

        Transform closestObject = null;
        float distance = Mathf.Infinity;
        foreach (Transform t in barkableObjects)
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

    void OnInteract()
    {
        Transform closestObject = null;
        float distance = Mathf.Infinity;
        if (interactableObjects == null || interactableObjects.Count == 0)
        {
            foreach (Waypoints waypoint in FindObjectsOfType<Waypoints>())
            {
                if (Vector3.Distance(transform.position, waypoint.transform.position) < distance)
                {
                    closestObject = waypoint.transform;
                }
            }

            if (closestObject != null)
            {
                closestObject.GetComponent<Interactables>().Interact();
                animator.SetTrigger("Sniff");
                if (closestObject.GetComponent<DialoguePrompt>())
                {
                    //mainCam.LookAt
                    // Zoom camera to look at object
                    transitionCamera = !transitionCamera;
                    if (transitionCamera == false)
                    {
                        closestObject.GetComponent<DialoguePrompt>().dialogueAlert.SetActive(true);
                    }
                }
            }
        }
        else
        {
            foreach (Transform t in interactableObjects)
            {
                if (Vector3.Distance(transform.position, t.position) < distance)
                {
                    closestObject = t;
                }
            }
            if (closestObject != null)
            {
                if (closestObject.GetComponent<Waypoints>())
                {
                    animator.SetTrigger("Sniff");
                }
                closestObject.GetComponent<Interactables>().Interact();
                if (closestObject.GetComponent<DialoguePrompt>())
                {
                    //mainCam.LookAt
                    // Zoom camera to look at object
                    transitionCamera = !transitionCamera;
                    if (transitionCamera == false)
                    {
                        closestObject.GetComponent<DialoguePrompt>().dialogueAlert.SetActive(true);
                    }
                }
            }
        }
       
    }

    void OnJump()
    {
        if (isGrounded)
        {
            yVelocity = (jumpHeight * -2 * (Physics.gravity.y * gravityMultiplier));

            animator.SetTrigger("Jump");
        }
    }

    void OnPause()
    {
        uiManager.TogglePause();
    }
}
