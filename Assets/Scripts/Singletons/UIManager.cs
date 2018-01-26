using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Prefab("Prefabs/UI", true)]
public class UIManager : Singleton<UIManager> {

    [SerializeField]
    Text sporeScore;
    int increments = 5;
    int currentValue, newValue;

	// Use this for initialization
	void Start () {
        DummyComponent.OnValue1Change += value1Changed;
        currentValue = newValue = 0;
    }

    void Update()
    {
        if(currentValue != newValue)
        {
            if (newValue < currentValue)
            {
                currentValue = Mathf.Clamp(currentValue + increments, currentValue, newValue);
            }
            else
            {
                currentValue = Mathf.Clamp(currentValue - increments, newValue, currentValue);
            }
            sporeScore.text = ""+currentValue;
        }
    }
	
	// Update is called once per frame
	void Destroy()
    {
        DummyComponent.OnValue1Change -= value1Changed;
    }

    void value1Changed(int value)
    {
        newValue = value;
    }


}
