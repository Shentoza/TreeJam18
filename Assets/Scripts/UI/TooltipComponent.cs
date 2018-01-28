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
            Debug.Log("Enter");
            UIManager.Instance.showTooltip(tree);
        }
    }

    void OnMouseExit()
    {
        Debug.Log("HIDE"); 
        if(tree)
            UIManager.Instance.hideTooltip();
    }
}
