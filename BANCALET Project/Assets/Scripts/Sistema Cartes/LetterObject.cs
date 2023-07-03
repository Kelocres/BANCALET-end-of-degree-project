using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Character_Letter
{
    Miller,
    RetiredMan,
    PC_Sister,
    Player

}

public class LetterObject : MonoBehaviour
{
    public Character_Letter sender;
    public Character_Letter receiver;

    /*
    public SO_LetterBlock headerBlock;
    public SO_LetterBlock[] bodyBlock;
    public SO_LetterBlock departureBlock;
    */
    public string letterText;

    public int totalAffection;
    public List<string> totalUnlocker_keys;

    //public bool fullyWriten = false;

    //Els dies en que tardarà en arribar a la bústia
    //Les cartes del jugador tindran 0 dies
    public int daysForArrival = 0;

    public LetterObject(Character_Letter _sender, Character_Letter _receiver, List<SO_LetterBlock> blocks)
    {
        sender = _sender;
        receiver = _receiver;

        letterText = "";
        totalAffection = 0;
        totalUnlocker_keys = new List<string>();

        for(int i=0; i<blocks.Count; i++)
        {
            letterText += blocks[i].text;
            if (i != blocks.Count - 1)
                letterText += "\n\n";

            totalAffection += blocks[i].affection;

            if (blocks[i].unlocker_keys.Length > 0)
                foreach (string key in blocks[i].unlocker_keys)
                    totalUnlocker_keys.Add(key);
        }
    }

    public LetterObject(Character_Letter _sender, Character_Letter _receiver, List<SO_LetterBlock> blocks, int _days)
    {
        sender = _sender;
        receiver = _receiver;

        letterText = "";
        totalAffection = 0;
        totalUnlocker_keys = new List<string>();

        for (int i = 0; i < blocks.Count; i++)
        {
            letterText += blocks[i].text;
            if (i != blocks.Count - 1)
                letterText += "\n\n";

            totalAffection += blocks[i].affection;

            if (blocks[i].unlocker_keys.Length > 0)
                foreach (string key in blocks[i].unlocker_keys)
                    totalUnlocker_keys.Add(key);
        }

        daysForArrival = _days;
    }

}
