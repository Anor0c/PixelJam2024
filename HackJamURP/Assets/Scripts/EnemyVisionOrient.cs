using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using KevinCastejon.ConeMesh;  

public class EnemyVisionOrient : MonoBehaviour
{
    Cone visionCone;
    Vector2 enemyDirection = Vector2.zero; 
    private void Start()
    {
        visionCone = GetComponent<Cone>(); 
    }

    public void OrientVision(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            enemyDirection = Vector2.zero; 
        }
        else
        {
            enemyDirection = ctx.ReadValue<Vector2>().normalized; 
        }
        if (enemyDirection.x >= 0.9)
        {
            visionCone.Orientation = ConeOrientation.X;
            visionCone.InvertDirection = false;
        }
        else if (enemyDirection.y >= 0.9)
        {
            visionCone.Orientation = ConeOrientation.Z;
            visionCone.InvertDirection = false;
        }
        else if (enemyDirection.x <= -0.9)
        {
            visionCone.Orientation = ConeOrientation.X;
            visionCone.InvertDirection = true; 
        }
        else if (enemyDirection.y <= -0.9)
        {
            visionCone.Orientation = ConeOrientation.Z;
            visionCone.InvertDirection = true;
        }
    }
}
