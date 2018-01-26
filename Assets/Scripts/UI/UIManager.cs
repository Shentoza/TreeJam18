using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;

    [SerializeField]
    Text value1;

	// Use this for initialization
	void Start () {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DummyComponent.OnValue1Change += value1Changed;

    }
	
	// Update is called once per frame
	void Destroy()
    {
        Instance = null;
        DummyComponent.OnValue1Change -= value1Changed;
    }

    void value1Changed(int value)
    {
        value1.text = "Seeds: " + value;
    }


}
