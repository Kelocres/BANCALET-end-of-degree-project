using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuDormir : UI_Menu
{
    public IniciaTimeline timeline;

    public FeedingSystem feedingSystem;
    public Text txtMissatge;
    private SoundManager soundManager;

    void Start()
    {
        //Debug.Log("UI_MenuDormir Start()");
        currentWordKey = "";
        SetButtons();
        soundManager = FindObjectOfType<SoundManager>();

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
        soundManager.StopMusic();
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
            //txtMissatge.text = "Est�s completament alimentat, aix� que despertar�s dem� amb la Resistencia plena i la Alimentaci� a la mitat.";
            //txtMissatge.text = "Est�s completamente alimentado, as� que despertar�s ma�ana con la Resistencia llena y la Alimentaci�n a la mitad.";
            txtMissatge.text = "You are fully fed, so you'll wake up tomorrow with full Stamina and half Feeding.";
        }
        else if (feedingSystem.currentValue >= feedingSystem.DEFICIENT_VALUE)
        {
            //txtMissatge.text = "Amb la alimentaci� actual, despertar�s dem� amb la Resistencia a la mitat i la Alimentaci� al 25%.\n" +
            //    "Si tens m�s menjar, alimentat un poc m�s abans d'anar a dormir.";
            //txtMissatge.text = "Con la Alimentaci�n actual, despertar�s ma�ana con la Resistencia a la mitad y la Alimentaci�n al 25%.\n"+
            //    "Si tienes m�s comida, alim�ntate un poco m�s antes de ir a dormir.";
            txtMissatge.text = "With the current Feeding, you'll wake up tomorrow with halft Stamina and 25% Feeding.\n"+
                "If you have some food, better feed yourself a little more before going to sleep.";
        }
        else
        {
            //txtMissatge.text = "Amb la alimentaci� actual, despertar�s dem� amb la Resistencia al 25% i la Alimentaci� a 0.\n" +
            //    "R�pid! Alimentat m�s abans de dormir si tens m�s menjar.";
            //txtMissatge.text = "Con la Alimentaci�n actual, despertar�s ma�ana con la Resistencia al 25% y la Alimentaci�n a 0.\n" + 
            //    "�R�pido!�Alim�ntate m�s antes de dormir si tienes m�s comida!";
            txtMissatge.text = "With the current Feeding, you'll wake up tomorrow with 25% Stamina and no Feeding.\n" + 
                "Quick! Feed yourself before sleeping if you have some food!";
        }
    }


}
