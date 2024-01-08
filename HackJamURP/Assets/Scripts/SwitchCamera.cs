using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCamera,hackCamera;
    public void ToPlayerCamera()
    {
        playerCamera.Priority = 11;
        hackCamera.Priority = 9;
    }
    public void ToHackCamera()
    {
        playerCamera.Priority = 9;
        hackCamera.Priority = 11;
    }
}
