using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SO_ToolItem : SO_ItemObject
{
    public int staminaCost = 10;
    //StaminaSystem playerStamina;
    private void Awake()
    {
        type = ItemType.Tool;
    }

    void Start()
    {
        //Tal volta esta part deuria estar en Start() i no en Awake()
        /*playerStamina = FindObjectOfType<StaminaSystem>();
        if (playerStamina == null)
            Debug.Log("SO_ToolItem Awake() playerStamina == null!!!");*/
    }

    //Comprovar si l'acció es realitzable, segons el GameObject i el valor de Stamina
    //public override bool CheckObjective(GameObject obj, StaminaSystem stamina, FeedingSystem feed)
    public override bool CheckObjective(GameObject obj, PJ_StateManager pj)
    {
        //Debug.Log("SO_ToolItem CheckObjective()");
        if (pj.stamina != null)
        {
            //Debug.Log("SO_ToolItem CheckObjective() playerStamina != null!!");
            return pj.stamina.CheckStamina() && CheckToolObjective(obj);
        }

        //Debug.Log("SO_ToolItem CheckObjective() playerStamina == null!!");
        return CheckToolObjective(obj);
    }

    //Funciona como CheckObjective, pero es la que hereden els fills
    protected virtual bool CheckToolObjective(GameObject obj)
    {
        //Debug.Log("SO_ToolItem CheckToolObjective()");
        return false;
    }

    //Realitzar l'acció i consumir stamina
    //public override bool ActivateItem(GameObject obj, StaminaSystem stamina, FeedingSystem feed)
    public override bool ActivateItem(GameObject obj, PJ_StateManager pj)
    {
        //Debug.Log("SO_ToolItem ActivateItem()");
        if (pj.stamina != null)
        {
            bool result = ActivateToolItem(obj);
            if (result)
                pj.stamina.ConsumeStamina(staminaCost);
            return result;
        }
            
        else
            return ActivateToolItem(obj);
    }

    //Funciona como CheckObjective, pero es la que hereden els fills
    protected virtual bool ActivateToolItem(GameObject obj)
    {
        //Debug.Log("SO_ToolItem ActivateToolItem()");
        return false;
    }

}
