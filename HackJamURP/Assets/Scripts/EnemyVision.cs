using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] Transform canon;
    [SerializeField] GameObject bullet;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] float currentTimer = 0f;
    [SerializeField] float visionDistance = 10f;
    [SerializeField] float visionAngle = 45f;
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
    public void Fire()
    {
        Instantiate(bullet, canon.position, canon.rotation); 
    }
}
