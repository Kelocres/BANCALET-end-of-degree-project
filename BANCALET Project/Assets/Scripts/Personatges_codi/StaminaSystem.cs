using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    public int MAX_VALUE = 100;
    public int currentValue = 50;
    public UI_ValueBar bar;

    //Per tindre el medidor de feed molt ple o molt buit
    public int LITTLE_RECOVERY = 10;
    
    //Per a quan es recupere stamina per dormir
    public int FULL_SLEEP_RECOVERY = 100; //Total del valor
    public int HALF_SLEEP_RECOVERY = 50;  //Mitat del valor
    public int QUARTER_SLEEP_RECOVERY = 25;

    //Exclusiu per a les proves
    public bool bloquejarConsumicio = false;

    // Start is called before the first frame update
    void Start()
    {
        //currentValue = 50;
        //maxValue = 100;
        if (bar != null)
            bar.SetValues(MAX_VALUE, currentValue);
    }

    // Update is called once per frame
    public bool CheckStamina() { return currentValue > 0; }
    
    public void ConsumeStamina(int actionValue)
    {
        if (bloquejarConsumicio) return;

        currentValue -= actionValue;
        if (currentValue < 0) currentValue = 0;

        if (bar != null)
            bar.UpdateValue(currentValue);
    }

    public void RecoverStamina(int recoverValue)
    {
        if (bloquejarConsumicio) return;

        currentValue += recoverValue;
        if (currentValue > MAX_VALUE) currentValue = MAX_VALUE;

        if (bar != null)
            bar.UpdateValue(currentValue);
    }

    //Es crida quan el FeedingSystem evalua els valors al dormir
    public void FixedRecovery(int newValue)
    {
        if (bloquejarConsumicio) return;

        currentValue = newValue;

        if (bar != null)
            bar.UpdateValue(currentValue);

    }
}
