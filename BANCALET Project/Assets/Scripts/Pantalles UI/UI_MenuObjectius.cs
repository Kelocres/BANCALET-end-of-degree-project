using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuObjectius : UI_Menu
{
    // El timeline de Dormir
    public IniciaTimeline timeline;

    // Els objectius del joc
    public SO_GameObjective[] gameObjectives;
    public SO_GameObjective currentObjective;
    public Text txtMissatge;

    public DynamicInterface deliveredItems;
    // El bool determina si s'està mostrant la recompensa o l'objectiu
    private bool reward;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("UI_MenuObjectius Start()");
        currentWordKey = "";
        SetButtons();

        if(gameObjectives==null || gameObjectives.Length == 0)
        {
            Debug.Log("UI_MenuObjectius Start() no hi han gameObjectives!!!");
            return;
        }

        currentObjective = gameObjectives[0];
        Debug.Log("UI_MenuObjectius Start() Buidar deliveredItems");
        deliveredItems.inventory.ClearAndResize(currentObjective.requiredItems.Container.Items.Length);
        //Es mostra l'objectiu
        reward = false;

        if (txtMissatge != null)
            txtMissatge.text = currentObjective.description;

        //Per a que la barra d'ítems siga controlable
        AllowItemsBarControl();
    }

    //Funció del botó Dormir
    public void Sleep()
    {
        //Debug.Log("UI_MenuDormir Sleep()");
        BackToGame();
        if (timeline != null) timeline.StartTimeline();
    }

    //Funció del botó Continuar
    public void Continue()
    {
        //Es guarden els items en deliveredItems per a les comprovacions
        //deliveredItems.inventory.Save();      NO FUNCIONA!!!

        if(!reward)
        {
            Debug.Log("UI_MenuObjectius Continue() Comprovar deliveredItems");
            InventorySlot[] requiredSlots = currentObjective.requiredItems.Container.Items;
            InventorySlot[] deliveredSlots = deliveredItems.inventory.Container.Items;
            for (int i=0; i<requiredSlots.Length; i++)
            {
                bool trobat = false;
                for(int j=0; j<deliveredSlots.Length; j++)
                {
                    if(requiredSlots[i].item.Id == deliveredSlots[j].item.Id && requiredSlots[i].amount <= deliveredSlots[j].amount)
                    {
                        trobat = true;
                        break;
                    }
                }

                if(!trobat)
                {
                    Debug.Log("UI_MenuObjectius Continue() objectiu no aconseguit");
                    return;
                }

                //OBJECTIU ACONSEGUIT!!
                // Retornar els items que sobren
                Debug.Log("UI_MenuObjectius Continue() OBJECTIU ACONSEGUIT, donar recompensa");
                deliveredItems.inventory = currentObjective.rewardItems;
                reward = true;
            }
        }
        else
        {

        }
    }


}
