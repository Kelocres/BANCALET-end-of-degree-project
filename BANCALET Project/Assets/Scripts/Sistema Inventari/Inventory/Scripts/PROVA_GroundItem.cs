using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s

//public class B_ItemProva : MonoBehaviour
public class PROVA_GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public SO_ItemObject item;

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

    public bool RecollirItem(SO_InventoryObject inventari)
    {
        Debug.Log("PROVA_GroundItem RecollirItem()");
        bool delivered = inventari.AddItem(item.data, 1);
        //return inventari.AddItem(item.data, 1);

        //Si s'ha insertat, programar la destrucció de l'objecte
        if (delivered == true)
            Destroy(this.gameObject, 0.1f);
        return delivered;
    }
}
