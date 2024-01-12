using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ChecckPlayerDeath : MonoBehaviour
{
    PlayerMove player; 
    public UnityEvent OnPlayerDied; 
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            return;
        OnPlayerDied.Invoke();
        Destroy(this); 
    }
}
