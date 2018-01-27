using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipComponent : MonoBehaviour {

    [SerializeField]
    string text = "This is "+"\n" +"debug stuff!";

    void OnMouseEnter()
    {
        UIManager.Instance.showTooltip(text);
    }

    void OnMouseExit()
    {
        UIManager.Instance.hideTooltip();
    }
}
