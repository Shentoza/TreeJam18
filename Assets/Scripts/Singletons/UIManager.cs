using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField]
    Text sporeScore;
    float increments = 1;
    float currentValue, newValue;

	// Use this for initialization
	void Start () {
        EventManager.OnSporeChange += value1Changed;
        currentValue = newValue = 0;
    }

    void Update()
    {
        if(Mathf.Abs(currentValue - newValue) >= 1.0f)
        {
            if (newValue > currentValue)
            {
                currentValue = Mathf.Clamp(currentValue + increments, currentValue, newValue);
            }
            else
            {
                currentValue = Mathf.Clamp(currentValue - increments, newValue, currentValue);
            }
            Debug.Log(currentValue);
            sporeScore.text = ""+currentValue;
        }
    }
	
	// Update is called once per frame
	void Destroy()
    {
        EventManager.OnSporeChange -= value1Changed;
    }

    void value1Changed(float oldValue, float newValue)
    { 
        this.newValue = newValue;
    }


}
