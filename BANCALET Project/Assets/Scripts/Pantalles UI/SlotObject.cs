using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotObject : MonoBehaviour
{
    //public SlotInventari slot;
    public InventorySlot slot;

    //public Sprite slotSprite;
    //public Sprite iconSprite;
    //public string amountText;

    private Image slotImage;
    private Image iconImage;
    private TextMeshProUGUI amountTM;

    //Per a evitar que es cride les funcions abans del Start()
    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!started)
            StartSlotObject();
        
    }

    private void StartSlotObject()
    {
        Debug.Log("SlotObject Start()");
        //slotSprite = GetComponent<Image>().sprite;
        slotImage = GetComponent<Image>();
        if (slotImage == null) Debug.Log("SlotObject Start() slotImage == null");
        //slotSprite = slotImage.sprite;

        iconImage = transform.GetChild(0).GetComponent<Image>();
        if (iconImage == null) Debug.Log("SlotObject Start() iconImage == null");
        //iconSprite = iconImage.sprite;

        amountTM = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        if (amountTM == null) Debug.Log("SlotObject Start() aumounTM == null");
        //amountText = amountTM.text;

        started = true;
    }

    public void AfegirSlot(InventorySlot nouSlot)
    {
        if (!started)
            StartSlotObject();

        slot = nouSlot;

        //if (!slot.buit)
        if (slot.item != null)
        {
            //slotImage.sprite = DisplayInventoryManager.GetSlotSprite(slot.item.tipusItem);

            iconImage.enabled = true;
            iconImage.sprite = slot.item.uiDisplay;

            amountTM.enabled = true;
            amountTM.text = slot.amount.ToString("n0");
        }
        else BuidarSlot();
    }

    public void BuidarSlot()
    {
        if (!started)
            StartSlotObject();
        
        
        //slotImage.sprite = DisplayInventoryManager.slotSpriteBuit;

        iconImage.enabled = false;
        amountTM.enabled = false;

        //slot = null;
    }
    
}
