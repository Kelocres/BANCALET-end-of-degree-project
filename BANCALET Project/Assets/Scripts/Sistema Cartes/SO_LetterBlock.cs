using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LetterBlockType
{
    Header,
    Body,
    Departure
}

[CreateAssetMenu(fileName = "New letter block", menuName = "Sistema Cartes/Bloc de carta")]
public class SO_LetterBlock : ScriptableObject
{
    public LetterBlockType type;

    [TextArea(15, 20)]
    public string text;

    public int affection;

    public string[] required_keys;
    public string[] unlocker_keys;
}
