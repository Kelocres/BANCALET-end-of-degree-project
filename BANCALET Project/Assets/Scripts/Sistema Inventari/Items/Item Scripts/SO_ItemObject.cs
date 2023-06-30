using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=_IqTeruf3-s&t=51s // Tutorial 1
//REF: https://www.youtube.com/watch?v=LcizwQ7ogGA&t=46s // Tutorial 3

public enum ItemType
{
    /*Consumible,
    Collita,
    Ferramenta,
    Llavor,
    Defecte*/
    Consumable,
    Harvest,
    Tool,
    Seed,
    Default
}

//Atributs que poden diferenciar objectes que son el mateix tipus però amb
//diferencies
//Açò es dels tutorials i no es necessari, però ho guardaré de moment
public enum Attribute //Tutorial 3
{
    Agility,
    Intellect,
    Stamina,
    Strength
}

//La classe serà abstracta, perque no será la que utilitzarem per a crear
//els items, sino la base.
//Les classes que heredaran seran, per exemple: FoodItem, WeaponItem, etc...
public abstract class SO_ItemObject : ScriptableObject
{
    // ID del item per a la serialització en la base de dades
    //public int Id; // Tutorial 3

    //public GameObject prefab; //Tutorial 1
    public Sprite uiDisplay;    //Tutorial 3
    public bool stackable;      //Tutorial 7 minut 26
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    //public B_ItemBuff[] buffs;
    public Item data = new Item();
    //Amount that is consumed in the normal action slot
    public int actionConsumption = 0;
    public string accioAnim = "defecte";

    //Comprovar si l'objecte pot interactuar amb l'objectiu seleccionat
    //public virtual bool CheckObjective(GameObject obj, StaminaSystem stamina, FeedingSystem feed)
    public virtual bool CheckObjective(GameObject obj, PJ_StateManager pj)
    {
        return false;
    }

    //Activació de l'objecte, el bool indica si s'ha realitzat amb èxit
    //public virtual bool ActivateItem(GameObject obj, StaminaSystem stamina, FeedingSystem feed)
    public virtual bool ActivateItem(GameObject obj, PJ_StateManager pj)
    {
        return false;
    }

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

//Tutorial 3
// Amb açò podem donar valors diferents al mateix tipus d'items
// Exemple: Les armes de Borderlands, que cada una té valors diferents encara que siguen
// el mateix model
[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;

    //El uiDisplay es necessari per a SlotObject, però pot ser que siga innecessari
    public Sprite uiDisplay;
    public string description;

    public Item() //Tutorial 6, per a quan es dega buidar l'inventari
    {
        Name = "";
        Id = -1;
        uiDisplay = null;
        description = "";
    }

    public Item(SO_ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        uiDisplay = item.uiDisplay;
        description = item.description;
        
        buffs = new ItemBuff[item.data.buffs.Length];
        for(int i=0; i < buffs.Length; i++)
        {
            //buffs[i] = new B_ItemBuff(item.buffs[i].min, item.buffs[i].max)
            //buffs[i].attribute = item.buffs[i].attribute;

            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                attribute = item.data.buffs[i].attribute
            };

            
        }
    }
}

//Classe per a mantindre els atributs dels items
[System.Serializable]
public class ItemBuff // Tutorial 3
{
    public B_Attribute attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    //Funció per a regenerar un nou valor (entre els valors min i max)
    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }

}
