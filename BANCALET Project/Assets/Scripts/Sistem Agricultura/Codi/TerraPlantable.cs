using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraPlantable : MonoBehaviour
{

    //Posici� on col�locar les llavors o la planta
    private Transform posCultiu;
    private PlantaScript planta;

    //Image de la textura de la terra (per a senyalar que est� arruixada, seca, etc)
    private SpriteRenderer spriteTerra;
    public Sprite spriteTerraSeca;
    public Sprite spriteTerraArruixada;

    private Renderer rendTerra;
    public Material matTerraSeca;
    public Material matTerraArruixada;


    //public bool plantat;
    public bool arruixat;

    void Start()
    {
        /*spriteTerra = GetComponentInChildren<SpriteRenderer>();
        if (spriteTerraSeca != null)
            spriteTerra.sprite = spriteTerraSeca;*/

        rendTerra = GetComponentInChildren<Renderer>();
        if (rendTerra != null)
            rendTerra.material = matTerraSeca;

        posCultiu = transform.GetChild(0).transform;
        //plantat = false;
        arruixat = false;
    }

    // Update is called once per frame
    public void NouDia()
    {
        //La terra es seca
        arruixat = false;
        //if (spriteTerraSeca != null)
        //    spriteTerra.sprite = spriteTerraSeca;
        if (rendTerra != null)
            rendTerra.material = matTerraSeca;
    }

    //public void PlantarEnTerra(GameObject objectePlanta, SO_ItemLlavor llavor)
    public void PlantarEnTerra(GameObject objectePlanta, SO_SeedItem llavor)
    {
        Debug.Log("TerraPlantable PlantarEnTerra()");
        //Com el objecte TerraPlantable de prova est� escalat, si instanciem la planta
        //en el transform del fill acabar� deformada
        //GameObject plantacio = Instantiate(objectePlanta, posCultiu);
        GameObject plantacio = Instantiate(objectePlanta,
            posCultiu.position, Quaternion.Euler(0f, 0f, 0f));

        planta = plantacio.GetComponent<PlantaScript>();
        planta.Plantar(llavor);
        planta.dSecar += SecarTerra;
        planta.dArrancar += PlantaArrancada;


        if (arruixat)
            planta.Arruixar();
    }

    //Per a comprobar que posCultiu est� incialitzat o no
    public bool ComprovarPosCultiu()
    {
        return posCultiu != null;
    }

    public bool ComprobarAmbPlanta()
    {
        return planta != null;
    }

    public void Arruixar()
    {
        //Si ja estava arruixat, no cal fer res
        if (!arruixat)
        {
            arruixat = true;

            //Canviar l'apari�ncia de terra seca a arruixada
            //if(spriteTerraArruixada!=null)
            //    spriteTerra.sprite = spriteTerraArruixada;
            if (rendTerra != null)
                rendTerra.material = matTerraArruixada;
            //Comunicar a la planta que est� arruixada
            if (planta != null)
                planta.Arruixar();
        }
    }

    public void SecarTerra()
    {
        arruixat = false;

        //Canviar l'apari�ncia de terra seca a arruixada
        //if (spriteTerraSeca != null)
        //    spriteTerra.sprite = spriteTerraSeca;
        if (rendTerra != null)
            rendTerra.material = matTerraSeca;
    }

    public void PlantaArrancada()
    {
        planta = null;

    }
}
