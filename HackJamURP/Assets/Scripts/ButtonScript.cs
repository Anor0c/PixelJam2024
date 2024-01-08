using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor; 

public class ButtonScript : MonoBehaviour
{
    public UnityEvent<LazerEnum> onButtonPressed;
    [SerializeField] LazerEnum lazerAffected = LazerEnum.Red;
    [SerializeField] Color meshColor = Color.red; 
 
    private void Awake()
    {
        //onButtonPressed.AddListener(FindObjectOfType<LazerManager>().OnAnyButtonPressed); 
    }
    public void ColorMesh(Color _color)
    {
        GetComponent<MeshRenderer>().material.color = _color;
    }
    private void OnTriggerEnter(Collider _collision)
    {
        Debug.Log(_collision);
        if (_collision.gameObject.tag != "Bullet")
            return;
        onButtonPressed.Invoke(lazerAffected);
    }

}
#if UNITY_EDITOR
[CustomEditor(typeof(ButtonScript))]
public class ButtonScriptEditor : Editor
{
    SerializedProperty editorMeshColor;
    private void OnEnable()
    {
        editorMeshColor = serializedObject.FindProperty("meshColor"); 
    }
    public override void OnInspectorGUI()
    {
        ButtonScript button = (ButtonScript)target;
        Color _newMeshColor = editorMeshColor.colorValue; 

        base.OnInspectorGUI();
        if(GUILayout.Button("Apply color to button Mesh"))
        {
            button.ColorMesh(_newMeshColor); 
        }
    }
}

#endif 
