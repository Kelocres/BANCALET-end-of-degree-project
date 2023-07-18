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

    //Esta funció estarà en UI_MenuObjectius
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
            //txtMissatge.text = "Estàs completament alimentat, així que despertaràs demà amb la Resistencia plena i la Alimentació a la mitat.";
            txtMissatge.text = "Estás completamente alimentado, así que despertarás mañana con la Resistencia llena y la Alimentación a la mitad.";
        }
        else if (feedingSystem.currentValue >= feedingSystem.DEFICIENT_VALUE)
        {
            //txtMissatge.text = "Amb la alimentació actual, despertaràs demà amb la Resistencia a la mitat i la Alimentació al 25%.\n" +
            //    "Si tens més menjar, alimentat un poc més abans d'anar a dormir.";
            txtMissatge.text = "Con la Alimentación actual, despertarás mañana con la Resistencia a la mitad y la Alimentación al 25%.\n"+
                "Si tienes más comida, aliméntate un poco más antes de ir a dormir.";
        }
        else
        {
            //txtMissatge.text = "Amb la alimentació actual, despertaràs demà amb la Resistencia al 25% i la Alimentació a 0.\n" +
            //    "Ràpid! Alimentat més abans de dormir si tens més menjar.";
            txtMissatge.text = "Con la Alimentación actual, despertarás mañana con la Resistencia al 25% y la Alimentación a 0.\n" + 
                "¡Rápido!¡Aliméntate más antes de dormir si tienes más comida!";
        }
    }


}
