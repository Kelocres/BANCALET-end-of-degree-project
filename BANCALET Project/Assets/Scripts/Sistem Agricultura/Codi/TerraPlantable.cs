using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraPlantable : MonoBehaviour
{

    //Posició on col·locar les llavors o la planta
    private Transform posCultiu;
    private PlantaScript planta;

    //Image de la textura de la terra (per a senyalar que està arruixada, seca, etc)
    //private SpriteRenderer spriteTerra;
    //public Sprite spriteTerraSeca;
    //public Sprite spriteTerraArruixada;

    private Renderer rendTerra;
    public Material matTerraNoTreballada;
    public Material matTerraTreballada;
    public Material matTerraArruixada;



    //public bool plantat;
    public bool treballat;
    public bool arruixat;
    private int contadorDies = 0;


    void Start()
    {
        /*spriteTerra = GetComponentInChildren<SpriteRenderer>();
        if (spriteTerraSeca != null)
            spriteTerra.sprite = spriteTerraSeca;*/

        rendTerra = GetComponentInChildren<Renderer>();
        if (rendTerra != null)
            rendTerra.material = matTerraNoTreballada;

        posCultiu = transform.GetChild(0).transform;
        //plantat = false;
        arruixat = false;
        treballat = false;
    }

    // Update is called once per frame
    public void NouDia()
    {
        if (!treballat) return;

        if (arruixat || planta!=null)
        {
            contadorDies = 0;
            if (planta != null)
            {
                planta.NouDia();
                
            }
            arruixat = false;
        }
        else
        {
            contadorDies++;
            if (contadorDies >= 3)
            {
                treballat = false;
                if (rendTerra != null)
                    rendTerra.material = matTerraNoTreballada;
                return;
            }
        }
        if (rendTerra != null)
            rendTerra.material = matTerraTreballada;
    }

    //public void PlantarEnTerra(GameObject objectePlanta, SO_ItemLlavor llavor)
    public void PlantarEnTerra(GameObject objectePlanta, SO_SeedItem llavor)
    {
        Debug.Log("TerraPlantable PlantarEnTerra()");
        if (!treballat)
        {
            return;
        }
        //Com el objecte TerraPlantable de prova està escalat, si instanciem la planta
        //en el transform del fill acabarà deformada
        //GameObject plantacio = Instantiate(objectePlanta, posCultiu);
        GameObject plantacio = Instantiate(objectePlanta,
            posCultiu.position, Quaternion.Euler(0f, 0f, 0f));

        planta = plantacio.GetComponent<PlantaScript>();
        planta.Plantar(llavor);
        //planta.dSecar += SecarTerra;
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
        if (treballat && !arruixat)
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

    public void Treballar()
    {
        //Si ja estava arruixat, no cal fer res
        if (!treballat)
        {
            treballat = true;

            //Canviar l'apariència de terra seca a arruixada
            //if(spriteTerraArruixada!=null)
            //    spriteTerra.sprite = spriteTerraArruixada;
            if (rendTerra != null)
                rendTerra.material = matTerraTreballada;
            //Comunicar a la planta que està arruixada
            //if (planta != null)
            //    planta.Arruixar();
        }
    }

    public void SecarTerra()
    {
        arruixat = false;

        //Canviar l'apariència de terra seca a arruixada
        //if (spriteTerraSeca != null)
        //    spriteTerra.sprite = spriteTerraSeca;
        if (rendTerra != null)
            rendTerra.material = matTerraTreballada;
    }

    public void PlantaArrancada()
    {
        planta = null;

    }
}
