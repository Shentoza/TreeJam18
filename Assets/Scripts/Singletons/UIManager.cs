﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField]
    Text sporeScore;
    float sporeIncrements = 1;
    float currentSporeValue, newSporeValue;

    [SerializeField]
    Text treeScore;

    [SerializeField]
    GameObject tooltipPanel;
    [SerializeField]
    Text tooltipText;

    public bool tooltipShowing { private set; get; }

    // Use this for initialization
    void Start () {
        EventManager.OnSporeChange += sporeValueChanged;
        EventManager.OnTreeCountChange += treeValueChanged;
        currentSporeValue = newSporeValue = 0;
        tooltipPanel.SetActive(false);
        tooltipShowing = false;
    }

    void Update()
    {
        if(Mathf.Abs(currentSporeValue - newSporeValue) >= 1.0f)
        {
            if (newSporeValue > currentSporeValue)
            {
                currentSporeValue = Mathf.Clamp(currentSporeValue + sporeIncrements, currentSporeValue, newSporeValue);
            }
            else
            {
                currentSporeValue = Mathf.Clamp(currentSporeValue - sporeIncrements, newSporeValue, currentSporeValue);
            }
            sporeScore.text = ""+currentSporeValue;
        }
    }
	
	void Destroy()
    {
        EventManager.OnSporeChange -= sporeValueChanged;
        EventManager.OnTreeCountChange -= treeValueChanged;
    }

    void OnGUI()
    {
        if (tooltipShowing)
        {
            Vector2 mousePosLocked = Event.current.mousePosition;
            Vector2 halfBounds = tooltipPanel.GetComponent<RectTransform>().rect.size;
            halfBounds += new Vector2(10, 10);
            halfBounds *= 0.5f;
            mousePosLocked.x = Mathf.Clamp(mousePosLocked.x + halfBounds.x, halfBounds.x, Camera.main.pixelWidth - halfBounds.x);
            mousePosLocked.y = Mathf.Clamp(Camera.main.pixelHeight - (mousePosLocked.y + halfBounds.y), halfBounds.y, Camera.main.pixelHeight- halfBounds.y);
            Vector3 mouse = new Vector3(mousePosLocked.x, mousePosLocked.y, 0);
            tooltipPanel.transform.position = mouse;    
        }
    }

    void sporeValueChanged(float oldValue, float newValue)
    { 
        this.newSporeValue = newValue;
    }

    void treeValueChanged(int oldValue, int newValue)
    {
        treeScore.text = ""+newValue;
    }

    public void showTooltip(string text)
    {
        tooltipShowing = true;
        tooltipPanel.SetActive(true);
        tooltipText.text = text;
    }

    public void hideTooltip()
    {
        tooltipShowing = false;
        tooltipPanel.SetActive(false);
    }
}
