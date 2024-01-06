using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerHack : MonoBehaviour
{
    [SerializeField] private float hitboxOffset = 1.0f;
    [SerializeField]private bool isTryHack = false; 
    [SerializeField]private bool isHackSuccess = false; 

    public UnityEvent OnHackSuccessful;
 
    public void OnHack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isTryHack = !isTryHack; 
        }


        Debug.Log(isTryHack); 
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
        Debug.Log(_other);

        if (!isTryHack)
            return;
        else if (_other.gameObject.TryGetComponent(out EnemyHackedBehaviour _enemyHacked)) 
        {
            OnHackSuccessful.AddListener(_enemyHacked.EnemyyHacked);
            Debug.Log("hack" + _enemyHacked); 
            OnHackSuccessful.Invoke();
            isTryHack = false;
            isHackSuccess = true; 
        } 
    }
}
    

