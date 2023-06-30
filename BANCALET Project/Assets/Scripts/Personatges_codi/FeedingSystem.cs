using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingSystem : MonoBehaviour
{
    public int MAX_VALUE = 100;
    public int currentValue = 50;
    public UI_ValueBar bar;

    public int CORRECT_VALUE = 70;  //Mitat del valor
    public int DEFICIENT_VALUE = 25;
    public int HOURLY_CONSUMPTION_VALUE = 5;

    public StaminaSystem stamina;

    // El sistema de hora per a la consumició d'Aliment
    private LightingManager lightingManager;

    // Degut a que el LightingManager es buggea un poc quan arriba a l'hora 0,
    // esta variable controlarà que no es cride a HourlyConsumption més vegades en 
    // la mateixa hora
    private int current_hour;

    //Exclusiu per a les proves
    public bool bloquejarConsumicio = false;

    public delegate void delCanviValor();
    public event delCanviValor dCanviValor;

    void Start()
    {
        if (bar != null)
            bar.SetValues(MAX_VALUE, currentValue);

        lightingManager = FindObjectOfType<LightingManager>();
        if(lightingManager == null)
        {
            Debug.Log("FeedingSystem Start() LightingManager == null!!!");
        }
        else
        {
            //lightingManager.nouDia += NouDia;
            lightingManager.eventHora += HourlyConsumption;
            lightingManager.dormir += SleepConsumption;
            current_hour = (int)lightingManager.dataTimeOfDay.TimeOfDay;
        }
    }

    public bool CheckFeeding()
    {
        return currentValue < MAX_VALUE;
    }

    public void Feed(int recoverValue)
    {
        if (bloquejarConsumicio) return;

        Debug.Log("FeedingSystem Feed() currentValue = "+currentValue+"; recoverValue = "+recoverValue);
        currentValue += recoverValue;
        if (currentValue > MAX_VALUE) currentValue = MAX_VALUE;

        Debug.Log("FeedingSystem Feed() new currentValue = " + currentValue );


        if (bar != null)
            bar.UpdateValue(currentValue);
        if (dCanviValor != null) dCanviValor();
    }

    //Es crida des del sistema de hores
    public void HourlyConsumption(int new_hour)
    {
        if (bloquejarConsumicio || current_hour == new_hour) return;

        //Debug.Log("FeedingSystem HourlyConsumption(hour = " + new_hour + ") currentValue = " + currentValue + "; stamina value = " + stamina.currentValue);
        //Modificació de la Stamina
        ModifyStamina();

        //Restar al valor de Alimentació
        currentValue -= HOURLY_CONSUMPTION_VALUE;
        if (currentValue < 0) currentValue = 0;

        current_hour = new_hour;

        if (bar != null)
            bar.UpdateValue(currentValue);
        if (dCanviValor != null) dCanviValor();
    }

    public void SleepConsumption(int horaInici, int horaFinal)
    {
        if (bloquejarConsumicio) return;

        Debug.Log("FeedingSystem SleepConsumption() currentValue =" + currentValue);
        if(currentValue >= CORRECT_VALUE)
        {
            stamina.FixedRecovery(stamina.FULL_SLEEP_RECOVERY);
            currentValue /= 2;
        }
        else if(currentValue >= DEFICIENT_VALUE)
        {
            stamina.FixedRecovery(stamina.HALF_SLEEP_RECOVERY);
            currentValue /= 4;
        }
        else
        {
            stamina.FixedRecovery(stamina.QUARTER_SLEEP_RECOVERY);
            currentValue = 0;
        }

        //Si l'hora final és a les 12 (es desperta després d'un desmaig), els valors de Feeding i Stamina estaràn a la mitat.
        if(horaFinal == 12)
        {
            currentValue /= 2;
            stamina.currentValue /= 2;
        }

        if (bar != null)
            bar.UpdateValue(currentValue);
        if (dCanviValor != null) dCanviValor();
    }

    public void ModifyStamina()
    {
        if (stamina == null) return;

        if (currentValue >= CORRECT_VALUE)
            stamina.RecoverStamina(stamina.LITTLE_RECOVERY);
        else if (currentValue < DEFICIENT_VALUE)
            stamina.ConsumeStamina(stamina.LITTLE_RECOVERY);
    }
}
