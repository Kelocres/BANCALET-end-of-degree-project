using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaScript : MonoBehaviour
{
    public SO_Planta tipusPlanta;

    //public float aiguaRegada; //Aquesta variable en PlantaScript
    private int diaPlantacio;

    //Els dies de vida quantifiquen els dies que porta viva fins que arribe DiaDeMort
    public int diesDeVida;
    private bool mort;

    //Els dies de progrés quantifiquen els avanços en el seu creixement
    public int diesDeProgres;
    //public float aiguaRegada;
    private bool arruixada;

    //Delegate per a comunicar a TerraPlantable que ha de secarse
    public delegate void delTerra();
    public event delTerra dSecar;
    //Delegate per a comunicar a TerraPlantable que està lliure
    public event delTerra dArrancar;

    public int diaDeMort;
    
    public int numFase;
    private FasePlanta faseActual;
    //private SO_Item collita;
    private SO_HarvestItem collita;
    //private SO_ItemObject collita;
    public int maxCollita = 0;
    public int collitaActual = 0;
    public int horaPlantacio;
    private SpriteRenderer spritePlanta;

    private void Start()
    {
        //Plantar();
    }

    public void Plantar(SO_SeedItem llavor)
    {
        Debug.Log("PlantaScript Plantar()");
        //Conectar amb algun script de manejar per a controlar el dia de plantació, 
        //quan comença el següent dia, etc
        ManejaPlantes.instance.NovaPlanta(this);

        
        //aiguaRegada = 0;    //O aiguaRegada = Aigua en TerraPlantable
        diesDeVida = 0;
        spritePlanta = GetComponentInChildren<SpriteRenderer>();

        //Objtindre SO_Planta per al creixement, sprites i demés data
        tipusPlanta = llavor.planta;

        //Activar fasePlanta[0]
        numFase = 0;
        mort = false;
        faseActual = tipusPlanta.fases[numFase];
        spritePlanta.sprite = faseActual.spriteSenseCollita;
        diaDeMort = tipusPlanta.fases[tipusPlanta.fases.Length - 1].finsDia;
        arruixada = false;


    }

    public void NouDia()
    {
        diesDeVida++;

        //Si la planta està morta, no es fan més comprovacions
        if (mort) return;

        //Generació aleatòria de collita
        if (collita != null && maxCollita > 0 && arruixada)
        {
            collitaActual = Random.Range(0, maxCollita + 1);
            if (collitaActual > 0)
            {
                if (faseActual.spriteAmbCollita != null)
                    spritePlanta.sprite = faseActual.spriteAmbCollita;
                else
                    Debug.Log("PlantaScript NouDia() NO HI HA SpriteAmbCollita en la fase " + numFase);

                Debug.Log("PlantaScript NouDia() Collita disponible: " + collitaActual);
            }
            else
                if (faseActual.spriteSenseCollita != null)
                spritePlanta.sprite = faseActual.spriteSenseCollita;
            else
                Debug.Log("PlantaScript NouDia() NO HI HA SpriteSenseCollita en la fase " + numFase);
        }

        //Si la planta està regada, s'avança en dies de progrés i es torna a secar
        //la planta i la terra
        if (arruixada)
        {
            diesDeProgres++;
            arruixada = false;
            if (dSecar != null)
                dSecar();
        }

        Debug.Log("PlantaScript NouDia() Dies de vida: "+diesDeVida+", dia de Mort: "+diaDeMort);        
        /*if(diesDeProgres >= tipusPlanta.fases[numFase].finsDia)
        {
            if (diesDeVida < diaDeMort)
                NovaFasePlanta();
            else
                PosDiaMort();
        }*/
        if(diesDeVida >= diaDeMort)
            PosDiaMort();
        else if(diesDeProgres >= tipusPlanta.fases[numFase].finsDia)
            NovaFasePlanta();

    }

    private void NovaFasePlanta()
    {
        Debug.Log("PlantaScript NovaFasePlanta()");
        numFase++;
        faseActual = tipusPlanta.fases[numFase];
        if(faseActual.spriteSenseCollita!=null)
            spritePlanta.sprite = faseActual.spriteSenseCollita;
        else
            Debug.Log("PlantaScript NovaFasePlanta() NO HI HA SpriteSenseCollita en la fase "+numFase);
        if (tipusPlanta.collita!=null && faseActual.maxCollita > 0)
        {
            collita = tipusPlanta.collita;
            maxCollita = faseActual.maxCollita;
        }
    }

    private void PosDiaMort()
    {
        Debug.Log("PlantaScript PostDiaMort()");
        mort = true;
        collitaActual = 0;
        if (tipusPlanta.spriteMort != null)
            spritePlanta.sprite = tipusPlanta.spriteMort;

    }

    public void Arrancar()
    {
        //Cridar a TerraPlantable
        if (dArrancar != null)
            dArrancar();

        //Llevar planta de la llista del ManejaPlantes
        ManejaPlantes.instance.EliminarPlanta(this);

        //Eliminar planta
        Destroy(this.gameObject);
        
    }

    public bool ComprovarQuantitatCollita()
    {
        return collitaActual > 0;
    }

    //public SO_Item ComprovarTipusCollita()
    public SO_HarvestItem ComprovarTipusCollita()
    {
        return collita;
    }


    //public SlotInventari Collir()
    public InventorySlot Collir()
    {
        /*
        int collitaDonada = collitaActual;
        collitaActual = 0;
        if (faseActual.spriteSenseCollita != null)
            spritePlanta.sprite = faseActual.spriteSenseCollita;

        return collitaDonada;
        */

        //Crear Slot de SO_Item per a poder pasar el item i la quantitat
        //SlotInventari slotCollita = new SlotInventari();
        InventorySlot slotCollita = new InventorySlot();
        //slotCollita.ReplenarSlot(collita, collitaActual);
        slotCollita.UpdateSlot(collita.data, collitaActual);

        collitaActual = 0;
        if (faseActual.spriteSenseCollita != null)
            spritePlanta.sprite = faseActual.spriteSenseCollita;

        return slotCollita;
        //FA FALTA COMPROVAR SI L'INVENTARI TÉ ESPAI PER A LA COLLITA!!!
    }

    public void Arruixar()
    {
        arruixada = true;
    }
}
