using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Prefab("Prefabs/Singletons/UIManager", true)]
public class UIManager : Singleton<UIManager> {

    [SerializeField]
    Text value1;

	// Use this for initialization
	void Start () {
        DummyComponent.OnValue1Change += value1Changed;

    }
	
	// Update is called once per frame
	void Destroy()
    {
        DummyComponent.OnValue1Change -= value1Changed;
    }

    void value1Changed(int value)
    {
        value1.text = "Seeds: " + value;
    }


}
