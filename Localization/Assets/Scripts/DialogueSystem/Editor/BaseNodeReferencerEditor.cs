using UnityEditor;
using UnityEngine;

[ExecuteAlways]
[CustomEditor(typeof(BaseNodeReferencer))]
public class BaseNodeReferencerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var baseNodeReferencer = (BaseNodeReferencer) target;
        
        DrawDefaultInspector();
        if (baseNodeReferencer.baseNode == null)
            EditorGUILayout.HelpBox("BASE DIALOGUE NODE MUST NOT BE EMPTY.", MessageType.Error);
    }
}