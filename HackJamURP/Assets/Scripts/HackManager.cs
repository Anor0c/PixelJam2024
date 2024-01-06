using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackManager : MonoBehaviour
{
    [SerializeField]EnemyHackedBehaviour[] enemies; 
    void Start()
    {
        enemies = FindObjectsByType<EnemyHackedBehaviour>(FindObjectsSortMode.None); 
    }

    public void onAnyEnemyHacked()
    {
        foreach (EnemyHackedBehaviour _enemy in enemies)
        {
            _enemy.EnemyyHacked(); 
        }
    }
}
