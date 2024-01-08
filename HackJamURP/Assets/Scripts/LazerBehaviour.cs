using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[RequireComponent(typeof(SpriteRenderer))]
public class LazerBehaviour : MonoBehaviour { }
/*{
    [SerializeField] Sprite[] lazerSprites;
    [SerializeField] public LazerEnum lazerColor = LazerEnum.Red;

    private void Start()
    {

    }
    public void SwitchSprite(int _lazerColorIndex)
    {
        GetComponent<SpriteRenderer>().sprite = lazerSprites[_lazerColorIndex]; 
    }
}
[CustomEditor(typeof(LazerBehaviour))]
public class LazerBehaviourEditor : Editor
{
    SerializedProperty editorColorEnum;

    private void OnEnable()
    {
        editorColorEnum = serializedObject.FindProperty("lazerColor");
    }
    public override void OnInspectorGUI()
    {
        LazerBehaviour lazer = (LazerBehaviour)target; 
        base.OnInspectorGUI();
        int _newColorEnum = editorColorEnum.enumValueIndex; 
        if(GUILayout.Button("Apply color to Lazer"))
        {
            lazer.SwitchSprite(_newColorEnum); 
        }
    }
}*/