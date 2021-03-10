using UnityEngine;

[CreateAssetMenu(fileName = "BaseNodeReferencer", menuName = "Dialogue System/Base Node Referencer")]
public class BaseNodeReferencer : SingletonScriptableObject<BaseNodeReferencer>
{
    public DialogueNode baseNode;

    public static BaseNodeReferencer GetInstance()
    {
        return GetInstance("BaseNodeReferencer");
    }
}
