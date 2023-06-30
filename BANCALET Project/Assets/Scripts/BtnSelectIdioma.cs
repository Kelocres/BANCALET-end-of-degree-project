using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSelectIdioma : TextSelectIdioma
{
    //El texte del botó, es canviarà el valor string pel de selectText
    protected Text textBoto;
    private Button boto;
    public Sprite spr_original;
    public Sprite spr_baixCursor;
    public Sprite spr_pulsat;
   
    void Start()
    {
        if (GetComponent<Button>() != null)
        {
            boto = GetComponent<Button>();
            Canviar_Original();
        }
        

        //Per a quan BtnDecisio herede aquest codi
        if(GetComponentInChildren<Text>()!=null)
        {
            textBoto = GetComponentInChildren<Text>();
            //Debug.Log("PROBA BtnSelectIdioma: "+textBoto.text);
            ActualitzaTextBoto();
        }
    }

    // Es cridarà al començar el joc, i cada vegada que es canvie el idioma
    public void ActualitzaTextBoto()
    {
        ActualitzaSelectText();
        textBoto.text = selectText;
    }

    //Per a canviar la imatge del botó mentre el ratolí està damunt
    //TUTORIAL: https://www.youtube.com/watch?v=S5Lp8vCb6hU
    public void Canviar_BaixCursor()
    {
        if (spr_baixCursor != null)
            boto.image.sprite = spr_baixCursor;
        else
            Debug.Log("BtnSelectIdioma Canviar_BaixCursor() ERROR!!! spr_baixCursor es null");
    }

    public void Canviar_Original()
    {
        if (spr_original != null)
            boto.image.sprite = spr_original;
        else
            Debug.Log("BtnSelectIdioma Canviar_Original() ERROR!!! spr_original es null");
    }

    public void Canviar_Pulsat()
    {
        if (spr_pulsat != null)
            boto.image.sprite = spr_pulsat;
        else
            Debug.Log("BtnSelectIdioma Canviar_Pulsat() ERROR!!! spr_pulsat es null");
    }

    public void CopiarEstetica(BtnSelectIdioma estetica)
    {
        spr_original = estetica.spr_original;
        Canviar_Original();
        spr_baixCursor = estetica.spr_baixCursor;
        Canviar_BaixCursor();
        spr_pulsat = estetica.spr_pulsat;
        Canviar_Pulsat();
    }
}
