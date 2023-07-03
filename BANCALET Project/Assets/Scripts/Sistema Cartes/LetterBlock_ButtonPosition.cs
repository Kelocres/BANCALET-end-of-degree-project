using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBlock_ButtonPosition : MonoBehaviour
{
    public RectTransform container;

    public RectTransform pos_UpLeft;
    public RectTransform pos_UpRight;
    public RectTransform pos_DownLeft;
    public RectTransform pos_DownRight;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveButtons();
    }

    // OnValidate() : 
    // Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector.
    // Use this to perform an action after a value changes in the Inspector; for example, making sure that data stays within a certain range.
    // REF: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnValidate.html
    private void OnValidate()
    {
        MoveButtons();
    }

    private void MoveButtons()
    {
        if (container == null) return;

        if(pos_UpLeft != null)
        {
            //pos_UpLeft.position = transform.position;
            pos_UpLeft.position = new Vector2(transform.position.x - 100, transform.position.y);
        }
        if (pos_UpRight != null)
        {
            pos_UpRight.position = container.anchoredPosition;
        }
        if (pos_DownLeft != null)
        {
            pos_DownLeft.position = container.anchoredPosition;
        }
        if (pos_DownRight != null)
        {
            pos_DownRight.position = container.offsetMin;
        }
    }
}
