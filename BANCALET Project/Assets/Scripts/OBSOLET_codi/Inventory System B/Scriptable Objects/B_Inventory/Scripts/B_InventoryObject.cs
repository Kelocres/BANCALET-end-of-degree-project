using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s // Tutorial 1
//REF: https://www.youtube.com/watch?v=232EqU1k9yQ       // Tutorial 2
//REF: https://www.youtube.com/watch?v=LcizwQ7ogGA&t=46s // Tutorial 3


//Per a repoblar l'objete a partir de la base de dades, la classe deu heredar de ISerializationCallbackReceiver

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System B/Inventory")]
//public class B_InventoryObject : ScriptableObject, ISerializationCallbackReceiver En Tutorial 3 no cal el ISerialiblabla
public class B_InventoryObject : ScriptableObject 
{
    //Camí on es guardarà la informació
    public string savePath;
    
    //Variable per a la base de dades //Tutorial 2
    public  B_ItemDatabaseObject database;

    // Tutorial 1
    // En Tutorial 3 es eliminat i es crea la classe B_Inventory
    //public List<B_InventorySlot> Container = new List<B_InventorySlot>();
    public B_Inventory Container;

    //Per a evitar errors i que el codi es trenque // Tutorial 1
    //Ja no serà necessari, al ser pública la base de dades // Tutorial 3
    /*
    private void OnEnable()
    {

#if UNITY_EDITOR
        //Per al build, la línia de AssetDatabase no funciona, ja que funciona només amb l'arquitectura d'arxius de l'editor Unity
        //database = (B_ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Personatges_codi/Inventory System B/Scriptable Objects/B_Items/Database.asset", typeof(B_ItemDatabaseObject));
        database = (B_ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/B_Database.asset", typeof(B_ItemDatabaseObject));
#else
        database = Resources.Load<B_ItemDatabaseObject>("B_Database");
#endif

    }*/

    /*public void AddItem(B_Item _item, int _amount)
    {
        //Comprovar si l'item te buffs // Tutorial 3
        // Si en té, es afegix en un altre slot en volta de apilarlo amb altres amb
        // diferents buffs
        if(_item.buffs.Length > 0)
        {
            Container.Items.Add(new B_InventorySlot(_item.Id, _item, _amount));
            return;
        }

        //Comprobar si ja tenim el item
        for(int i=0; i<Container.Items.Count; i++)
        {
            //if(Container.Items[i].item == _item) //Tutorial 1
            if (Container.Items[i].item.Id == _item.Id)  //Tutorial 3
            {
                //Sumar la quantitat en el slot del item
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }

        //Si no tenim previament el item, es crea un nou slot        
        Container.Items.Add(new B_InventorySlot(_item.Id, _item, _amount));
        
    }*/
    /*
    public void AddItem(B_Item _item, int _amount)
    {
        if(_item.buffs.Length > 0)
        {
            SetEmptySlot(_item, _amount);
            return;
        }

        //Comprobar si ja tenim el item
        for(int i=0; i<Container.Items.Length; i++)
        {
            
            if (Container.Items[i].item.Id == _item.Id) 
            {
                //Sumar la quantitat en el slot del item
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }

        SetEmptySlot(_item, _amount);

        //Si no tenim previament el item, es crea un nou slot        
        
    }*/

