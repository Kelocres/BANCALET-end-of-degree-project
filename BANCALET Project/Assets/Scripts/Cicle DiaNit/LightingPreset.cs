//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

//REF: https://www.youtube.com/watch?v=m9hj9PdO328&list=PLBejj_VWui0T7m9E7AQOOBBwsUNXZbrQs&index=55&t=304s
[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Scriptables/Lighting Preset", order = 1)]
public class LightingPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
}
