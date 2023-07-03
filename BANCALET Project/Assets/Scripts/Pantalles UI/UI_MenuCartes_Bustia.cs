using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuCartes_Bustia : UI_Menu
{
    //Es possible que faja falta crear una base de dades per a les cartes
    public List<LetterObject> llistaEscrites = new List<LetterObject>();
    public List<LetterObject> llistaRebudes = new List<LetterObject>();

    

    public void NovaCartaEscritaEnBustia(LetterObject carta)
    {
        llistaEscrites.Add(carta);
    }

    //Es crida quan arriba el carter e interacciona amb la bústia
    public void AccioCarter()
    {
        //Les cartes escrites passen a MenuCartes com a Enviades en la seua fila corresponent

        //Es reben les cartes del LetterGenerator

    }
}
