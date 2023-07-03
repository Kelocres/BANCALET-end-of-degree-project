using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventoryManager : MonoBehaviour
{/*



    //[SerializeField]
    //The Array below works as a Dictionary: each Sprite corresponds to one TipusItem
    // (Is static so SlotObject from the slotPrefabs can access and get the Sprites)
    // PROBLEMA:
    //      SI LA VARIABLE ES EST�TICA, NO ES MOSTRA EN L'EDITOR DE UNITY
    // SOLUCI�:
    //      CREAR VARIABLES P�BLIQUES, AFEGIRLI ELS VALORS EN EDITOR, I AL COMEN�AR
    //      ES PASSEN ELS SEUS VALORS A LES VARIABLES STATIC
    public GameObject edit_slotPrefab;
    [SerializeField]
    public TipusItem_Sprite[] edit_slotSprites;
    public Sprite edit_slotSpriteBuit;

    //Les variables static:
    public static GameObject slotPrefab;
    public static TipusItem_Sprite[] slotSprites;
    public static Sprite slotSpriteBuit;

    //El slot que est� seleccionat
    public static SlotInventari selectSlot;

    //El slot amb el qual es canviar� la posici� el slot seleccionat
    public static SlotInventari exchangeSlot;

    void Start()
    {
        //Quan s'inicia el PlayMode, la PantallaInventari estar� segurament desactivada
        //En el moment que s'active, es cridar� el Start();
        slotPrefab = edit_slotPrefab;
        slotSprites = edit_slotSprites;
        slotSpriteBuit = edit_slotSpriteBuit;
    }

    public static Sprite GetSlotSprite(TipusItem tipus)
    {
        if (slotSprites.Length > 0)
        {
            foreach (TipusItem_Sprite tis in slotSprites)
                if (tis.tipusItem == tipus) return tis.sprite;
        }

        return slotSpriteBuit;
    }*/
}
