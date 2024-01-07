using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHackedBehaviour : MonoBehaviour
{
    [SerializeField] bool isConeVision = false; 

    private PlayerInput enemyInputComponent;
    private HackerInput enemyInputAsset; 
    private PlayerMove enemyMove;
    private InputAction moveAction, hackAction;
    [SerializeField] EnemyVisionOrient vision; 
    [SerializeField] SwitchCamera cameraSwitch; 

    private void Awake()
    {
        enemyInputAsset = new HackerInput();
        moveAction = enemyInputAsset.Keyboard.Move; 
        hackAction = enemyInputAsset.Keyboard.Hack; 
    }
    private void Start()
    {
        if (isConeVision)
        {
            vision = GetComponentInChildren<EnemyVisionOrient>(); 
        }
        cameraSwitch = GetComponent<SwitchCamera>(); 
    }
    public void EnemyHacked()
    {
        if (TryGetComponent(out PlayerInput input))
            return; 
        enemyInputComponent= gameObject.AddComponent(typeof(PlayerInput)) as PlayerInput; 
        enemyInputComponent.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        enemyInputAsset.Enable(); 
        enemyMove = gameObject.AddComponent(typeof (PlayerMove)) as PlayerMove;
        moveAction.started += enemyMove.OnMove;
        moveAction.performed += enemyMove.OnMove;
        moveAction.canceled += vision.OrientVision;
        if (isConeVision)
        {
            moveAction.started += vision.OrientVision;
            moveAction.performed += vision.OrientVision;
            moveAction.canceled += enemyMove.OnMove;
        }
        hackAction.started += EnemyUnHacked;
    }
    public void EnemyUnHacked(InputAction.CallbackContext ctx)
    {
        cameraSwitch.ToPlayerCamera();
        enemyInputAsset.Disable(); 
        if (enemyInputComponent != null)
            Destroy(GetComponent<PlayerInput>());
        if (enemyMove != null)
            Destroy(GetComponent<PlayerMove>()); 
    }
    
}
