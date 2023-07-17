using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarHora : MonoBehaviour
{
    public float DayTime;
    private Text txtHora;

    private LightingManager lightManager;
    void Start()
    {
        txtHora = GameObject.FindGameObjectWithTag("txtHora").GetComponent<Text>();
        lightManager = FindObjectOfType<LightingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //SoundManager.instance.StartMusic(menuTheme);
        if (txtHora != null && lightManager)
        {
            int hours = (int)lightManager.GetTimeOfDay();
            float decimals = (lightManager.GetTimeOfDay() % 1) * 10;
            int minutes = ((int)decimals * 60) / 10;
            //Debug.Log("MostraHora Update() x=" + x);

            txtHora.text = hours.ToString()+":"+minutes.ToString();
            //txtHora.text = Data_Lighting.instance.GetTimeOfDay().ToString();
        }



    }
}
