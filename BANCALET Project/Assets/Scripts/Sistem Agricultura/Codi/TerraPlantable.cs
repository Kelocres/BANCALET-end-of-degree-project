using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraPlantable : MonoBehaviour
{

    //Posició on col·locar les llavors o la planta
    private Transform posCultiu;
    private PlantaScript planta;

    //Image de la textura de la terra (per a senyalar que està arruixada, seca, etc)
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
        //Com el objecte TerraPlantable de prova està escalat, si instanciem la planta
        //en el transform del fill acabarà deformada
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

    //Per a comprobar que posCultiu està incialitzat o no
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

            //Canviar l'apariència de terra seca a arruixada
            //if(spriteTerraArruixada!=null)
            //    spriteTerra.sprite = spriteTerraArruixada;
            if (rendTerra != null)
                rendTerra.material = matTerraArruixada;
            //Comunicar a la planta que està arruixada
            if (planta != null)
                planta.Arruixar();
        }
    }

    public void SecarTerra()
    {
        arruixat = false;

        //Canviar l'apariència de terra seca a arruixada
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
