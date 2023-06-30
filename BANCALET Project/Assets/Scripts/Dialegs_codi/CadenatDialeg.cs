using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadenatDialeg : MonoBehaviour
{
    // Start is called before the first frame update
    public Clau_Dialeg[] dialegsBloquetjats;
    public Dialeg dialegEixida;
    

    public Dialeg DonarDialeg()
    {
        for(int i=0; i<dialegsBloquetjats.Length; i++)
        {
            Clau_Dialeg actual = dialegsBloquetjats[i];
            bool totesLesClaus = true;
            //Comprobar cada clau
            for (int j=0; j<actual.claus.Length; j++)
            {
                if(!ManejaClaus.instance.ComprobarClau(actual.claus[j]))
                {
                    totesLesClaus = false;
                    break;
                }

            }
            if (totesLesClaus)
                return actual.dialeg;
        }

        //Com no es pot ningún dels bloquetjats, es trau el dialeg d'eixida
        return dialegEixida;
    }
}

public class Clau_Dialeg
{
    public string[] claus;
    public Dialeg dialeg;
}
