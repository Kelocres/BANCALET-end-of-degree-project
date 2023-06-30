using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REF: https://www.youtube.com/watch?v=m9hj9PdO328&list=PLBejj_VWui0T7m9E7AQOOBBwsUNXZbrQs&index=55&t=304s

// [ExecuteAlways] Makes instances of a script always execute, both as part of Play Mode and when editing.
//      The [ExecuteAlways] attribute can be used when you want your script to perform certain things as part of Editor tooling,
//      which is not necessarily related to the Play logic that happens in buildt Players and in Play Mode. Sometimes the Play
//      functionality of such a script is identical to its Edit Mode functionality, while other times they differ greatly.
//      REF: https://docs.unity3d.com/ScriptReference/ExecuteAlways.html

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    //Variables
    //[SerializeField, Range(0, 24)] public float TimeOfDay { get; private set; } // El Range i els getters/setters no combinen bé
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    //La variable que indica quans segons son una hora de joc
    public float SECONDS_PER_GAMEHOUR = 1f;

    //Per a crear un Singleton
    //public static LightingManager instance;

    //Per a recollir i guardar l'informació
    //private Data_Lighting dataLighting;
    public SO_TimeOfDay dataTimeOfDay;

    //Delegate per a comunicar events al ManejaPlantes quan escomença un dia nou
    //public delegate void triggerNouDia();
    //public event triggerNouDia nouDia;

    //
    public delegate void triggerEventHora(int hora);
    public event triggerEventHora eventHora;

    //Delegate per a comunicar events quan el jugador dorm
    public delegate void triggerDormir(int horaInici, int horaFinal);
    public event triggerDormir dormir;

    //Per a controlar els events que ocorren a una certa hora del dia
    //private Dictionary<int, bool> horari;
    private int seguentHora;
    //private bool eventHoraRealitzat;

    //Per a saber quan despertarse
    public float WAKING_HOUR = 6f;
    public float WAKING_AFTER_FAINT_HOUR = 12f;

    //Per a les comprovacions de la llum ambiental
    public Color getAmbientColor = Color.black;
    public Color getFogColor = Color.black;
    public Color getDirectionalLightColor = Color.black;


    private void Awake()
    {
        if(dataTimeOfDay != null)
        {
            TimeOfDay = dataTimeOfDay.TimeOfDay;
            Debug.Log("LightingManager Awake() TimeOfDay:" + TimeOfDay);

            //Configurar horari segons el valor de dataTimeOfDay
            seguentHora = (int)TimeOfDay + 1;
            if (seguentHora >= 24) seguentHora %= 24;
            
        }


    }

    private void Update()
    {
        if (Preset == null) return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime / SECONDS_PER_GAMEHOUR;
            TimeOfDay %= 24; //Clamp between 0-24
            UpdateLighting(TimeOfDay / 24f);
        }
        else
            UpdateLighting(TimeOfDay / 24f);

        ControlHorari();

    }

    private void UpdateLighting(float timePercent)
    {
        //NOTA: Es necessari que en Window -> Rendering -> Lightining -> Environment ->
        // Environment Lighting -> Source el valor siga Color i no Skybox
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        getAmbientColor = RenderSettings.ambientLight;

        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        getFogColor = RenderSettings.fogColor;

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            getDirectionalLightColor = DirectionalLight.color;

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void ControlHorari()
    {
        if((TimeOfDay > seguentHora && seguentHora < 24) || (seguentHora == 0 && TimeOfDay < 1f))
        {
            //Debug.Log("LightingManager ControlHorari() Event de l'hora " + seguentHora+", TimeOfDay="+TimeOfDay);
            if(eventHora!=null)
                eventHora(seguentHora);

            seguentHora = (int)TimeOfDay + 1;
            if (seguentHora >= 24) seguentHora %= 24;
        }
    }

    // OnValidate() : 
    // Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector.
    // Use this to perform an action after a value changes in the Inspector; for example, making sure that data stays within a certain range.
    // REF: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnValidate.html
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
            DirectionalLight = RenderSettings.sun;
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    
    public float GetTimeOfDay()
    {
        return TimeOfDay;
    }

    //Si es tanca la aplicació, es guarda la data de día 
    private void OnApplicationQuit()
    {
        //dataLighting.TimeOfDay = TimeOfDay;
        if (dataTimeOfDay != null)
        {
            dataTimeOfDay.TimeOfDay = TimeOfDay;
            Debug.Log("LightingManager OnApplicationQuit() TimeOfDay:" + TimeOfDay);
        }
    }

    //Es crida des del Signal Receiver quan es realitza la cinemàtica de dormir
    public void CalculsNouDia()
    {
        Debug.Log("LightingManager CalculsNouDia()");
        //Cridar a ManejaPlantes per a que comprove les plantes que creixquen entre
        //l'hora actual i la WAKING_HOUR, i al FeedingSystem per a la variació de 
        //Alimentació i Stamina al dormir
        if (dormir != null) dormir((int)TimeOfDay, (int)WAKING_HOUR);
        

        //Canviar hora i actualitzar il·luminació
        TimeOfDay = WAKING_HOUR;
        seguentHora = (int)WAKING_HOUR + 1;
        UpdateLighting(TimeOfDay / 24f);



    }

    //Es crida des del Signal Receiver quan es realitza la cinemàtica del desmaig
    public void CalculsDesmaig()
    {
        Debug.Log("LightingManager CalculsDesmaig()");
        //Cridar a ManejaPlantes per a que comprove les plantes que creixquen entre
        //l'hora actual i la WAKING_HOUR, i al FeedingSystem per a la variació de 
        //Alimentació i Stamina al dormir
        if (dormir != null) dormir((int)TimeOfDay, (int)WAKING_AFTER_FAINT_HOUR);


        //Canviar hora i actualitzar il·luminació
        TimeOfDay = WAKING_AFTER_FAINT_HOUR;
        seguentHora = (int)WAKING_HOUR + 1;
        UpdateLighting(TimeOfDay / 24f);



    }


}
