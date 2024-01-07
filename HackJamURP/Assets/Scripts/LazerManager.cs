using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerManager : MonoBehaviour
{
    [SerializeField] private LazerBehaviour[] lazersInScene;
    private void Start()
    {
        lazersInScene = FindObjectsOfType<LazerBehaviour>(); 
    }
    public void OnAnyButtonPressed(LazerEnum _buttonColor)
    {
        foreach (LazerBehaviour l in lazersInScene) 
        {
            if (l.lazerColor == _buttonColor)
            {
                l.gameObject.SetActive(false); 
            }
        }
    }
}
