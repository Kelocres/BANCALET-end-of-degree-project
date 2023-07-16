using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Objective", menuName = "Sistema Inventari/Objectiu")]
public class SO_GameObjective : ScriptableObject
{
    public string description;
    public SO_InventoryObject requiredItems;
    public SO_InventoryObject rewardItems;

}
