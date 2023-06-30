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
            txtHora.text = lightManager.GetTimeOfDay().ToString();
            //txtHora.text = Data_Lighting.instance.GetTimeOfDay().ToString();


    }
}
