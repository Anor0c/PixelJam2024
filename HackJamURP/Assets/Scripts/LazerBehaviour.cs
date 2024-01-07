using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class LazerBehaviour : MonoBehaviour
{
    [SerializeField] public LazerEnum lazerColor = LazerEnum.Red;
    [SerializeField] Sprite[] lazerSprites;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = lazerSprites[(int)lazerColor]; 
    }
}
