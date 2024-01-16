using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class DoorScript : MonoBehaviour
{
    SceneScript sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneScript>(); 
    }

    private void OnTriggerEnter(Collider _collision)
    {

        if (!_collision.gameObject.CompareTag("Player"))
            return;
        sceneManager.ToNextScene(); 
    }
}
