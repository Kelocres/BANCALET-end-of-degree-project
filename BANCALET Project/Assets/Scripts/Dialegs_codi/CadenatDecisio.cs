using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadenatDecisio : LiniaDecisio
{
    // Start is called before the first frame update
    public Clau_BtnDecisio[] opcionsBloquetjades;
    public BtnDesicio[] opcionsEixida;

    public override void SetLiniaDecisio(DataLiniaDecisio data)
    {
        //Funció idéntica a la que hereda, sols que les opcions bloquejades deuran editar-se
        //sense utilitzar JSON
        nom = data.nom;
        idPersonatge = data.idPersonatge;
        //Els anims es deuen crear amb AddComponent
        //Els AddComponent es deuen fer des d'una classe MonoBehaviour
        /*anims = new Personatge_Anim[data.anims.Length];
        for (int j = 0; j < anims.Length; j++)
        {
            anims[j] = new Personatge_Anim(data.anims[j]);
        }*/
        textValencia = data.textValencia;
        textCastellano = data.textCastellano;
        textEnglish = data.textEnglish;

        for (int j = 0; j < opcionsEixida.Length; j++)
        {
            opcionsEixida[j].SetTexte(data.opcions[j]);
        }
    }

    public override BtnDesicio[] GetOpcions()
    {
        List<BtnDesicio> opcions = new List<BtnDesicio>(opcionsEixida);

        //Afegir opcions que s'hagen desbloquetjat
        for(int i=0; i<opcionsBloquetjades.Length; i++)
        {
            Clau_BtnDecisio actual = opcionsBloquetjades[i];
            bool totesLesClaus = true;
            for (int j = 0; j < actual.claus.Length; j++)
            {
                if (!ManejaClaus.instance.ComprobarClau(actual.claus[j]))
                {
                    totesLesClaus = false;
                    break;
                }

            }
            if (totesLesClaus)
                opcions.Add(actual.opcio);
        }

        //Retornem la llista convertida en array
        return opcions.ToArray();
    }
}

public class Clau_BtnDecisio
{
    public string[] claus;
    public BtnDesicio opcio;
}
