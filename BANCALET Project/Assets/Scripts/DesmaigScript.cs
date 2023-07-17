using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesmaigScript : MonoBehaviour
{
    LightingManager lm;
    public int HORA_DESMAIG = 2;
    public IniciaTimeline timelineDesmaig;
    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        lm = FindObjectOfType<LightingManager>();
        soundManager = FindObjectOfType<SoundManager>();
        if(lm != null)
        {
            lm.eventHora += ComporovarHora;
            Debug.Log("DesmaigScript Start() LightingManager.eventHora += ComprovarHora");
        }
    }

   

    public void ComporovarHora(int hora)
    {
        Debug.Log("DesmaigScript ComprovarHora("+hora+")");

        if (hora != HORA_DESMAIG) return;

        if(timelineDesmaig == null)
        {
            Debug.Log("DesmaigScript ComprovarHora() timelineDesmaig == null!!!");
            return;
        }
        soundManager.StopMusic();
        timelineDesmaig.StartTimeline();
    }
}
