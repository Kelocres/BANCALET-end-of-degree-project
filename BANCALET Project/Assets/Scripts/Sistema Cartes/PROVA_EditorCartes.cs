using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PROVA_EditorCartes : MonoBehaviour
{
    // Start is called before the first frame update
    //Posem ara un array, després seria interesant gastar una base de dades
    public SO_LetterBlock[] totalBlocks;

    public Image slotBlock;
    public TextMeshProUGUI txtSlotBlock;

    public Button btnPreviousBlock;
    public Button btnNextBlock;
    public Button btnDecide;

    public TextMeshProUGUI txtDecidedBlock;

    private bool started = false;
    private bool finished = false;
    private SO_LetterBlock currentBlock;
    private int posCurrentBlock = 0;

    //Array dels textos dels blocks seleccionats
    private List<string> selectedTexts;
    private List<SO_LetterBlock> selectedBlocks;

    

    void Start()
    {
        //EventTrigger trigger = GetComponent<EventTrigger>();


        if (slotBlock != null && !started && totalBlocks.Length > 0)
        {
            StartBlockSelection();
        }
    }

  

    public void StartBlockSelection()
    {
        selectedBlocks = new List<SO_LetterBlock>();
        selectedTexts = new List<string>();

        started = true;
        currentBlock = totalBlocks[posCurrentBlock];
        txtSlotBlock.text = currentBlock.text;

    }

    public void PreviousBlock()
    {
        if(started && !finished && totalBlocks.Length > 0)
        {
            posCurrentBlock--;
            if (posCurrentBlock < 0) posCurrentBlock = totalBlocks.Length - 1;

            currentBlock = totalBlocks[posCurrentBlock];
            txtSlotBlock.text = currentBlock.text;
        }
    }

    public void NextBlock()
    {
        if (started && !finished && totalBlocks.Length > 0)
        {
            posCurrentBlock++;
            if (posCurrentBlock >= totalBlocks.Length) posCurrentBlock = 0;

            currentBlock = totalBlocks[posCurrentBlock];
            txtSlotBlock.text = currentBlock.text;
        }
    }

    public void DecideBlock()
    {
        finished = true;

        txtDecidedBlock.text = currentBlock.text;

        currentBlock = null;
        txtSlotBlock.text = "";
    }
}
