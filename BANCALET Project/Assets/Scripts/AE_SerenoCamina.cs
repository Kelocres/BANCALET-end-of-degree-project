using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AE_SerenoCamina : ActivadorEvent
{
    //Llista de CTs:
    //  - CT davant de la porta, guia al sereno cap a fora del local
    //  - CT fora del local, ordena que es tanquen les portes
    //      i que es pare el sereno
    public override void Activar()
    {
        Debug.Log("AE_SerenoCamina Activar()");
    }
}
