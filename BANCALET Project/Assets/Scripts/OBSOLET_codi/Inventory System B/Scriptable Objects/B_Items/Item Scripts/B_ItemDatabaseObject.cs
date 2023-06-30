using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=232EqU1k9yQ      // Tutorial 2
//REF: https://www.youtube.com/watch?v=LcizwQ7ogGA&t=46s // Tutorial 3

//Com Unity no serialitza diccionaries, s'utilitzarà una funció callback per a crear
//el diccionari (ISerializationCallbackReceiver)
//S'han d'aplicar obligatoriament les funcions OnAfterDeserialize() i OnBeforeSerialize()
[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System B/Items/Database")]
public class B_ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    //Llista de tots els objectes que existixen en el joc
    public B_ItemObject[] Items;

    //Per a importar els items i recuperar fàcilment el id de cada item (la id es un int)
    //public Dictionary<B_ItemObject, int> GetId = new Dictionary<B_ItemObject, int>(); // En tutorial 3 no fa falta
    //Per a recuperar els items a partir de la seua id (diccionari duplicat, suposa un
    //cost extra de memòria a canvi de millora computacional)
    //public Dictionary<int, B_ItemObject> GetItem = new Dictionary<int, B_ItemObject>();
    [ContextMenu("Update ID's")]
    public void UpdateID()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            //GetId.Add(Items[i], i);
            //Items[i].Id = i; //Tutorial 3
            if(Items[i].data.Id != i) //Tutorial 7 h 1 minut 3
                Items[i].data.Id = i; //Tutorial 7
            //GetItem.Add(i, Items[i]);

        }
    }

    public void OnAfterDeserialize()
    {
        //throw new System.NotImplementedException();

        //Es torna a crear el diccionari, per assegurar-se de que no dupliquem
        //GetId = new Dictionary<B_ItemObject, int>();
        //GetItem = new Dictionary<int, B_ItemObject>(); // En Tutorial 3 es canvia de posició
        UpdateID();
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();

        //Abans de serialitzar, buida els items i després canvia els seus IDs
        //GetItem = new Dictionary<int, B_ItemObject>();        
    }



}
