using System.Collections;
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

	// Use this for initialization
	void Start () {
        EventManager.OnSporeChange += sporeValueChanged;
        EventManager.OnTreeCountChange += treeValueChanged;
        currentSporeValue = newSporeValue = 0;
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

    void sporeValueChanged(float oldValue, float newValue)
    { 
        this.newSporeValue = newValue;
    }

    void treeValueChanged(int oldValue, int newValue)
    {
        treeScore.text = ""+newValue;
    }


}