    public bool AddItem(B_Item _item, int _amount) //Tutorial 7
    {
        //En esta versió, si el item es stackable i hi ha existències en l'inventari
        // però no hi ha slot buits, es perd l'item de manera innecessària
        if (EmptySlotCount <= 0) return false;

        B_InventorySlot slot = FindItemOnInventory(_item);
        //Si el objecte no es stackable o no hi ha existències en l'inventari,
        //es guarda en un slot buit
        //if(!database.GetItem[_item.Id].stackable || slot==null)
        if (!database.Items[_item.Id].stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }

    //Per a comptar els slots buits
    public int EmptySlotCount // Tutorial 7 minut 27
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].item.Id <= -1)
                    counter++;
            }

            return counter;
        }
    }

    public B_InventorySlot FindItemOnInventory(B_Item _item) // Tutorial 7 minut 28
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Id == _item.Id)
                return Container.Items[i];
        }

        return null;
    }

    //Tutorial 5
    public B_InventorySlot SetEmptySlot(B_Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].item.Id <= -1)
            {
                //Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                Container.Items[i].UpdateSlot(_item, _amount);
                return Container.Items[i];
            }
        }
        // Pendent de desenvolupar
        return null;
    }

    // Tutorial 5, per a eliminar un item de l'inventari quan l'arrastres fora
    public void RemoveItem(B_Item _item)
    {
        for(int i=0; i<Container.Items.Length; i++)
        {
            if(Container.Items[i].item == _item)
            {
                //Container.Items[i].UpdateSlot(-1, null, 0);
                Container.Items[i].UpdateSlot(null, 0);
            }
        }

    }

    [ContextMenu("Save")]   //Èsta linea és Tutorial 3, permitix guardar la informació del SO des de l'Editor
    public void Save()
    {
        //Tutorial 2
        /*
        //Es guardarà la informació en un format binari en la utilitat JSON
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();

        //string.Concat(): per a combinar varios strings en un
        //Application.persistentDataPath: Funció del Uniti per a guardar arxius en un camí persistent a
        //través de varios dispositius
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);

        //Hem acabat amb file, així que el tanquem
        file.Close();*/

        //Tutorial 3
        //S'utilitzarà un IFormatter per a guardar en volta d'un JSON
        //Si vols que el jugador puga modificar els paràmetres fàcilment, deixau en el JSON

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        //Hi ha que tancar el stream
        stream.Close();

    }
    [ContextMenu("Load")]   //Èsta linea és Tutorial 3, permitix carregar la informació del SO des de l'Editor
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            // Tutorial 1
            /*
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);

            //Reconvertir file en un Scriptable Object
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
            */

            // Tutorial 3
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Container = (B_Inventory)formatter.Deserialize(stream);
            //Hi ha que tancar el stream
            stream.Close();


        }
    }

    [ContextMenu("Load_Tut5")]  //Este servix per actualitzar l'inventari que s'està utilitzant
    public void LoadTut5()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
           
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            //Container = (B_Inventory)formatter.Deserialize(stream);
            B_Inventory newContainer = (B_Inventory)formatter.Deserialize(stream);

            for(int i = 0; i < Container.Items.Length; i++)
            {
                /*Container.Items[i].UpdateSlot(newContainer.Items[i].item.Id,
                    newContainer.Items[i].item,
                    newContainer.Items[i].amount);*/
                Container.Items[i].UpdateSlot(
                    newContainer.Items[i].item,
                    newContainer.Items[i].amount);
            }


            //Hi ha que tancar el stream
            stream.Close();


        }
    }

    // Tutorial 3, funció per a buidar l'inventari
    [ContextMenu("Clear")]   //Permitix la operació des de l'Editor
    public void Clear()
    {
        //Container = new B_Inventory();
        Container.Clear(); // Tutorial 6
    }

    //Tutorial 5, per a intercanviar posició de slots
    /*public void MoveItem(B_InventorySlot item1, B_InventorySlot item2)
    {
        B_InventorySlot temp = new B_InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }*/

    // Tutorial 7, fa la mateixa funció que MoveItem
    public void SwapItems(B_InventorySlot item1, B_InventorySlot item2)
    {
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        { 
        B_InventorySlot temp = new B_InventorySlot(item2.item, item2.amount);
        item2.UpdateSlot(item1.item, item1.amount);
        item1.UpdateSlot(temp.item, temp.amount);
        }
    }

    // En Tutorial 3 no fan falta
    /*
    public void OnAfterDeserialize()
    {
        for(int i=0; i<Container.Items.Count; i++)
        {
            //Quan es modifica el ID del Item, aquest es canvia al que correspon per ID en la base de dades
            Container.Items[i].item = database.GetItem[Container.Items[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }*/
}

[System.Serializable]
public class B_Inventory
{
    //public List<B_InventorySlot> Items = new List<B_InventorySlot>(); //Tutorial 3
    public B_InventorySlot[] Items = new B_InventorySlot[20]; //Tutorial 5, 24 serà la quantitat per defecte

    public void Clear() // Tutorial 6
    {
        for(int i= 0; i< Items.Length; i++)
        {
            //Items[i].UpdateSlot(-1, new B_Item(), 0);
            //Items[i].UpdateSlot(new B_Item(), 0);
            Items[i].RemoveItem(); // Tutorial 7
        }
    }
}



//El serializable es per a que es mostre en l'Editor
[System.Serializable]
public class B_InventorySlot
{
    // Tutorial 6, per a poder canviar-ho d'inventari
    //NonSerialized per a evitar que es mostre en l'Editor i que siga guardat pel sistema
    [System.NonSerialized]
    public B_UserInteface parent;

    // Tutorial 6, per a concretar quins items poden anar en este slot
    // (si Length == 0, qualsevol tipus està permitit)
    public B_ItemType[] AllowedItems = new B_ItemType[0];

    //Variable per a la base de dades (REF: https://www.youtube.com/watch?v=232EqU1k9yQ)
    //public int ID = -1; // En Tutorial 7 es borra

    //public B_ItemObject item; //Tutorial 1
    public B_Item item; // Tutorial 3
    public int amount;

    public B_ItemObject ItemObject // Tutorial 7 min 20
    {
        get
        {
            if(item.Id >= 0)
            {
                //return parent.inventory.database.GetItem[item.Id];
                return parent.inventory.database.Items[item.Id];
            }
            return null;
        }
    }

    //public B_InventorySlot(int _id, B_Item _itemObject, int _amount)
    public B_InventorySlot(B_Item _itemObject, int _amount)
    {
        //ID = _id;
        item = _itemObject;
        amount = _amount;
    }
    //Tutorial 5
    public B_InventorySlot()
    {
        //ID = -1;
        //item = null;
        item = new B_Item();
        amount = 0;
    }
    //Tutorial 5
    //public void UpdateSlot(int _id, B_Item _itemObject, int _amount)
    public void UpdateSlot(B_Item _itemObject, int _amount)
    {
        //ID = _id;
        item = _itemObject;
        amount = _amount;
    }

    //Tutorial 7
    public void RemoveItem()
    {
        item = new B_Item();
        amount = 0;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    //Tutorial 6
    /*public bool CanPlaceInSlot(B_ItemObject _item)
    {
        if (AllowedItems.Length == 0) return true;

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_item.type == AllowedItems[i]) return true;
        }

        return false;
    }*/
    //Tutorial 7
    public bool CanPlaceInSlot(B_ItemObject _itemObject)
    {
        if (AllowedItems.Length == 0 || _itemObject == null || _itemObject.data.Id < 0) 
            return true;

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_itemObject.type == AllowedItems[i]) return true;
        }

        return false;
    }

}
