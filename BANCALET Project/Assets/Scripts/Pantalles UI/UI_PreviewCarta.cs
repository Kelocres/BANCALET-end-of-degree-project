using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PreviewCarta : UI_Menu
{
    
    private LetterObject currentLetter;
    public UI_MenuCartes_Bustia menuBustia;
    public UI_MenuCartes menuCartes;

    //Text on es mostrarà la carta
    public TextMeshProUGUI txtLetter;

    public void SetLetter(LetterObject letter)
    {
        currentLetter = letter;
        txtLetter.text = currentLetter.letterText;
    }

    //Quan s'accedixca a la carta acabada d'escriure
    //El botó sí
    public void ConfirmSending()
    {
        if(menuBustia == null || menuCartes == null)
        {
            Debug.Log("UI_PreviewCarta ConfirmSending() menuBustia == null || menuCartes == null!!");
            return;
        }

        menuBustia.NovaCartaEscritaEnBustia(currentLetter);
        ChangeMenu(menuCartes);
    }

    //Quan s'accedixca a les cartes rebudes
    //El botó següent
    public void NextReceivedLetter()
    {

    }
}
