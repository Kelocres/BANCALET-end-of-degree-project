using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s

//public class B_ItemProva : MonoBehaviour
public class B_GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public B_ItemObject item;

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }
}
