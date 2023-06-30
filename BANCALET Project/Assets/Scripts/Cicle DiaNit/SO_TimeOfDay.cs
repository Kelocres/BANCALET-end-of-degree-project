using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nou arxiu Time of Day", menuName = "TimeOfDay")]
public class SO_TimeOfDay : ScriptableObject
{
    [SerializeField, Range(0, 24)] public float TimeOfDay;
    public int CountedDays = 0;
}
