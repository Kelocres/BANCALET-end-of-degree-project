using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public UI_Menu[] menus;
    private UI_Menu menuActivat;

    //Per a comprovar que la tecla d'un menú ha sigut pulsada
    private KeyCode[] keycodes;


    // Start is called before the first frame update
    void Start()
    {
        keycodes = new KeyCode[menus.Length];
        for(int i=0; i< menus.Length; i++)
        {
            menus[i].SetButtons();
            menus[i].backToGame += BackToGame;
            menus[i].changeMenu += ChangeMenu;

            keycodes[i] = menus[i].keycode;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobar cada botó
        for(int i=0; i<keycodes.Length; i++)
        {
            if(Input.GetKeyDown(keycodes[i]))
            {
                Debug.Log("MenuManager Update() keycode[" + i + "] down");
                if (menuActivat == null)
                    ShowMenu(i);
                //DEGUT A UN BUG QUE ES PRODUIX AL EIXIR D'ALGUNS MENÚS AMB LA TECLA
                //NOMÉS ES PERMET FER LES EIXIDES O CANVIS DE MENÚ
                //AMB ELS BOTONS DEL CANVAS
                /*
                else if (menuActivat == menus[i])
                    BackToGame();
                else
                    ChangeMenu(i);
                */
            }
        }
    }

    
    public void ShowMenu(int numMenu)
    {
        Debug.Log("MenuManager ShowMenu(int)");
        menuActivat = menus[numMenu];

        menuActivat.gameObject.SetActive(true);
        Time.timeScale = 0f;

        //Bloquetjar CursorSelector mentre es mostren els menús
        Debug.Log("MenuManager Show Menu() cridar a  CursorSelector.BlockByUI()");
        CursorSelector.BlockByMenu();
    }

    public void ShowMenu(UI_Menu menu)
    {
        int index = GetMenuNumber(menu);
        if (index == -1 || index >= menus.Length)
            Debug.Log("MenuManager ShowMenu(UI_Menu) menu not found in array!!!");
        else ShowMenu(index);
    }

    public void BackToGame()
    {
        if (menuActivat == null) return;

        menuActivat.gameObject.SetActive(false);
        menuActivat = null;

        foreach (UI_Menu menu in menus)
            menu.HideAlternativeGroup();

        Time.timeScale = 1f;

        //Desbloquetjar CursorSelector
        CursorSelector.UnblockByMenu();
    }

    public void ChangeMenu(int numMenu)
    {
        Debug.Log("MenuManager ChangeMenu(" + numMenu + ")");
        menuActivat.gameObject.SetActive(false);

        menuActivat = menus[numMenu];
        menuActivat.gameObject.SetActive(true);
    }

    //Éste es crida des dels botons dels menus
    public void ChangeMenu(UI_Menu newMenu)
    {
        int index = GetMenuNumber(newMenu);
        if (index == -1 || index >= menus.Length)
            Debug.Log("MenuManager ShowMenu(UI_Menu) menu not found in array!!!");
        else ChangeMenu(index);
    }

    private int GetMenuNumber(UI_Menu searchMenu)
    {
        for(int i=0; i<menus.Length; i++)
        {
            if (menus[i] == searchMenu)
                return i;
        }

        return -1;
    }
}
