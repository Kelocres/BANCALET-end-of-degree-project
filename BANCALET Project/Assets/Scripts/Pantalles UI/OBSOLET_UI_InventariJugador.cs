using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OBSOLET_UI_InventariJugador : MonoBehaviour
{

    //Inventari del jugador
    //Deu tindre al menys 10 slots
    //Els primers 10 slots seran per a la barra d'items
    //public SO_Inventari inventari;
    public SO_InventoryObject inventari;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPECE_BETWEEN_ITEMS;

    //Dictionary<SlotInventari, GameObject> itemsMostrats = new Dictionary<SlotInventari, GameObject>();
    Dictionary<InventorySlot, GameObject> itemsMostrats = new Dictionary<InventorySlot, GameObject>();


    public GameObject slotPrefab;
    //[SerializeField]
    //The Array below works as a Dictionary: each Sprite corresponds to one TipusItem
    // (Is static so SlotObject from the slotPrefabs can access and get the Sprites)
    // PROBLEMA:
    //      SI LA VARIABLE ES ESTÀTICA, NO ES MOSTRA EN L'EDITOR DE UNITY
    // SOLUCIÓ:
    //      CREAR VARIABLES PÚBLIQUES, AFEGIRLI ELS VALORS EN EDITOR, I AL COMENÇAR
    //      ES PASSEN ELS SEUS VALORS A LES VARIABLES STATIC
    [SerializeField]
    public  TipusItem_Sprite[] edit_slotSprites;
    public  Sprite edit_slotSpriteBuit;

    //Les variables static:
    public static TipusItem_Sprite[] slotSprites;
    public static Sprite slotSpriteBuit;

    //Botons de l'inventari
    public Button btnTornar;
    public Button btnMenuPausa;
    public Button btnCartes;

    //Delegates dels botons
    public delegate void delBotoInventari();
    public event delBotoInventari dTornarAlJoc;
    public event delBotoInventari dMenuPausa;
    public event delBotoInventari dMenuCartes;
    //alreadySetBotons = false;
    private bool botonsFixats = false;


    void Start()
    {
        //Quan s'inicia el PlayMode, la PantallaInventari estarà segurament desactivada
        //En el moment que s'active, es cridarà el Start();
        slotSprites = edit_slotSprites;
        slotSpriteBuit = edit_slotSpriteBuit;

        //Debug.Log("UI_InventariJugador Start()");
        GetInventory();

        if(inventari == null)
        {
            Debug.Log("UI_InventariJugador Start() NO HI HA INVENTARI");
            return;
        }

        CrearMenuInventari();
    }

    //public void SetBotons()
    public void FixarBotons()
    {
        if (botonsFixats) return;

        if (btnTornar == null)
        {
            btnTornar = GameObject.FindGameObjectWithTag("btnInventari_Tornar").GetComponent<Button>();
        }
        if (btnTornar == null) Debug.Log("UI_InventariJugador FixarBotons() NO HI HA btnTornar!!!");
        else
        {
            btnTornar.onClick.AddListener(TornarAlJoc);
        }

        if (btnMenuPausa == null)
        {
            btnMenuPausa = GameObject.FindGameObjectWithTag("btnInventari_MenuPausa").GetComponent<Button>();
        }
        if (btnMenuPausa == null) Debug.Log("UI_InventariJugador FixarBotons() NO HI HA btnMenuPausa!!!");
        else
        {
            btnMenuPausa.onClick.AddListener(MenuPausa);
        }

        if (btnCartes == null)
        {
            btnCartes = GameObject.FindGameObjectWithTag("btnInventari_Cartes").GetComponent<Button>();
        }
        if (btnCartes == null) Debug.Log("UI_InventariJugador FixarBotons() NO HI HA btnCartes!!!");
        else
        {
            btnCartes.onClick.AddListener(MenuCartes);
        }


        botonsFixats = false;
    }

    public void TornarAlJoc()
    {
        //if (dTornarAlJoc != null) dTornarAlJoc();
        dTornarAlJoc?.Invoke();
    }

    public void MenuPausa()
    {
        //if (dMenuPausa != null) dMenuPausa();
        dMenuPausa?.Invoke();
    }

    public void MenuCartes()
    {
        //if (dMenuPausa != null) dMenuPausa();
        dMenuCartes?.Invoke();
    }

    //private void ObtindreInventari()
    private void GetInventory()
    {
        if (FindObjectOfType<PJ_StateManager>().inventari == null)
            Debug.Log("UI_InventariJugador GetInventory() FindObjectOfType<PJ_StateManager>().inventari == NULL !!!");
        else
            inventari = FindObjectOfType<PJ_StateManager>().inventari;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inventari != null && PROVA_InvocarPausa.EstatJocEsInventariJugador())
        {
            //ActualitzaPantalla();
            UpdateDisplay();
        }
    }

    public void CrearMenuInventari()
    {
        /*
        //for (int i=0; i < inventari.Contenedor.Length; i++)
        for (int i = 0; i < inventari.Container.Items.Length; i++)
        {
            //Crear slot i fixar la seua posició i rotació en el món i el seu pare
            //(Segurament hi haurà que agafar sols el sprite de la icona del SO_Item)
            /*if(inventari.Contenedor[i]==null)
            {
                Debug.Log("UI_InventariJugador CrearMenuInventari() inventari.Contenedor[" + i + "] == null");
                return;
            }
            if (inventari.Contenedor[i].item == null)
            {
                Debug.Log("UI_InventariJugador CrearMenuInventari() inventari.Contenedor[" + i + "].item == null");
                return;
            }
            if (inventari.Contenedor[i].item.icona == null)
            {
                Debug.Log("UI_InventariJugador CrearMenuInventari() inventari.Contenedor[" + i + "].item.icona == null");
                return;
            }

            //var obj = Instantiate(inventari.Contenedor[i].item.icona, Vector3.zero, Quaternion.identity, transform);
            var obj = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);

            //Asignar valor al slot (si no està buit, li afegim la icona i quantitat; si ho està, el buidem)
            SlotObject slotObject = obj.GetComponent<SlotObject>();
            slotObject.AfegirSlot(inventari.Container.Items[i]);

            //Colocar slot en la posició local desitjada
            obj.GetComponent<RectTransform>().localPosition = GetSlotPosition(i);

            //Mostrar la quantitat de item en el text
            //obj.GetComponentInChildren<TextMeshProUGUI>().text = inventari.Contenedor[i].quantitat.ToString("n0");

            //Afegir el item creat al diccionari
            itemsMostrats.Add(inventari.Contenedor[i], obj);
        }*/
    }

    //public Vector3 ObtindrePosicioSlot(int i)
    public Vector3 GetSlotPosition(int i)
    {
        return new Vector3( X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)), 
                            Y_START + (-Y_SPECE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 
                            0f);
    }
    public void UpdateDisplay()
    {/*
        for(int i=0; i < inventari.Contenedor.Length; i++)
        {
            //ESTA PART DEL CODI POT SER TOTALMENT INNECESSÀRIA
            //Si ja hi ha un slot d'eixe item, es suma a la quantitat
            if (itemsMostrats.ContainsKey(inventari.Contenedor[i]))
            {
                itemsMostrats[inventari.Contenedor[i]].GetComponentInChildren<TextMeshProUGUI>().text =
                    inventari.Contenedor[i].quantitat.ToString("n0");
            }
            // Si no, es crea el slot del item
            else
            {
                if (inventari.Contenedor[i] == null)
                {
                    Debug.Log("UI_InventariJugador UpdateDisplay() inventari.Contenedor[" + i + "] == null");
                    return;
                }
                if (inventari.Contenedor[i].item == null)
                {
                    Debug.Log("UI_InventariJugador UpdateDisplay() inventari.Contenedor[" + i + "].item == null");
                    return;
                }
                if (inventari.Contenedor[i].item.icona == null)
                {
                    Debug.Log("UI_InventariJugador UpdateDisplay() inventari.Contenedor[" + i + "].item.icona == null");
                    return;
                }
                //Crear slot y fixar la seua posició i rotació en el mon, i el seu pare
                //var obj = Instantiate(inventari.Contenedor[i].item.icona, Vector3.zero, Quaternion.identity, transform);
                var obj = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, transform);
                
                SlotObject slotObject = obj.GetComponent<SlotObject>();
                slotObject.AfegirSlot(inventari.Contenedor[i]);
                //Colocar slot en la posició local desitjada
                obj.GetComponent<RectTransform>().localPosition = GetSlotPosition(i);

                //Mostrar la quantitat de item en el text
                //obj.GetComponentInChildren<TextMeshProUGUI>().text = 
                //    inventari.Contenedor[i].quantitat.ToString("n0");
                //Afegir el item creat al diccionari
                itemsMostrats.Add(inventari.Contenedor[i], obj);
            }
        }*/
    }

    /*public static Sprite GetSlotSprite(TipusItem tipus)
    {
        if(slotSprites.Length >0)
        {
            foreach (TipusItem_Sprite tis in slotSprites)
                if (tis.tipusItem == tipus) return tis.sprite;
        }

        return slotSpriteBuit;
    }*/


}
/*[System.Serializable]
public struct TipusItem_Sprite
{
    public TipusItem tipusItem;
    public Sprite sprite;
}*/
