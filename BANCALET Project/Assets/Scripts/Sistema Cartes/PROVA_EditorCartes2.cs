using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PROVA_EditorCartes2 : MonoBehaviour
{
    // Start is called before the first frame update
    //Posem ara un array, després seria interesant gastar una base de dades
    public SO_LetterBlock[] totalBlocks;
    private List<LetterBlockEditorScript> allBlocsEditor;
    private LetterBlockEditorScript blocEditor;
    public GameObject objBlocEditor;
    public RectTransform posInstancia;
    public float distanciaY = 50f;

    //Llista dels blocs seleccionats per a la carta
    private List<SO_LetterBlock> blocsForLetter;
    private int numCurrentBloc;

    //Botó de Start, es deshabilita després de pulsarlo i es rehabilita quan 
    // es borra la carta i es pot escriure una nova
    public Button btnStart;
    
    //Botó de Send, s'habilita quan s'ha afegit un bloc de despedida
    public Button btnSend;

    //Botó de Delete, s'habilita després de pulsar Start i es deshabilita després de
    //ser pulsat o després d'enviar la carta (amb Send)
    public Button btnDelete;

    private Character_Letter sender = Character_Letter.Player;
    private Character_Letter receiver = Character_Letter.Miller;


    private void Start()
    {
        if(btnStart == null || btnSend == null || btnDelete == null)
        {
            if (btnStart == null)       Debug.Log("PROVA_EditorCartes2 Start() btnStart == null!!!");
            else if (btnSend == null)   Debug.Log("PROVA_EditorCartes2 Start() btnSend == null!!!");
            else if (btnDelete == null) Debug.Log("PROVA_EditorCartes2 Start() btnDelete == null!!!");

            return;
        }

        btnStart.onClick.AddListener(StartLetter);
        btnStart.interactable = true;

        btnSend.onClick.AddListener(SendLetter);
        btnSend.interactable = false;

        btnDelete.onClick.AddListener(DeleteLetter);
        btnDelete.interactable = false;

    }

    // Es crida amb el botó Start, bloquetja i desbloqueja botons i
    // activa la creació de la carta
    public void StartLetter()
    {
        btnStart.interactable = false;
        btnDelete.interactable = true;

        blocsForLetter = new List<SO_LetterBlock>();
        allBlocsEditor = new List<LetterBlockEditorScript>();
        numCurrentBloc = 0;

        StartEditor();
    }

    public void SendLetter()
    {
        btnStart.interactable = true;
        btnSend.interactable = false;
        btnDelete.interactable = false;

        LetterObject letter = new LetterObject(sender, receiver, blocsForLetter);
        Debug.Log("PROVA_EditorCartes2 SendLetter() Text: \n" + letter.letterText + "\n\ntotalAfection: " + letter.totalAffection);

    }

    public void DeleteLetter()
    {
        btnStart.interactable = true;
        btnSend.interactable = false;
        btnDelete.interactable = false;

        //for(int i=0; i<allBlocsEditor.Count; i++)
        while(allBlocsEditor.Count > 0)
        {
            //Debug.Log("PROVA_EditorCartes2 DeleteLetter() allBlocsEd");
            LetterBlockEditorScript editor = allBlocsEditor[0];
            //SO_LetterBlock bloc = blocsForLetter[i];

            allBlocsEditor.Remove(editor);
            if (blocsForLetter.Count > 0)
                blocsForLetter.RemoveAt(0);


            Destroy(editor.gameObject);
        }

        if (blocEditor != null) Destroy(blocEditor.gameObject);
    }



    public void StartEditor()
    {
        if (numCurrentBloc >= 4) return;

        numCurrentBloc = blocsForLetter.Count;

        List<SO_LetterBlock> blockList = SetCorrectBlocks();
        if(blockList.Count <= 0)
        {
            Debug.Log("PROVA_EditorCartes2 StartEditor() No blocs!!!");
            return;
        }

        //EventTrigger trigger = GetComponent<EventTrigger>();
        if (objBlocEditor == null || totalBlocks == null || totalBlocks.Length <= 0 || posInstancia == null)
        {
            Debug.Log("PROVA_EditorCartes2 StartEditor() elements no inicialitzats!!!");
            if (objBlocEditor == null) Debug.Log("objBlocEditor == null");
            else if(totalBlocks == null) Debug.Log("totalBlocks == null");
            else if (totalBlocks.Length <= 0) Debug.Log("totalBlocks.Length <= 0");
            else if (posInstancia == null) Debug.Log("posInstancia == null");
            return;
        }

        var obj = Instantiate(objBlocEditor, posInstancia);
        //obj.GetComponent<RectTransform>().localPosition = new Vector2(0, 0 + (numCurrentBloc * distanciaY));
        blocEditor = obj.GetComponent<LetterBlockEditorScript>();
        blocEditor.delDecided += DecidedBlock;
        blocEditor.delRedo += RedoBlock;
        blocEditor.delDelete += CheckDelete;
        blocEditor.StartBlockSelection(numCurrentBloc, blockList);
        allBlocsEditor.Add(blocEditor);
    }

    private List<SO_LetterBlock> SetCorrectBlocks()
    {
        List<SO_LetterBlock> blockList = new List<SO_LetterBlock>();

        foreach (SO_LetterBlock bloc in totalBlocks)
        {
            if (numCurrentBloc == 0 && bloc.type == LetterBlockType.Header)
                blockList.Add(bloc);
            else if (numCurrentBloc == 1 && bloc.type == LetterBlockType.Body)
                blockList.Add(bloc);
            else if(numCurrentBloc >=2 && numCurrentBloc <= 3 
                && (bloc.type == LetterBlockType.Body || bloc.type == LetterBlockType.Departure))
                blockList.Add(bloc);
            else if (numCurrentBloc == 4 && bloc.type == LetterBlockType.Departure)
                blockList.Add(bloc);
        }

        return blockList;
    }

    public void DecidedBlock(int posInLetter, SO_LetterBlock decidedBlock)
    {
        if (decidedBlock == null) return;

        // Si era el últim bloc creat
        if (posInLetter == blocsForLetter.Count)
        {
            blocsForLetter.Add(decidedBlock);
            //Si no era una despedida, es continua amb el següent bloc
            if (decidedBlock.type != LetterBlockType.Departure)
                StartEditor();
            //Si era una despedida, habilitar botó de Send
            else
                btnSend.interactable = true;
        }
        else if(posInLetter == blocsForLetter.Count - 1)
        {
            blocsForLetter[posInLetter] = decidedBlock;
            if (decidedBlock.type != LetterBlockType.Departure)
                StartEditor();
            //Si era una despedida, habilitar botó de Send
            else
                btnSend.interactable = true;
        }

        else // Si era cualsevol dels ja creats
        {
            blocsForLetter[posInLetter] = decidedBlock;
            // Si no era una despedida, rehabilitar

            // Si es una despedida, borrar blocs següents i habilitar botó de Send
            if (decidedBlock.type == LetterBlockType.Departure)
            {
                btnSend.interactable = true;
            }
            else
                allBlocsEditor[allBlocsEditor.Count - 1].OpenBlock(true);
        }
    }

            
        
    public void RedoBlock(int posInLetter)
    {
        // El bloc activat actualment deu ser tancat
        /*
        if (blocEditor.posInLetter == blocsForLetter.Count - 1)
            blocsForLetter.Add(blocEditor.currentBlock);
        else
            blocsForLetter[posInLetter] = */
        Debug.Log("PROVA_EditorCartes2 RedoBlock() posInLetter= " + posInLetter + ", numCurrentBloc=" + numCurrentBloc);
        if(posInLetter != numCurrentBloc)
        {
            allBlocsEditor[numCurrentBloc].OpenBlock(false);
            blocsForLetter.Add(allBlocsEditor[numCurrentBloc].currentBlock);

            numCurrentBloc = posInLetter;
            blocEditor = allBlocsEditor[numCurrentBloc];
        }


        // El nou bloc actual deu ser el posInLetter
    }

    public void CheckDelete(int posInLetter)
    {
        Debug.Log("PROVA_EditorCartes2 CheckDelete() blocsForLetter.Count=" + blocsForLetter.Count + "; posInLetter=" + posInLetter);
        if (blocsForLetter.Count <= 1 && posInLetter == 0) return;

        GameObject obj = allBlocsEditor[posInLetter].gameObject;
        allBlocsEditor.RemoveAt(posInLetter);
        if(blocsForLetter.Count != posInLetter)
            blocsForLetter.RemoveAt(posInLetter);

        Destroy(obj);
    }

}
