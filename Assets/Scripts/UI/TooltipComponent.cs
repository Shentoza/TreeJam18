using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipComponent : MonoBehaviour {

    private ShroomTree tree;

    void Start()
    {
        tree = GetComponent<ShroomTree>();
    }

    void OnMouseEnter()
    {
        if(tree)
        {
            UIManager.Instance.showTooltip(tree);
        }
    }

    void OnMouseExit()
    {
        if(tree)
            UIManager.Instance.hideTooltip();
    }
}
