using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class EnemyHackedBehaviour : MonoBehaviour
{
    [SerializeField] bool isConeVision = false; 

    private PlayerInput enemyInputComponent;
    private HackerInput enemyInputAsset; 
    private PlayerMove enemyMove;
    private InputAction moveAction, hackAction, fireAction;
    [SerializeField] EnemyVisionOrient visionOrient; 
    [SerializeField] EnemyVision vision; 
    public UnityEvent OnUnHack; 

    private void Awake()
    {
        enemyInputAsset = new HackerInput();
        moveAction = enemyInputAsset.Keyboard.Move; 
        hackAction = enemyInputAsset.Keyboard.Hack; 
        fireAction = enemyInputAsset.Keyboard.Shoot; 
    }
    private void Start()
    {
        vision = GetComponentInChildren<EnemyVision>(); 
        if (isConeVision)
        {
            visionOrient = GetComponentInChildren<EnemyVisionOrient>(); 
        } 
    }
    public void EnemyHacked()
    {
        if (TryGetComponent(out PlayerInput input))
            return;
        enemyInputComponent = gameObject.AddComponent(typeof(PlayerInput)) as PlayerInput;
        enemyInputComponent.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        enemyInputAsset.Enable();
        enemyMove = gameObject.AddComponent(typeof(PlayerMove)) as PlayerMove;
        moveAction.started += enemyMove.OnMove;
        moveAction.performed += enemyMove.OnMove;
        moveAction.canceled += enemyMove.OnMove;
        moveAction.started += vision.OrientCanon;
        moveAction.performed += vision.OrientCanon;
        moveAction.canceled += vision.OrientCanon;

        if (isConeVision)
        {
            moveAction.started += visionOrient.OrientVision;
            moveAction.performed += visionOrient.OrientVision;
            moveAction.canceled += visionOrient.OrientVision;

        }
        hackAction.started += EnemyUnHacked;
        fireAction.started += vision.InputFire; 

    }
    public void EnemyUnHacked(InputAction.CallbackContext ctx)
    {
        OnUnHack.Invoke(); 
        enemyInputAsset.Disable(); 
        if (enemyInputComponent != null)
            Destroy(GetComponent<PlayerInput>());
        if (enemyMove != null)
            Destroy(GetComponent<PlayerMove>()); 
    }
    
}
