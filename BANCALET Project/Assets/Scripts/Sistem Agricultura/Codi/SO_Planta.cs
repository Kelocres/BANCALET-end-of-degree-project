using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova planta", menuName = "Planta de cultiu")]
public class SO_Planta : ScriptableObject
{
    public FasePlanta[] fases;
    //public SO_Item collita;
    public SO_HarvestItem collita;
    public float aiguaNecessaria;

    public Sprite spriteMort;
    //public float aiguaRegada; //Aquesta variable en PlantaScript
    //private int diaPlantacio;
    //public int diesDeVida = 0;
    //public int diaDeMort;
}

[System.Serializable]
public class FasePlanta
{
    //Número del últim dia en que dura esta fase
    public int finsDia;
    //Sprite per a l'aspecte de la planta (sense collita)
    public Sprite spriteSenseCollita;
    //Sprite per a l'aspecte de la planta amb collita (tomaques, raim, etc)
    public Sprite spriteAmbCollita;
    //Número màxim de consumibles que aporta (si es cero, no dona res encara)
    public int maxCollita;
}
