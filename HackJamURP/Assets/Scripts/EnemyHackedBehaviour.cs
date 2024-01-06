using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyHackedBehaviour : MonoBehaviour
{
    private PlayerInput enemyInputComponent;
    private HackerInput enemyInputAsset; 
    private PlayerMove enemyMove;
    private InputAction moveAction, hackAction; 

    [SerializeField] InputActionAsset hackerActionAsset;
    private void Awake()
    {
        enemyInputAsset = new HackerInput();
        moveAction = enemyInputAsset.Keyboard.Move; 
        hackAction = enemyInputAsset.Keyboard.Hack; 
    }
    public void EnemyyHacked()
    {
        enemyInputComponent= gameObject.AddComponent(typeof(PlayerInput)) as PlayerInput;
        enemyInputComponent.actions = hackerActionAsset; 
        enemyInputComponent.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        enemyInputAsset.Enable(); 
        enemyMove = gameObject.AddComponent(typeof (PlayerMove)) as PlayerMove;
        moveAction.started += enemyMove.OnMove;
        moveAction.performed += enemyMove.OnMove;
        moveAction.canceled += enemyMove.OnMove;
        hackAction.started += EnemyUnHacked; 
    }
    public void EnemyUnHacked(InputAction.CallbackContext ctx)
    {
        enemyInputAsset.Disable(); 
        if (enemyInputComponent != null)
            Destroy(GetComponent<PlayerInput>());
        if (enemyMove != null)
            Destroy(GetComponent<PlayerMove>()); 
    }
    
}
