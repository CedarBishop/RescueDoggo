using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{

    private PlayerInput playerInput;

    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
    }

    void OnMove (InputValue value)
    {
        Vector2 movementValue = value.Get<Vector2>();

    }

    void OnBark()
    {

    }

    void OnInteract ()
    {

    }

    void OnJump ()
    {

    }
}
