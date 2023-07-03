using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OBSOLET_UI_MenuPausa : MonoBehaviour
{
    private bool botonsFixats = false;

    public Button btnTornar;
    public Button btnInventari;
    public Button btnTornarStart;
    public Button btnEixir;

    //Delegate per a cridar la funció de tornar al joc
    public delegate void delBotoPausa();
    public event delBotoPausa dTornarAlJoc;
    //Delegate per a cridar al menú d'inventari del jugador
    public event delBotoPausa dInventari;
    //public event delBotoPausa dTornarStart;
    void Start()
    {
        FixarBotons();
    }

    public void FixarBotons()
    {
        if(!botonsFixats)
        {

            if (btnTornar == null)
            {
                btnTornar = GameObject.FindGameObjectWithTag("btnPausa_Tornar").GetComponent<Button>();
            }
            if (btnTornar == null) Debug.Log("UI_MenuPausa Start() NO HI HA btnTornar!!!");
            else
            {
                btnTornar.onClick.AddListener(TornarAlJoc);
            }

            if (btnInventari == null)
            {
                btnInventari = GameObject.FindGameObjectWithTag("btnPausa_Inventari").GetComponent<Button>();
            }
            if (btnInventari == null) Debug.Log("UI_MenuPausa Start() NO HI HA btnInventari!!!");
            else
            {
                btnInventari.onClick.AddListener(Inventari);
            }
            
            if (btnTornarStart == null)
            {
                btnTornarStart = GameObject.FindGameObjectWithTag("btnPausa_TornarStart").GetComponent<Button>();
            }
            if (btnTornarStart == null) Debug.Log("UI_MenuPausa Start() NO HI HA btnTornarStart!!!");
            else
            {
                btnTornarStart.onClick.AddListener(TornarMenuStart);
            }

            if (btnEixir == null)
            {
                btnEixir = GameObject.FindGameObjectWithTag("btnPausa_Eixir").GetComponent<Button>();
            }
            if (btnEixir == null) Debug.Log("UI_MenuPausa Start() NO HI HA btnEixir!!!");
            else
            {
                btnEixir.onClick.AddListener(EixirDelJoc);
            }

            botonsFixats = true;
        }
    }

    

    public void TornarAlJoc()
    {
        //if(dTornarAlJoc != null)     dTornarAlJoc();
        dTornarAlJoc?.Invoke();
    }

    public void Inventari()
    {
        //if (dInventari != null)      dInventari();
        dInventari?.Invoke();
    }

    public void TornarMenuStart()
    {
        Debug.Log("UI_MenuPausa TornarMenuStart()");
        //if (dTornarStart != null) dTornarStart();
        //dTornarStart?.Invoke();
    }

    public void EixirDelJoc()
    {
        Debug.Log("UI_MenuPausa EixirDelJoc()");
        Application.Quit();
    }
}
