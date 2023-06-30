using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSelectIdioma : MonoBehaviour
{
    //Utilitza el ManejaPartida per a saber quin idioma serà l'indicat
    ManejaPartida mp;

    //Strings dels textos, mateixa informació però en diferents idiomes
    [SerializeField][TextArea(3,10)] private string textValencia;
    [SerializeField][TextArea(3,10)] private string textCastellano;
    [SerializeField][TextArea(3,10)] private string textEnglish;

    //Variable pública, el text que es mostrarà al jugador
    //[HideInInspector]
    [HideInInspector] public string selectText;

    void Awake()
    {
        mp = FindObjectOfType<ManejaPartida>();
        ActualitzaSelectText();
    }

    public void SetTexte(DataTextSelectIdioma data)
    {
        textValencia = data.textValencia;
        textCastellano = data.textCastellano;
        textEnglish = data.textEnglish;

        ActualitzaSelectText();
    }

  
    //Aquesta funció es cridarà al començar el joc, i cada vegada que es canvie l'idioma
    public void ActualitzaSelectText()
    {
        string idioma = mp.idiomaActual;
        switch (idioma)
        {
            case "valencià":
                selectText = textValencia;
                break;
            case "español":
                selectText = textCastellano;
                break;
            case "english":
                selectText = textEnglish;
                break;
            default:
                selectText = "ERROR EN TextSelectIdioma";
                break;
        }
    }

    
}

public class DataTextSelectIdioma
{
    public string textValencia;
    public string textCastellano;
    public string textEnglish;
}
