using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=232EqU1k9yQ      // Tutorial 2
//REF: https://www.youtube.com/watch?v=LcizwQ7ogGA&t=46s // Tutorial 3

//Com Unity no serialitza diccionaries, s'utilitzarà una funció callback per a crear
//el diccionari (ISerializationCallbackReceiver)
//S'han d'aplicar obligatoriament les funcions OnAfterDeserialize() i OnBeforeSerialize()
[CreateAssetMenu(fileName = "Nova Base de dades", menuName = "Sistema Inventari/Items/Base de dades")]
public class SO_ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    //Llista de tots els objectes que existixen en el joc
    public SO_ItemObject[] Items;

    [ContextMenu("Actualitza IDs")]
    public void UpdateID()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            
            if(Items[i].data.Id != i) //Tutorial 7 h 1 minut 3
                Items[i].data.Id = i; //Tutorial 7
            

        }
    }

    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();

        
    }



}
