using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuDormir : UI_Menu
{
    public IniciaTimeline timeline;

    public FeedingSystem feedingSystem;
    public Text txtMissatge;

    void Start()
    {
        //Debug.Log("UI_MenuDormir Start()");
        currentWordKey = "";
        SetButtons();

        if(feedingSystem == null)
        {
            feedingSystem = FindObjectOfType<FeedingSystem>();
            if(feedingSystem == null)
            {
                Debug.Log("UI_MenuDormir Start() FeedingSystem == null!!!");
                return;
            }
            feedingSystem.dCanviValor += ActualitzarMissatge;
            ActualitzarMissatge();
        }
    }

    //Esta funci� estar� en UI_MenuObjectius
    public void Sleep()
    {
        //Debug.Log("UI_MenuDormir Sleep()");
        BackToGame();
        if (timeline != null) timeline.StartTimeline();
    }

    public void ActualitzarMissatge()
    {
        if(txtMissatge == null)
        {
            Debug.Log("UI_MenuDormir ActualitzarMissatge() txtMissatge == null!!!");
            return;
        }

        if(feedingSystem == null)
        {
            Debug.Log("UI_MenuDormir ActualitzarMissatge() FeedingSystem == null!!!");
            return;
        }

        if (feedingSystem.currentValue >= feedingSystem.CORRECT_VALUE)
        {
            txtMissatge.text = "Est�s completament alimentat, aix� que despertar�s dem� amb la Resistencia plena i la Alimentaci� a la mitat.";
        }
        else if (feedingSystem.currentValue >= feedingSystem.DEFICIENT_VALUE)
        {
            txtMissatge.text = "Amb la alimentaci� actual, despertar�s dem� amb la Resistencia a la mitat i la Alimentaci� al 25%.\n" +
                "Si tens m�s menjar, alimentat un poc m�s abans d'anar a dormir.";
        }
        else
        {
            txtMissatge.text = "Amb la alimentaci� actual, despertar�s dem� amb la Resistencia al 25% i la Alimentaci� a 0.\n" +
                "R�pid! Alimentat m�s abans de dormir si tens m�s menjar.";
        }
    }


}
