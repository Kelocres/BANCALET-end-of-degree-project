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

[CreateAssetMenu(fileName = "New Inventory", menuName = "Sistema Inventari/Inventari")]
//public class B_InventoryObject : ScriptableObject, ISerializationCallbackReceiver En Tutorial 3 no cal el ISerialiblabla
public class SO_InventoryObject : ScriptableObject 
{
    //Camí on es guardarà la informació
    public string savePath;
    
    //Variable per a la base de dades //Tutorial 2
    public  SO_ItemDatabaseObject database;

    
    public Inventory Container;

    
    public bool AddItem(Item _item, int _amount) //Tutorial 7
    {
        //En esta versió, si el item es stackable i hi ha existències en l'inventari
        // però no hi ha slot buits, es perd l'item de manera innecessària
        if (EmptySlotCount <= 0) return false;

        InventorySlot slot = FindItemOnInventory(_item);
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

    public InventorySlot FindItemOnInventory(Item _item) // Tutorial 7 minut 28
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Id == _item.Id)
                return Container.Items[i];
        }

        return null;
    }

    //Tutorial 5
    public InventorySlot SetEmptySlot(Item _item, int _amount)
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
    public void RemoveItem(Item _item)
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

    [ContextMenu("Guardar")]   //Èsta linea és Tutorial 3, permitix guardar la informació del SO des de l'Editor
    public void Save()
    {
        
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        //Hi ha que tancar el stream
        stream.Close();

    }
    

    [ContextMenu("Carregar")]  //Este servix per actualitzar l'inventari que s'està utilitzant
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
           
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            //Container = (B_Inventory)formatter.Deserialize(stream);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);

            for(int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(
                    newContainer.Items[i].item,
                    newContainer.Items[i].amount);
            }


            //Hi ha que tancar el stream
            stream.Close();


        }
    }

    // Tutorial 3, funció per a buidar l'inventari
    [ContextMenu("Netejar")]   //Permitix la operació des de l'Editor
    public void Clear()
    {
        //Container = new B_Inventory();
        Container.Clear(); // Tutorial 6
    }

    

    // Tutorial 7, fa la mateixa funció que MoveItem
    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        { 
        InventorySlot temp = new InventorySlot(item2.item, item2.amount);
        item2.UpdateSlot(item1.item, item1.amount);
        item1.UpdateSlot(temp.item, temp.amount);
        }
    }

    
}

[System.Serializable]
public class Inventory
{
    //public List<B_InventorySlot> Items = new List<B_InventorySlot>(); //Tutorial 3
    public InventorySlot[] Items = new InventorySlot[20]; //Tutorial 5, 24 serà la quantitat per defecte

    public void Clear() // Tutorial 6
    {
        for(int i= 0; i< Items.Length; i++)
        {
            Items[i].RemoveItem(); // Tutorial 7
        }
    }
}



//El serializable es per a que es mostre en l'Editor
[System.Serializable]
public class InventorySlot
{
    // Tutorial 6, per a poder canviar-ho d'inventari
    //NonSerialized per a evitar que es mostre en l'Editor i que siga guardat pel sistema
    [System.NonSerialized]
    public UserInteface parent;

    // Tutorial 6, per a concretar quins items poden anar en este slot
    // (si Length == 0, qualsevol tipus està permitit)
    public ItemType[] AllowedItems = new ItemType[0];

    //Variable per a la base de dades (REF: https://www.youtube.com/watch?v=232EqU1k9yQ)
    //public int ID = -1; // En Tutorial 7 es borra

    //public B_ItemObject item; //Tutorial 1
    public Item item; // Tutorial 3
    public int amount;

    public SO_ItemObject ItemObject // Tutorial 7 min 20
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
    public InventorySlot(Item _itemObject, int _amount)
    {
        //ID = _id;
        item = _itemObject;
        amount = _amount;
    }
    //Tutorial 5
    public InventorySlot()
    {
        //ID = -1;
        //item = null;
        item = new Item();
        amount = 0;
    }
    //Tutorial 5
    //public void UpdateSlot(int _id, B_Item _itemObject, int _amount)
    public void UpdateSlot(Item _itemObject, int _amount)
    {
        //ID = _id;
        item = _itemObject;
        amount = _amount;
    }

    //Tutorial 7
    public void RemoveItem()
    {
        item = new Item();
        amount = 0;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void ConsumeItem(int _amount)
    {
        //Debug.Log("InventorySlot ConsumeItem()");
        amount -= _amount;
        if (amount <= 0) RemoveItem();
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
    public bool CanPlaceInSlot(SO_ItemObject _itemObject)
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
