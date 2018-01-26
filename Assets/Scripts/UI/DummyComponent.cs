using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyComponent : MonoBehaviour {


    private int value1;
    private int value2;

    public delegate void value1Change(int newValue);
    public static event value1Change OnValue1Change;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("w"))
        {
            setValue1(value1 + 100);
        }
        if (Input.GetKeyDown("s"))
            setValue1(value1 - 100);
	}

    public void setValue1(int value)
    {
        value1 = value;
        OnValue1Change(value);
    }
}
