using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] Transform canon;
    [SerializeField] GameObject bullet;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] float currentTimer = 0f;
    [SerializeField] float visionDistance = 10f;
    [SerializeField] float visionAngle = 45f;
    [SerializeField] float hitboxOffset = 1.5f;
    Vector2 enemyDirection; 
    [SerializeField] PlayerMove target; 
    private void Start()
    {
        currentTimer = timeBetweenShots;
        target = FindObjectOfType<PlayerMove>(); 
    }
    private void OnTriggerStay(Collider other)
    {
        if (target == null)
            return;
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Hackable") 
            return;

        var targetDir = target.transform.position - canon.position;
        canon.forward = Vector3.MoveTowards(canon.forward, targetDir, 100);
        /*if (canon.localEulerAngles.y < visionAngle || canon.localEulerAngles.y > -visionAngle)*/
        var _checkLOS = Physics.Raycast(canon.position, targetDir, visionDistance);
        Debug.Log(_checkLOS);
        Debug.DrawRay(canon.position, targetDir, Color.red);
        if (currentTimer <= 0f && _checkLOS)
        {
            Fire();
            currentTimer = timeBetweenShots;
        }
        else if (_checkLOS)
            currentTimer -= Time.deltaTime;
        else
            currentTimer = timeBetweenShots;


    }
    private void OnTriggerExit(Collider other)
    {
        currentTimer = timeBetweenShots; 
    }
    public void OrientCanon(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            enemyDirection = Vector2.zero;
        }
        else
        {
            enemyDirection = ctx.ReadValue<Vector2>().normalized;
        }



        if (enemyDirection.x >= 0.9)
        {
            canon.forward = Vector3.MoveTowards(canon.position, Vector3.right, 100);
            canon.localPosition = canon.forward * hitboxOffset;
        }
        else if (enemyDirection.y >= 0.9)
        {
            canon.localPosition = canon.forward * hitboxOffset;
            canon.forward = Vector3.MoveTowards(canon.position, Vector3.up, 100);
        }
        else if (enemyDirection.x <= -0.9)
        {
            canon.localPosition = canon.forward * hitboxOffset;
            canon.forward = Vector3.MoveTowards(canon.position, -Vector3.right, 100);
        }
        else if (enemyDirection.y <= -0.9)
        {
            canon.localPosition = canon.forward * hitboxOffset;
            canon.forward = Vector3.MoveTowards(canon.position, -Vector3.up, 100);
        }
    }
    public void InputFire(InputAction.CallbackContext ctx)
    {
        if (!ctx.started)
            return;
        Fire();
    }
    public void Fire()
    {
        Debug.Log("fire!!!!!!"); 
        Instantiate(bullet, canon.position, canon.rotation); 
    }
}
