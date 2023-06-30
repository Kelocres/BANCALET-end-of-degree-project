using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nova planta", menuName = "Planta de cultiu (versio plana)")]
public class SO_PlantaTex : ScriptableObject
{
    public FasePlantaTex[] fases;
    //public SO_Item collita;
    public SO_HarvestItem collita;
    public float aiguaNecessaria;

    //public Sprite spriteMort;
    public Material matMort;

    //public float aiguaRegada; //Aquesta variable en PlantaScript
    //private int diaPlantacio;
    //public int diesDeVida = 0;
    //public int diaDeMort;
    public bool esArbre = false;
}
[System.Serializable]
public class FasePlantaTex
{
    //Número del últim dia en que dura esta fase
    public int finsDia;
    //Sprite per a l'aspecte de la planta (sense collita)
    public Material matSenseCollita;
    //Sprite per a l'aspecte de la planta amb collita (tomaques, raim, etc)
    public Material matAmbCollita;
    //Número màxim de consumibles que aporta (si es cero, no dona res encara)
    public int maxCollita;
}
