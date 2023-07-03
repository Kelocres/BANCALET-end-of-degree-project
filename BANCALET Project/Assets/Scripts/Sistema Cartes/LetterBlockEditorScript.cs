using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterBlockEditorScript : MonoBehaviour
{
    //public SO_LetterBlock[] totalBlocks;
    public List<SO_LetterBlock> totalBlocks;

    public Image slotBlock;
    public TextMeshProUGUI txtSlotBlock;

    public Button btnPreviousBlock;
    public Button btnNextBlock;
    public Button btnDecide;
    public Button btnDelete;

    //public TextMeshProUGUI txtDecidedBlock;

    private bool started = false;
    private bool finished = false;
    private bool allInPlace = false;
    public SO_LetterBlock currentBlock;
    private int posCurrentBlock = 0;

    //Array dels textos dels blocks seleccionats
    private List<string> selectedTexts;
    private List<SO_LetterBlock> selectedBlocks;

    //Delegate per a indicar al Editor de Cartes que s'ha polsat el botó Decide
    public delegate void delLetterBlock(int pos, SO_LetterBlock bloc);
    public event delLetterBlock delDecided;

    //Delegate per a indicar al Editor de Cartes que un bloc ja posat anteriorment
    // va a editarse
    public delegate void delLetterBlockRedo(int pos);
    public event delLetterBlockRedo delRedo;

    //Delegate per a indicar que es vol borrar 
    public event delLetterBlockRedo delDelete;

    //Per a detectar en quina posició de la creació de la carta està
    public int posInLetter = -1;

    //Per al canvi de tamany segons siga el de slotBlock (DESCARTAT!!)
    //private RectTransform rectTransform;

    void Start()
    {
        if (allInPlace) return;

        if (slotBlock == null || txtSlotBlock == null || btnPreviousBlock == null 
            || btnNextBlock == null || btnDecide == null || btnDelete == null)
        {
            //StartBlockSelection();
            Debug.Log("LetterBlockEditorScript Start() elements no inicialitzats!!!");
            return;
        }

        btnPreviousBlock.onClick.AddListener(PreviousBlock);
        btnNextBlock.onClick.AddListener(NextBlock);
        btnDecide.onClick.AddListener(DecideBlock);
        btnDelete.onClick.AddListener(CheckDelete);

        slotBlock.gameObject.SetActive(false);
        txtSlotBlock.gameObject.SetActive(false);
        btnPreviousBlock.gameObject.SetActive(false);
        btnNextBlock.gameObject.SetActive(false);
        btnDecide.gameObject.SetActive(false);
        btnDelete.gameObject.SetActive(false);

        allInPlace = true;

        //rectTransform = GetComponent<RectTransform>();
        //StartBlockSelection();
    }



    //public void StartBlockSelection(SO_LetterBlock[] _totalBlocks)
    public void StartBlockSelection(int _posInLetter, List<SO_LetterBlock> _totalBlocks)
    {
        if (!allInPlace) Start();
        

        slotBlock.gameObject.SetActive(true);
        txtSlotBlock.gameObject.SetActive(true);
        btnPreviousBlock.gameObject.SetActive(true);
        btnNextBlock.gameObject.SetActive(true);
        btnDecide.gameObject.SetActive(true);
        btnDelete.gameObject.SetActive(true);

        totalBlocks = _totalBlocks;
        posInLetter = _posInLetter;

        selectedBlocks = new List<SO_LetterBlock>();
        selectedTexts = new List<string>();

        started = true;
        currentBlock = totalBlocks[posCurrentBlock];
        txtSlotBlock.text = currentBlock.text;
        /*
        if(rectTransform != null && slotBlock!=null)
        {
            rectTransform.sizeDelta = new Vector2(slotBlock.rectTransform.sizeDelta.x, slotBlock.rectTransform.sizeDelta.y);
        }*/

        ChangeColor();

    }

    public void PreviousBlock()
    {
        if (!allInPlace)
        {
            Debug.Log("LetterBlockEditorScript StartBlockSelection() elements no inicialitzats!!!");
            return;
        }

        if (started && !finished && totalBlocks.Count > 0)
        {
            posCurrentBlock--;
            if (posCurrentBlock < 0) posCurrentBlock = totalBlocks.Count - 1;

            currentBlock = totalBlocks[posCurrentBlock];
            txtSlotBlock.text = currentBlock.text;

            ChangeColor();
            /*
            if (rectTransform != null && slotBlock != null)
            {
                rectTransform.sizeDelta = new Vector2(slotBlock.rectTransform.sizeDelta.x, slotBlock.rectTransform.sizeDelta.y);
            }*/
        }
    }

    public void NextBlock()
    {
        if (!allInPlace)
        {
            Debug.Log("LetterBlockEditorScript StartBlockSelection() elements no inicialitzats!!!");
            return;
        }

        if (started && !finished && totalBlocks.Count > 0)
        {
            posCurrentBlock++;
            if (posCurrentBlock >= totalBlocks.Count) posCurrentBlock = 0;

            currentBlock = totalBlocks[posCurrentBlock];
            txtSlotBlock.text = currentBlock.text;

            ChangeColor();
            /*if (rectTransform != null && slotBlock != null)
            {
                rectTransform.sizeDelta = new Vector2(slotBlock.rectTransform.sizeDelta.x, slotBlock.rectTransform.sizeDelta.y);
            }*/
        }
    }

    public void DecideBlock()
    {
        if (!allInPlace)
        {
            Debug.Log("LetterBlockEditorScript StartBlockSelection() elements no inicialitzats!!!");
            return;
        }

        // Apretar el botó per a acabar la decisió i bloquejar botons
        if (!finished)
        {
            /*
            btnPreviousBlock.gameObject.SetActive(false);
            btnNextBlock.gameObject.SetActive(false);

            //Amagar la image (per a destacar el canvi)
            slotBlock.gameObject.SetActive(false);

            finished = true;*/
            OpenBlock(false);
            if (delDecided != null) delDecided(posInLetter, currentBlock);
        }
        // Apretar el botó per a tornar a decidir
        else
        {
            /*
            btnPreviousBlock.gameObject.SetActive(true);
            btnNextBlock.gameObject.SetActive(true);

            //Amagar la image (per a destacar el canvi)
            slotBlock.gameObject.SetActive(true);

            finished = false;*/
            OpenBlock(true);
            if (delRedo != null) delRedo(posInLetter);

        }

        //if (delDecided != null) delDecided(posInLetter, finished);
        //txtDecidedBlock.text = currentBlock.text;

        //currentBlock = null;
        //txtSlotBlock.text = "";
    }

    public void OpenBlock(bool letsOpen)
    {
        if(letsOpen)
        {
            btnPreviousBlock.gameObject.SetActive(true);
            btnNextBlock.gameObject.SetActive(true);

            //Amagar la image (per a destacar el canvi)
            //slotBlock.gameObject.SetActive(true);
            //slotBlock.enabled = true;

            finished = false;
            ChangeColor();
        }
        else
        {
            btnPreviousBlock.gameObject.SetActive(false);
            btnNextBlock.gameObject.SetActive(false);

            //Amagar la image (per a destacar el canvi)
            //slotBlock.gameObject.SetActive(false);
            //slotBlock.enabled = false;

            finished = true;
            ChangeColor();
        }
    }

    private void CheckDelete()
    {
        Debug.Log("LetterBlockEditorScript CheckDelete() ");
        //Comprovar delegate per si es permet ser borrat
        if (delDelete != null) delDelete(posInLetter);
    }

    

    //Per a senyalar si el bloc es Header, Body o Departure
    private void ChangeColor()
    {
        if (slotBlock == null) return;

        if(finished)
        {
            slotBlock.color = Color.gray;
            return;
        }

        if (currentBlock.type == LetterBlockType.Header)
            slotBlock.color = Color.green;
        else if (currentBlock.type == LetterBlockType.Body)
            slotBlock.color = Color.yellow;
        else if (currentBlock.type == LetterBlockType.Departure)
            slotBlock.color = Color.red;


    }
}