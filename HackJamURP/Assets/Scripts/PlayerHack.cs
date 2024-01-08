using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerHack : MonoBehaviour
{
    [SerializeField] private float hitboxOffset = 1.0f;
    [SerializeField] private bool isTryHack = false;
    [SerializeField] private bool isHackSuccess = false;
    [SerializeField] private SwitchCamera cameraSwitch; 

    public UnityEvent OnHackSuccessful;
    private void Start()
    {
        cameraSwitch = FindObjectOfType<SwitchCamera>(); 
    }
    public void OnHack(InputAction.CallbackContext ctx)
    {
        if (isHackSuccess)
        {
            isTryHack = false; 
            return; 
        }
        if (ctx.performed)
        {
            isTryHack = true;
        }
        else
            isTryHack = false; 

    }
    public void OnUnHack()
    {
        isHackSuccess = false; 
    }
    public void HitboxOrientation(Vector2 _inputDir)
    {
        var _hitboxOrientation = Vector3.zero; 
        if (_inputDir == Vector2.zero)
            _hitboxOrientation = Vector3.forward;
        else 
         _hitboxOrientation = new Vector3(_inputDir.x, 0, _inputDir.y);

        transform.localPosition = _hitboxOrientation * hitboxOffset; 
    }
    private void OnTriggerStay(Collider _other)
    {
        if (!isTryHack)
            return;
        else if (_other.gameObject.TryGetComponent(out EnemyHackedBehaviour _enemyHacked)) 
        {
            Debug.Log("hack" + _enemyHacked);
            OnHackSuccessful.AddListener(cameraSwitch.ToHackCamera); 
            OnHackSuccessful.Invoke();
            isHackSuccess = true; 
        } 
    }
}
    

