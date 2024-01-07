using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof (CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float deadZone = 0.8f;

    private CharacterController controller;
    [SerializeField]private EnemyVisionOrient enemy; 

    private Vector2 inputDir = Vector2.zero;
    private Vector3 FinalDir = Vector3.zero;

    public UnityEvent<Vector2> OnInputDirection;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            inputDir = Vector2.zero;
            return; 
        }
        inputDir = ctx.ReadValue<Vector2>().normalized;
        if (inputDir.x < deadZone && inputDir.x > -deadZone)
        {
            inputDir.x = 0f; 
        }
        if (inputDir.y < deadZone && inputDir.y > -deadZone)
        {
            inputDir.y = 0f;
        }
        //Debug.Log(inputDir); 
        OnInputDirection.Invoke(inputDir); 
    }
    private void Update()
    {
        FinalDir = new Vector3(inputDir.x, 0, inputDir.y)*speed*Time.deltaTime;
        if (!controller.isGrounded)
        {
            FinalDir.y -= gravity;
        }
        else
        {
            FinalDir.y = 0f;
        }
    }

    void FixedUpdate()
    {
        controller.Move(FinalDir); 
    }
}
