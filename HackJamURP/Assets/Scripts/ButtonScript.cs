using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ButtonScript : MonoBehaviour
{
    [SerializeField] LazerEnum color = LazerEnum.Red;
    [SerializeField] Color meshColor = Color.red; 
    public UnityEvent<LazerEnum> onButtonPressed;
    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = meshColor;   
    }
    private void OnTriggerEnter(Collider _collision)
    {
        Debug.Log(_collision);
        if (_collision.gameObject.tag != "Bullet")
            return;
        onButtonPressed.Invoke(color);
    }

}
