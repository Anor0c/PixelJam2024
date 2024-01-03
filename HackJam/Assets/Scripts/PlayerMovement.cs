using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private  float speed = 1.0f;

    private Vector2 inputDirection = Vector2.zero;
    private Vector3 finalPlayerDirection = Vector3.zero;
    private Quaternion playerRotation = Quaternion.identity; 

    private CharacterController playerController; 

    void Start()
    {
        playerController = GetComponent<CharacterController>(); 
    }

    public void OnMove(InputAction.CallbackContext _ctx)
    {
        if (!_ctx.performed)
        {
            inputDirection = Vector2.zero;
            return; 
        }
        inputDirection = _ctx.ReadValue<Vector2>(); 
    }
    public void OnRotate (InputAction.CallbackContext _obj)
    {
        if (_obj.performed)
            return;
        var _inputRotation = new Vector3(0,_obj.ReadValue<Vector3>().y,0);
        playerRotation = Quaternion.Euler(_inputRotation * Time.deltaTime);
    }
    private void Update()
    {
        finalPlayerDirection = new Vector3(inputDirection.x, 0, inputDirection.y) * speed;
        transform.rotation = playerRotation; 
    }
    void FixedUpdate()
    {
        playerController.Move(finalPlayerDirection); 
    }
}
