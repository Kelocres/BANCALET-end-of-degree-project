using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROVA_CanviaHeight : MonoBehaviour
{
    public float canviaHeight;
    public RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        canviaHeight = rectTransform.sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, canviaHeight);
    }
}
