using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    Rigidbody rb; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*speed, ForceMode.VelocityChange); 
    }

    private void OnTriggerEnter(Collider _other)
    {


        if (_other.gameObject.GetComponentInParent<PlayerMove>()!=null)
        {
            Destroy(_other.gameObject);
            Destroy(gameObject);
            Debug.Log(_other);
        }
        else if (_other.isTrigger)
        {
            Debug.Log("1"); 
            return;
        }
        else
        {
            Debug.Log("3");
            Destroy(gameObject);
        }
        Debug.Log("4");
    }
}
