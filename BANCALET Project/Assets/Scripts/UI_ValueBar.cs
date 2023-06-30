using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ValueBar : MonoBehaviour
{
    public Image uiDisplay;

    private float maxValue;
    private float currentValue;

    //Per a fer els canvis més vistosos
    private float newValue;
    private float displayValue;

    private bool alreadySetValues = false;
    // Start is called before the first frame update
    public void SetValues(float _maxValue, float _currentValue)
    {
        if (alreadySetValues) return;

        maxValue = _maxValue;
        currentValue = _currentValue;
        alreadySetValues = true;

        if (uiDisplay != null)
            uiDisplay.transform.localScale = new Vector3(1f, currentValue / maxValue, 1f);
    }

    // Update is called once per frame
    public void UpdateValue(float _newValue)
    {
        //Versió bàsica
        currentValue = _newValue;
        if (uiDisplay != null)
            uiDisplay.transform.localScale = new Vector3(1f, currentValue / maxValue, 1f);

    }
}
