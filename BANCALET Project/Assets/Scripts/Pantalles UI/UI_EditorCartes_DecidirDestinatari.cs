using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EditorCartes_DecidirDestinatari : UI_Menu
{
    public UI_EditorCartes editor;

    private void Start()
    {
        if (editor != null) return;

        UI_EditorCartes[] allobjs = FindObjectsOfType<UI_EditorCartes>();
        if(allobjs.Length != 1)
        {
            Debug.Log("UI_EditorCartes_DecidirDestinatari Start() allobjs.Length = " + allobjs.Length);
            return;
        }

        editor = allobjs[0];

    }

    private void SeleccionarDestinatari(Character_Letter cl)
    {
        //Canviar valor del destinatari
        if (editor == null) return;
        {
            //editor.receiver = cl;
            editor.SetReceiver(cl);
            ChangeMenu(editor);
        }
        
    }

    //Per a seleccionar destinatari des dels botons del UI
    public void SeleccionarMolinera()
    {
        SeleccionarDestinatari(Character_Letter.Miller);
    }
    public void SeleccionarJubilat()
    {
        SeleccionarDestinatari(Character_Letter.RetiredMan);
    }
    public void SeleccionarGermana()
    {
        SeleccionarDestinatari(Character_Letter.PC_Sister);
    }
}
