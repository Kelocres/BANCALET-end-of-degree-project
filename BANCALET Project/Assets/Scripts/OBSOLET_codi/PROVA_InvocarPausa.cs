using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROVA_InvocarPausa : MonoBehaviour
{
    //Per a saber si el joc està pausat (será static per a que siga accessible
    //des de qualsevol Script)
    //public static bool JocEnPausa = false;

    //Variable enum pera a saber si el joc està funcionant, en menú de pausa o inventari
    public enum EstatJoc : short {GamePlay = 0,  MenuPausa = 1, InventariJugador = 2 };
    public static EstatJoc estatJoc = EstatJoc.GamePlay;
    /*
    public static bool EstatJocEsGameplay(EstatJoc _estatJoc)           { return _estatJoc == EstatJoc.GamePlay; }
    public static bool EstatJocEsMenuPausa(EstatJoc _estatJoc)          { return _estatJoc == EstatJoc.MenuPausa; }
    public static bool EstatJocEsInventariJugador(EstatJoc _estatJoc)   { return _estatJoc == EstatJoc.InventariJugador; }
    */
    public static bool EstatJocEsGameplay() { return estatJoc == EstatJoc.GamePlay; }
    public static bool EstatJocEsMenuPausa() { return estatJoc == EstatJoc.MenuPausa; }
    public static bool EstatJocEsInventariJugador() { return estatJoc == EstatJoc.InventariJugador; }

    // I SI CREE UNA CLASSE UI_Menu de la qual hereden UI_MenuPausa, UI_InventariJugador i UI_Cartes
    //  Menú pausa
    public GameObject objecteMenuPausa;
    private OBSOLET_UI_MenuPausa scriptMenuPausa;

    // Menú inventari del jugador
    public GameObject objecteInventariJugador;
    private OBSOLET_UI_InventariJugador scriptInventari;

    void Start()
    {
        //Per a trobar els gameobjects encara que estiguen desactivats
        if(objecteMenuPausa == null || objecteInventariJugador == null)
        {
            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if(go.tag == "menuPausa" && objecteMenuPausa == null)
                {
                    objecteMenuPausa = go;
                }
                else if (go.tag == "menuInventariJugador" && objecteInventariJugador == null)
                {
                    objecteInventariJugador = go;
                }

                //Si ja tenim els dos objectes, eixim del foreach
                if (objecteMenuPausa != null && objecteInventariJugador != null)
                    break;
            }
        }

        if (objecteMenuPausa == null)
            Debug.Log("PROVA_InvocarPausa Start() NO HI HA MenuPausaUI!!!");
        else
        {
            scriptMenuPausa = objecteMenuPausa.GetComponent<OBSOLET_UI_MenuPausa>();
            scriptMenuPausa.dTornarAlJoc += TornarJoc;
            scriptMenuPausa.dInventari += PausarJoc_Inventari;
        }

        if (objecteInventariJugador == null)
            Debug.Log("PROVA_InvocarPausa Start() NO HI HA InventariJugador!!!");
        else
        {
            scriptInventari = objecteMenuPausa.GetComponent<OBSOLET_UI_InventariJugador>();
            scriptInventari.dTornarAlJoc += TornarJoc;
            scriptInventari.dMenuPausa += PausarJoc_MenuPausa;
            scriptInventari.dMenuCartes += PausarJoc_Cartes;
            //scriptMenuPausa.dTornar += TornarJoc;
        }
        /*
        //JocEnPausa = false;
        if(objecteMenuPausa == null)
        {
            objecteMenuPausa = GameObject.FindGameObjectWithTag("menuPausa");
            if (objecteMenuPausa == null) Debug.Log("PROVA_InvocarPausa Start() NO HI HA MenuPausaUI!!!");
            else
            {
                scriptMenuPausa = objecteMenuPausa.GetComponent<UI_MenuPausa>();
                scriptMenuPausa.dTornar += TornarJoc;

            }

        }*/



        TornarJoc();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //if (!JocEnPausa) PausarJoc_MenuPausa();
            /*if (EstatJocEsGameplay(estatJoc) || EstatJocEsInventariJugador(estatJoc)) PausarJoc_MenuPausa();
            else
            {
                TornarJoc();
            }*/
            if (EstatJocEsMenuPausa()) TornarJoc();
            else
            {
                PausarJoc_MenuPausa();
            }
        }

        else if (Input.GetKeyDown(KeyCode.I))
        {
            //if (!JocEnPausa) PausarJoc_MenuPausa();
            /*if (EstatJocEsGameplay(estatJoc) || EstatJocEsMenuPausa(estatJoc)) PausarJoc_Inventari();
            else
            {
                TornarJoc();
            }*/
            if (EstatJocEsInventariJugador()) TornarJoc();
            else
            {
                PausarJoc_Inventari();
            }
        }

    }

    public void PausarJoc_MenuPausa()
    {
        if (objecteMenuPausa != null)
            objecteMenuPausa.SetActive(true);

        if (objecteInventariJugador != null)
            objecteInventariJugador.SetActive(false);

        if(Time.timeScale != 0f)Time.timeScale = 0f;
        //JocEnPausa = true;
        estatJoc = EstatJoc.MenuPausa;
    }

    public void PausarJoc_Inventari()
    {
        if (objecteMenuPausa != null)
            objecteMenuPausa.SetActive(false);

        if (objecteInventariJugador != null)
            objecteInventariJugador.SetActive(true);

        Time.timeScale = 0f;
        //JocEnPausa = true;
        estatJoc = EstatJoc.InventariJugador;
    }

    public void PausarJoc_Cartes()
    {
        //Fer apareixer el menú de les cartes

    }

    public void TornarJoc()
    {
        if(objecteMenuPausa != null)
            objecteMenuPausa.SetActive(false);

        if (objecteInventariJugador != null)
            objecteInventariJugador.SetActive(false);

        Time.timeScale = 1f;
        //JocEnPausa = false;
        estatJoc = EstatJoc.GamePlay;
    }
}
