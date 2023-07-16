using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_InventariJugador : UI_Menu
{
    //public DisplayInventory displayInventari;
    //public SO_Inventari inventari;
    //public SO_InventoryObject inventari;
    public DynamicInterface mostraInventari;

    public Image itemIcona;
    public TextMeshProUGUI itemNom;
    public TextMeshProUGUI itemDescripcio;

    //Delegate per a que la barra d'items sàpiga que està habilitat o deshabilitat
    //ARA ES GUARDA EN UI_MENU PER A QUE SIGA ACTIVABLE EN ALTRES MENÚS
    //public delegate void delSignal(bool intro);
    //public event delSignal delIsActive;

    void Start()
    {
        mostraInventari = GetComponentInChildren<DynamicInterface>();
        if(mostraInventari != null)
        {
            mostraInventari.delSelectedSlot += SelectedSlot;

        }

        SelectedSlot(null);

        //Busca al codi de la barra d'items per a que inserte el delegate
        //FindObjectOfType<ItemsBarScript>().SetMenuInventari(this);
        //if (delIsActive != null) delIsActive(true);
        AllowItemsBarControl();
    }

    /*private void OnEnable()
    {
        if (delIsActive != null) delIsActive(true);
    }

    private void OnDisable()
    {
        if (delIsActive != null) delIsActive(false);
    }*/

    // Update is called once per frame
    public void SelectedSlot(InventorySlot selectedSlot)
    {
        if (!ComprobarItemDataWindow())
        {
            Debug.Log("UI_InventariJugador SelectedSlot() element de ItemDataWindow no identificats");
            return;
        }

        if (selectedSlot == null || selectedSlot.item.Id == -1)
        {
            itemIcona.color = new Color32(255, 255, 255, 0);
            itemNom.text = "";
            itemDescripcio.text = "";
        }
        else
        {
            itemIcona.sprite = selectedSlot.ItemObject.uiDisplay;
            itemIcona.color = new Color32(255, 255, 255, 255);
            itemNom.text = selectedSlot.ItemObject.data.Name;
            itemDescripcio.text = selectedSlot.ItemObject.description;
        }
    }

    public bool ComprobarItemDataWindow()
    {
        if (itemIcona == null) return false;
        if (itemNom == null) return false;
        if (itemDescripcio == null) return false;

        return true;
    }

    public void CreateSeeds()
    {
        InventorySlot selectedSlot = mostraInventari.selectedSlot;
        if (selectedSlot == null || selectedSlot.item.Id == -1 || selectedSlot.ItemObject.type != ItemType.Harvest || selectedSlot.amount <=0)
            return;

        //Buscar slot d'eixa llavor, o si no existix, un slot buit
        SO_HarvestItem harvest = (SO_HarvestItem)selectedSlot.ItemObject;
        SO_SeedItem seed = harvest.seed;

        if (mostraInventari.inventory.AddItem(seed.data, 1) || FindObjectOfType<ItemsBarScript>().itemsbar.inventory.AddItem(seed.data, 1))
            selectedSlot.ConsumeItem(1);
    }
}
